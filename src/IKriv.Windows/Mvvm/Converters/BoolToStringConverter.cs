using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace IKriv.Windows.Mvvm.Converters
{
    public class BoolToStringConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public string True { get; set; }
        public string False { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return String.Empty;
            else if (value is bool) return ((bool)value) ? True : False;
            else if (value is bool?) return ((bool?)value).Value ? True : False;
            else if (value.Equals(false) || value.Equals("False")) return False;
            else if (value.Equals(true) || value.Equals("True")) return True;
            else return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            var s = value.ToString();
            if (s == null) return DependencyProperty.UnsetValue;
            if (s.ToUpper() == True.ToUpper()) return true;
            if (s.ToUpper() == False.ToUpper()) return false;
            return DependencyProperty.UnsetValue;
        }
    }
}
