using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace IKriv.Windows.Mvvm.Converters
{
    // this is a converter that does absolutely nothing; use it to debug bindings
    class TrivialConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
