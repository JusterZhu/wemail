using System;
using System.Globalization;
using System.Windows.Data;

namespace Wemail.Common.Converts
{
    public class SexConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isParse = int.TryParse(value.ToString(), out int result);
            if (isParse)
            {
                string sex = result == 1 ? "女" : "男";
                return sex;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
