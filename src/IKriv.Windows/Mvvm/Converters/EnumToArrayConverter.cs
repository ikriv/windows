using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace IKriv.Windows.Mvvm.Converters
{
    public class EnumToArrayConverter : MarkupExtension, IValueConverter
    {
        public string NoneValue { get; set; }
        public bool Sort { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Type type = parameter as Type;

            if (type == null) return DependencyProperty.UnsetValue;

            List<string> values = new List<string>(Enum.GetNames(type));
            if (Sort) values.Sort();
            if (NoneValue != null) values.Insert(0, NoneValue);

            return values.ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
