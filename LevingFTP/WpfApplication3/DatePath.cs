using System;
using System.Windows.Controls;

namespace FtpClient
{
    class DatePath
    {
        readonly string selectedDay;
        readonly string selectedMonth;
        readonly string selectedYear;
        readonly string hour;
        readonly public string fullPath;

        public DatePath(DatePicker calendar, TextBox textBox)
        {
            
            this.selectedDay = calendar.SelectedDate.Value.Day < 10 ?
                $"0{calendar.SelectedDate.Value.Day.ToString()}" :
                calendar.SelectedDate.Value.Day.ToString();

            this.selectedMonth = calendar.SelectedDate.Value.Month < 10 ?
                $"0{calendar.SelectedDate.Value.Month.ToString()}" :
                calendar.SelectedDate.Value.Month.ToString();

            this.selectedYear = calendar.SelectedDate.Value.Year.ToString();

            this.hour = Convert.ToInt32(textBox.Text) < 10 ?
                $"0{textBox.Text}" :
                textBox.Text;
            fullPath = selectedYear + selectedMonth + selectedDay + "/" + hour + "/";
            
        }
    }
}
