using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Collections;

namespace IKriv.Windows.Mvvm.Converters
{
    public class NullToVisConverter : MarkupExtension, IValueConverter
    {
        public bool Invert { get; set; }
        public bool ExcludeEmptyLists { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool visible = value != null;

            if (value != null && ExcludeEmptyLists)
            {
                var enumerable = value as IEnumerable;
                if (enumerable != null && !enumerable.Cast<object>().Any())
                {
                    visible = false;
                }
            }

            if (Invert) visible = !visible;
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
