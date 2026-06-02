using Lab4.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Device
    {
        [Required]
        public Sensor Sensor { get; set; }

        [Range(1, int.MaxValue,
        ErrorMessage = "Номер місця встановлення повинен бути більше 0.")]
        public int MountingPosition { get; set; }

        [Required]
        public DateTime CalibrationDate { get; set; }

        public double Temperature { get; set; }

        [RegularExpression(@"^[A-Z]{2}-\d{4}$",
       ErrorMessage = "Серійний номер повинен бути у форматі AA-1234.")]
        public string SerialNumber { get; set; }

        public Device(Sensor sensor,int mountingPosition,DateTime calibrationDate,string serialNumber)
        {
            Sensor = sensor;
            MountingPosition = mountingPosition;
            CalibrationDate = calibrationDate;
            SerialNumber = serialNumber;
        }
        public Device(Device other)
        {
            Sensor = new Sensor(other.Sensor);
            MountingPosition = other.MountingPosition;
            CalibrationDate = other.CalibrationDate;
            SerialNumber = other.SerialNumber;
        }

        public override string ToString()
        {
            return $"Пристрій №{MountingPosition} | " +
                   $"Серійний №: {SerialNumber} | " +
                   $"Величина: {Sensor.QuantityType} | " +
                   $"Значення: {Sensor.CurrentValue}";
        }
    }
}