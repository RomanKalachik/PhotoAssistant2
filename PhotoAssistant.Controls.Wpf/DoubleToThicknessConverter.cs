﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PhotoAssistant.Controls.Wpf {
    public class DoubleToThicknessConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return new Thickness((double)value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
