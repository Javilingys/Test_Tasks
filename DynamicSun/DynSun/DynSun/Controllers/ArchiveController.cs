using DynSun.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using DynSun.DbModels;
using DynSun.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DynSun.Controllers
{
    ///////////////////// Вынес бы в отдельные классы, но в тестовом какая разница :)
    public class ViewArchiveParams
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 25;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int PageIndex { get; set; } = 1;

        public string? Month { get; set; }
        public string? Year { get; set; }
    }

    public static class SelectConstants
    {
        private static readonly List<string> _months = new()
        {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        };

        private static readonly List<int> _years = new();

        static SelectConstants()
        {
            for (int i = 2010; i <= 2023; i++)
            {
                _years.Add(i);
            }
        }

        public static IReadOnlyCollection<string> Months => _months.AsReadOnly();
        public static IReadOnlyCollection<int> Years => _years.AsReadOnly();

        public static List<SelectListItem> MonthsSelectList =>
            _months.Select((x, index) => new SelectListItem(x, (++index).ToString())).ToList();

        public static List<SelectListItem> YearsSelectList =>
            _years.Select(x => new SelectListItem(x.ToString(), x.ToString())).ToList();
    }
    /////////////////////

    public class ArchiveController : Controller
    {
        private readonly ILogger<ArchiveController> _logger;

        public ArchiveController(ILogger<ArchiveController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ViewArchive([FromServices] AppDbContext dbContext, 
            ViewArchiveParams viewArchiveParams)
        {
            // Вынес бы в сервис. И далее от размера проекта, если не большой, то прям в сервис инжектить дбКОнтекст
            // Если же сервис большой, то реппозитори/юнит оф ворк паттерн поверх EF
            var archivesPagedList = await dbContext.WeatherModels
                .AsNoTracking()
                .Where(x => (string.IsNullOrEmpty(viewArchiveParams.Year) ||
                             x.DateOnly.Year == int.Parse(viewArchiveParams.Year)) &&
                            (string.IsNullOrEmpty(viewArchiveParams.Month) ||
                             x.DateOnly.Month == int.Parse(viewArchiveParams.Month)))
                .OrderBy(x => x.DateOnly)
                .ThenBy(x => x.TimeOnly)
                .Select(x => new ArchiveDto
                {
                    Id = x.Id,
                    TimeOnly = x.TimeOnly.ToString(),
                    AirHumidity = x.AirHumidity.ToString(),
                    Cloudy = x.Cloudy == null ? "" : x.Cloudy.ToString(),
                    DateOnly = x.DateOnly.ToString(),
                    H = x.H == null ? "" : x.H.ToString(),
                    HorizontalView = x.HorizontalView,
                    Other = x.Other,
                    Pressure = x.Pressure.ToString(),
                    Td = x.Td.ToString(),
                    Temperature = x.Temperature.ToString(),
                    WindDirection = x.WindDirection,
                    WindSpeed = x.WindSpeed == null ? "" : x.WindSpeed.ToString()
                })
                .ToPagedListAsync(viewArchiveParams.PageIndex, viewArchiveParams.PageSize);

            ViewData["Months"] = SelectConstants.MonthsSelectList; 
            ViewData["Years"] = SelectConstants.YearsSelectList;

            return View(new ViewArchivesViewModel
            {
                ArchivesPagedList = archivesPagedList,
                Month = viewArchiveParams.Month ?? "",
                Year = viewArchiveParams.Year ?? ""
            });
        }

        [HttpGet]
        public IActionResult LoadArchives()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostArchives(IFormCollection files, [FromServices] AppDbContext dbContext)
        {
            try
            {
                // Вынес бы в сервис логику

                // 12 - кол-во месяцев/кол-во sheet в одном excel; 240 - среднее кол-во строк в одном sheet
                var approximatelyRowsCount = files.Count * 12 * 240;
                var addedRows = new List<WeatherModel>(approximatelyRowsCount);

                foreach (var file in files.Files)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    ms.Position = 0;
                    var book = new XSSFWorkbook(ms);

                    for (int i = 0; i < book.NumberOfSheets; i++)
                    {
                        int startRow = 4;
                        var sheet = book.GetSheetAt(i);

                        for (int rowIndex = startRow; rowIndex < sheet.LastRowNum; rowIndex++)
                        {
                            var currentRow = sheet.GetRow(rowIndex);
                            addedRows.Add(ParseRowToWeatherModel(currentRow));
                        }
                    }
                }

                dbContext.AddRange(addedRows);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ViewData["ExcelError"] = "Ошибка. Некорректные данные или формат файла.";
            }

            return View(nameof(LoadArchives));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private WeatherModel ParseRowToWeatherModel(IRow currentRow)
        {
            var date = currentRow.GetCell(0);
            var time = currentRow.GetCell(1);
            var t = currentRow.GetCell(2);
            var percent = currentRow.GetCell(3);
            var td = currentRow.GetCell(4);
            var mmrt = currentRow.GetCell(5);
            var direction = currentRow.GetCell(6);
            var speed = currentRow.GetCell(7);
            var obl = currentRow.GetCell(8);
            var h = currentRow.GetCell(9);
            var vv = currentRow.GetCell(10);
            var weather = currentRow.GetCell(11);

            var model = new WeatherModel();

            model.DateOnly = DateOnly.FromDateTime(DateTime.Parse(date.StringCellValue));
            model.TimeOnly = TimeOnly.FromTimeSpan(TimeSpan.Parse(time.StringCellValue));
            model.Temperature = (float)t.NumericCellValue;
            model.AirHumidity = (sbyte)percent.NumericCellValue;
            model.Td = (float)td.NumericCellValue;
            model.Pressure = (short)mmrt.NumericCellValue;
            model.WindDirection = direction.StringCellValue;
            model.WindSpeed = speed.CellType switch
            {
                CellType.Numeric => (sbyte)speed.NumericCellValue,
                CellType.String => null
            };
            model.Cloudy = obl.CellType switch
            {
                CellType.Numeric => (sbyte)obl.NumericCellValue,
                CellType.String => null
            };
            model.H = h.CellType switch
            {
                CellType.Numeric => (short)h.NumericCellValue,
                CellType.String => null
            };
            model.HorizontalView = vv.CellType switch
            {
                CellType.Numeric => vv.NumericCellValue.ToString(),
                CellType.String => vv.StringCellValue
            };
            model.Other = weather is null ? "" : weather.StringCellValue;

            return model;
        }
    }
}
