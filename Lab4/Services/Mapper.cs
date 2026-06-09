using Lab4.DTO;
using Lab4.Models;
using Lab4.DTO;
using Lab4.Models;
using System.Linq;

namespace Lab4.Services
{
    // Клас для перетворення між моделлю (MeasurementChannel) та DTO (MeasurementChannelDto) для серіалізації
    public static class Mapper
    {
        public static MeasurementChannelDto ToDto(MeasurementChannel channel)
        {
            return new MeasurementChannelDto
            {                                   
                Devices = channel.Devices.Select(d => new DeviceDto//перетворює колекцію об'єктів Device (модель) в колекцію об'єктів DeviceDto 
                {
                        MountingPosition = d.MountingPosition,
                        CalibrationDate = d.CalibrationDate,
                        SerialNumber = d.SerialNumber,

                    // Створюємо вкладений DTO для сенсора
                        Sensor = new SensorDto
                        {
                            // Конвертуємо enum в int для JSON
                            QuantityType = (int)d.Sensor.QuantityType,

                            MinValue = d.Sensor.MinValue,
                            MaxValue = d.Sensor.MaxValue,
                            CurrentValue = d.Sensor.CurrentValue
                        }
                    }).ToList()
            };
        }
        // Метод для перетворення DTO назад у модель
        public static MeasurementChannel FromDto(MeasurementChannelDto dto)
        {
            MeasurementChannel channel = new MeasurementChannel();

            foreach (var d in dto.Devices)
            {
                // Створюємо об'єкт Sensor на основі даних з DTO
                Sensor sensor = new Sensor(
                    (QuantityType)d.Sensor.QuantityType,
                    d.Sensor.MinValue,
                    d.Sensor.MaxValue,
                    d.Sensor.CurrentValue);

                // Створюємо об'єкт Device на основі даних з DTO та створеного Sensor
                Device device = new Device(sensor,d.MountingPosition,d.CalibrationDate, d.SerialNumber);
                // Додаємо пристрій до каналу
                channel.Devices.Add(device);
            }

            return channel;
        }
    }
}   