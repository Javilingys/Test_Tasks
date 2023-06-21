namespace DynSun.Models
{
    public class ArchiveDto
    {
        public int Id { get; set; }
        public string DateOnly { get; set; }
        public string TimeOnly { get; set; }
        public string Temperature { get; set; }
        public string AirHumidity { get; set; }
        public string Td { get; set; }
        public string Pressure { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Cloudy { get; set; }
        public string H { get; set; }
        public string HorizontalView { get; set; }
        public string Other { get; set; }
    }
}
