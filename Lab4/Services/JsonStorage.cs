using Lab4.DTO;
using Lab4.Models;
using System.IO;
using System.Text.Json;

public static class JsonStorage
{
    public static void Save(MeasurementChannel channel, string path)
    {
        // Створюємо DTO з додатковою інформацією
        var dto = new MeasurementChannelDto
        {
            Devices = channel.Devices.Select(d => new DeviceDto
            {//зберегти інформацію про пристрій
                MountingPosition = d.MountingPosition,
                CalibrationDate = d.CalibrationDate,
                SerialNumber = d.SerialNumber,
                Sensor = new SensorDto
                {
                    QuantityType = (int)d.Sensor.QuantityType,
                    MinValue = d.Sensor.MinValue,
                    MaxValue = d.Sensor.MaxValue,
                    CurrentValue = d.Sensor.CurrentValue
                }
            }).ToList(),
            
            ChannelNumber = channel.ChannelNumber//зберегти номер каналу
        };

        string json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });//записати з C# в JSON у файл
        File.WriteAllText(path, json);
    }

    public static MeasurementChannel Load(string path)
    {
        string json = File.ReadAllText(path);
        MeasurementChannelDto? dto = JsonSerializer.Deserialize<MeasurementChannelDto>(json);//зчитати з JSON у C# об'єкт

        if (dto == null)
            throw new InvalidDataException("Помилка десеріалізації.");

        // Створюємо канал
        var channel = new MeasurementChannel();

        // Додаємо пристрої
        foreach (var d in dto.Devices)
        {
            Sensor sensor = new Sensor(
                (QuantityType)d.Sensor.QuantityType,
                d.Sensor.MinValue,
                d.Sensor.MaxValue,
                d.Sensor.CurrentValue);

            Device device = new Device(sensor, d.MountingPosition, d.CalibrationDate, d.SerialNumber);

            channel.AddDevice(device);//додати пристрій до каналу
        }

        //перевіряємо валідність даних
        foreach (var device in channel.Devices)
        {
            device.Validate();
        }

        return channel;
    }
}