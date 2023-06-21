namespace DynSun.DbModels
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public DateOnly DateOnly { get; set; }
        public TimeOnly TimeOnly { get; set; }
        public float Temperature { get; set; }
        public sbyte AirHumidity { get; set; }
        public float Td { get; set; }
        public short Pressure { get; set; }
        public string WindDirection { get; set; }
        public sbyte? WindSpeed { get; set; }
        public sbyte? Cloudy { get; set; }
        public short? H { get; set; }
        public string HorizontalView { get; set; }
        public string Other { get; set; }
    }
}
