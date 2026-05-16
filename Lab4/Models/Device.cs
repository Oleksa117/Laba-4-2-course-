using Lab4.Models;
using System;

namespace Lab4.Models
{
    public class Device
    {
        public Sensor Sensor { get; set; }

        public int MountingPosition { get; set; }

        public DateTime CalibrationDate { get; set; }

        public Device(Sensor sensor,int mountingPosition,DateTime calibrationDate)
        {
            Sensor = sensor;
            MountingPosition = mountingPosition;
            CalibrationDate = calibrationDate;
        }

        public override string ToString()
        {
            return $"Пристрій №{MountingPosition}";
        }
    }
}