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
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace TwitterRx
{
  public sealed class ImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      try
      {
        Debug.WriteLine(value);
        return new BitmapImage(new Uri((string)value));
      }
      catch
      {
        return new BitmapImage();
      }
    }

    public object ConvertBack(object value, Type targetType,
                              object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
