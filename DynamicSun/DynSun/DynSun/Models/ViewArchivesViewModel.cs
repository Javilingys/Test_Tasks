using X.PagedList;

namespace DynSun.Models
{
    public class ViewArchivesViewModel
    {
        public IPagedList<ArchiveDto> ArchivesPagedList { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
