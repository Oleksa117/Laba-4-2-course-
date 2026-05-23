using Lab4.Forms;
using Lab4.Models;
using System.Windows;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        private MeasurementChannel _channel;

        public MainWindow()
        {
            InitializeComponent();

            _channel = new MeasurementChannel();

            RefreshList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DeviceForm form = new DeviceForm();

            if (form.ShowDialog() == true)
            {
                _channel.Devices.Add(form.CreatedDevice);

                RefreshList();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is Device device)
            {
                _channel.Devices.Remove(device);

                RefreshList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is not Device device)
                return;

            DeviceForm form = new DeviceForm(device);

            if (form.ShowDialog() == true)
            {
                int index = _channel.Devices.IndexOf(device);

                _channel.Devices[index] =form.CreatedDevice;

                RefreshList();
            }
        }

        private void RefreshList()
        {
            DevicesListBox.ItemsSource = null;
            DevicesListBox.ItemsSource = _channel.Devices;
        }
    }
}