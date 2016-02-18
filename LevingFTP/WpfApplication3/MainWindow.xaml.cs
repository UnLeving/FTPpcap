using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace FtpClient
{
    public partial class MainWindow : Window
    {
        RunFTP runFTP;
        List<string> resultsList;
        string key;

        public MainWindow()
        {
            InitializeComponent();
            resultsList = new List<string>();
            calendar.SelectedDate = DateTime.Now.Date;
        }

        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            listView.IsEnabled = false;
            btnSave.IsEnabled = false;
            listView.ItemsSource = null;
            resultsList.Clear();

            if (key5.IsChecked == true) key = key5.Name;
            if (key8.IsChecked == true) key = key8.Name;
            if (key9.IsChecked == true) key = key9.Name;
            runFTP = new RunFTP(key, new DatePath(calendar, textBoxHour).fullPath, textBoxPhoneNumber.Text);
           
            resultsList = await runFTP.GetFileList();

            if (resultsList.Count > 0)
            {
                listView.IsEnabled = true;
                listView.ItemsSource = resultsList;
                btnSave.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (string selectedString in listView.SelectedItems)
                runFTP.Downloading(selectedString);

            MessageBox.Show("Done");
        }

        private void pnl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short value;
            if (!short.TryParse(e.Text, out value))
                e.Handled = true;
        }

        private void pnl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxPhoneNumber.Text != string.Empty && textBoxHour.Text != string.Empty && Convert.ToInt32(textBoxHour.Text) < 25)
                btnRun.IsEnabled = true;
            else
                btnRun.IsEnabled = false;
        }
    }
}
