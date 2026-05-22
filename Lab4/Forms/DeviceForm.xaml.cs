using Lab4.Forms;
using Lab4.Models;
using Lab4.Models;
using System;
using System.Windows;

namespace Lab4.Forms
{
    public partial class DeviceForm : Window
    {
        private Sensor _sensor;

        public Device CreatedDevice { get; private set; }

        public DeviceForm()
        {
            InitializeComponent();

            CalibrationDatePicker.SelectedDate = DateTime.Today;
        }

        private void SensorButton_Click(object sender, RoutedEventArgs e)
        {
            SensorForm form = new SensorForm();

            if (form.ShowDialog() == true)
            {
                _sensor = form.CreatedSensor;

                MessageBox.Show("Датчик створено.");
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sensor == null)
                {
                    MessageBox.Show("Спочатку створіть датчик.");
                    return;
                }

                int position =
                    int.Parse(PositionTextBox.Text);

                DateTime calibrationDate =
                    CalibrationDatePicker.SelectedDate.Value;

                CreatedDevice = new Device(
                    _sensor,
                    position,
                    calibrationDate);

                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show(
                    "Перевірте правильність введених даних.");
            }
        }
    }
}