using System;

namespace Lab4.DTO
{
    public class DeviceDto
    {
        public SensorDto? Sensor { get; set; }  //може бути null, якщо дані не надані
        public int MountingPosition { get; set; }
        public DateTime CalibrationDate { get; set; }
        public string? SerialNumber { get; set; }  
    }
}