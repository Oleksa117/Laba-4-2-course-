using System.IO;
using System.Windows;
using Lab4.Forms;
using Lab4.Models;
using Lab4.Services;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        private MeasurementChannel _channel;
        private const string FileName = "channel.json";

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(FileName))
            {
                _channel = JsonStorage.Load(FileName);
            }
            else
            {
                _channel = new MeasurementChannel();
            }

            DevicesListBox.ItemsSource = _channel.Devices;

            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;

            Closing += MainWindow_Closing;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DeviceForm form = new DeviceForm();

            if (form.ShowDialog() == true)
            {
                _channel.Devices.Add(form.CreatedDevice);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is Device device)
            {
                _channel.Devices.Remove(device);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is not Device device)
                return;

            Device copy = new Device(device);

            DeviceForm form = new DeviceForm(copy);

            if (form.ShowDialog() == true)
            {
                int index = _channel.Devices.IndexOf(device);

                _channel.Devices[index] = form.CreatedDevice;
            }
        }


        private void MainWindow_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            JsonStorage.Save(_channel,FileName);
        }

        private void DevicesListBox_SelectionChanged(object sender,System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool selected = DevicesListBox.SelectedItem != null;

            EditButton.IsEnabled = selected;
            DeleteButton.IsEnabled = selected;
        }
    }
}