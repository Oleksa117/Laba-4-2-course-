using Lab4.Models;

namespace Lab4.Models
{
    public class Sensor
    {
        public QuantityType QuantityType { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

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
    }
}