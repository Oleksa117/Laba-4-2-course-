using Lab4.Models;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Sensor
    {
        [Required]
        public QuantityType QuantityType { get; set; }

        [Range(-1000000, 1000000)]
        public double MinValue { get; set; }

        [Range(-1000000, 1000000)]
        public double MaxValue { get; set; }

        [Range(-1000000, 1000000)]
        public double CurrentValue { get; set; }

        public Sensor(QuantityType quantityType,double minValue,double maxValue,double currentValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentException(
                    "Мінімальне значення повинно бути меншим за максимальне.");
            }

            if (currentValue < minValue || currentValue > maxValue)
            {
                throw new ArgumentException(
                    "Поточне значення повинно знаходитись у заданому діапазоні.");
            }

            QuantityType = quantityType;
            MinValue = minValue;
            MaxValue = maxValue;
            CurrentValue = currentValue;
        }

        public Sensor(Sensor other)
        {
            QuantityType = other.QuantityType;
            MinValue = other.MinValue;
            MaxValue = other.MaxValue;
            CurrentValue = other.CurrentValue;
        }
    }

}