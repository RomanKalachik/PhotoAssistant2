using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PhotoAssistant.Controls.Wpf {
    public class DoubleToStringConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return string.Format("{0} {1}", value, "sec.");
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string val = value as string;
            if(val == null)
                return 0.0;
            string[] str = val.Split(' ');
            return double.Parse(str[0]);
        }
    }
}
