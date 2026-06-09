using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab4.Models
{
    public class MeasurementChannel//вимірювальний канал
    {
        private static int _totalChannels = 0;//заг

        private int _channelNumber;

        private List<Device> _devices;

        public ObservableCollection<Device> Devices { get; private set; }

        // Властивість для доступу до номера каналу
        public int ChannelNumber => _channelNumber;

        public static int TotalChannels => _totalChannels;

        // Конструктор для створення нового каналу
        public MeasurementChannel()
        {
            _totalChannels++;
            _channelNumber = _totalChannels;

            _devices = new List<Device>();//ініціалізація звичайної колекції

            Devices = new ObservableCollection<Device>();//ініціалізація ObservableCollection для UI
        }

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            _devices.Add(device);
            Devices.Add(device);
        }

        // Синхронізація ObservableCollection з основною колекцією
        public void SyncCollections()
        {
            Devices.Clear();
            foreach (var device in _devices)
            {
                Devices.Add(device);
            }
        }

        // Отримання List для роботи з plain колекцією
        public List<Device> GetPlainList()
        {
            return _devices.ToList();  // Повертаємо копію
        }

        // Оновлення обох колекцій з plain списку
        public void UpdateFromPlainList(List<Device> plainList)
        {
            _devices = plainList.ToList();//оновлюємо основну колекцію
            Devices.Clear();
            foreach (var device in _devices)
            {
                Devices.Add(device);
            }
        }

        // Видалення пристрою
        public bool RemoveDevice(Device device)
        {
            bool removedFromList = _devices.Remove(device);
            bool removedFromObs = Devices.Remove(device);
            return removedFromList || removedFromObs;//повертаємо true, якщо пристрій був видалений з будь-якої колекції
        }

        // Очищення всіх пристроїв
        public void ClearDevices()
        {
            _devices.Clear();
            Devices.Clear();
        }
    }
}