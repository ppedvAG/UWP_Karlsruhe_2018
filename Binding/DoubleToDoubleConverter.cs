using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Binding
{
    public class DoubleToDoubleConverter : IValueConverter
    {

        public int WeitererParameter { get; set; }

        //Source -> Target
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value != null && double.TryParse(value.ToString(), out double number1))
            {
                if(parameter != null && double.TryParse(parameter.ToString(), out double number2))
                {
                    return number1 / number2;
                }
            }
            return 12;
        }

        //Target -> Source
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
