using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace TwitterRx
{
  public class TimestampConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      DateTime timestanp = (DateTime)value;

      var diff = DateTime.Now - timestanp;

      if (diff.TotalSeconds < 60)
      {
        return string.Format("{0:N0} seconds ago", diff.TotalSeconds);
      }

      if (diff.TotalMinutes < 60)
      {
        return string.Format("{0:N0} minutes ago", diff.TotalMinutes);
      }

      if (diff.TotalHours < 12)
      {
        return string.Format("{0:N0} hours ago", diff.TotalHours);
      }

      return timestanp.ToString("d/M/yyyy");
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
