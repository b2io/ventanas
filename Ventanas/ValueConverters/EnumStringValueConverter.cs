using System;
using System.Globalization;
using System.Windows.Data;
using Base2io.Util.EnumUtil;

namespace Base2io.Ventanas.ValueConverters
{
    class EnumStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsEnum = value as Enum;
            return valueAsEnum != null ? valueAsEnum.GetStringValue() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
