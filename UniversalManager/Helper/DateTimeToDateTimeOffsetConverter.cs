using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UniversalManager.Helper
{
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        //DateTime->DateTimeOffset
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is DateTime date)
            {
                DateTimeOffset dateOffset = date;
                return dateOffset;
            }
            return DateTimeOffset.Now;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset date)
            {
                return date.DateTime;
            }
            return DateTime.Now;
        }
    }
}
