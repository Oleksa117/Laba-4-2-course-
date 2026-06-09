using Lab4.Models;
using Lab4.Models;
using System;
using System.Windows;

namespace Lab4.Forms
{
    public partial class SensorForm : Window//Датчик
    {
        public Sensor CreatedSensor { get; private set; }// зберігає створений датчик після успішного завершення форми

        public SensorForm()
        {
            InitializeComponent();

            QuantityTypeComboBox.ItemsSource =Enum.GetValues(typeof(QuantityType));

            QuantityTypeComboBox.SelectedIndex = 0;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuantityType quantityType =(QuantityType)QuantityTypeComboBox.SelectedItem;//повертає вибраний тип величини з ComboBox

                double minValue =double.Parse(MinValueTextBox.Text);

                double maxValue =double.Parse(MaxValueTextBox.Text);

                double currentValue =double.Parse(CurrentValueTextBox.Text);

                CreatedSensor = new Sensor(quantityType,minValue,maxValue,currentValue);

                DialogResult = true;
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні числові значення.","Помилка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (ArgumentException ex)//"Мін < макc"
            {
                MessageBox.Show(ex.Message,"Помилка",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
    }
}