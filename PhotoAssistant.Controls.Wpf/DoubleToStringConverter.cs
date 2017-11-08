using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
namespace PhotoAssistant.Controls.Wpf {
    public class DoubleToStringConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => $"{value} {"sec."}";
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string val = value as string;
            if(val == null) {
                return 0.0;
            }

            string[] str = val.Split(' ');
            return double.Parse(str[0]);
        }
    }
}
