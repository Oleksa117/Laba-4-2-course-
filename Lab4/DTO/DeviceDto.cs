using Lab4.DTO;
using System;

namespace Lab4.DTO
{
    public class DeviceDto
    {
        public SensorDto Sensor { get; set; }

        public int MountingPosition { get; set; }

        public DateTime CalibrationDate { get; set; }
    }
}