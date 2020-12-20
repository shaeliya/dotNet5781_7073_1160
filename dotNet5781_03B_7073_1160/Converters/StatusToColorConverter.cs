using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace dotNet5781_03B_7073_1160.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum.Status && value != null)
            {
                var status = (Enum.Status)value;
                var color = new SolidColorBrush(Colors.White);

                switch (status)
                {
                    case Enum.Status.ReadyToGo:
                        color = new SolidColorBrush(Colors.Green);
                        break;
                    case Enum.Status.Refueling:
                        color = new SolidColorBrush(Colors.Orange);
                        break;
                    case Enum.Status.InTreatment:
                        color = new SolidColorBrush(Colors.Lavender);
                        break;
                    case Enum.Status.MidTravel:
                        color = new SolidColorBrush(Colors.Pink);
                        break;
                    case Enum.Status.NeedFuel:
                        color = new SolidColorBrush(Colors.Azure);
                        break;
                    case Enum.Status.NeedTreatment:
                        color = new SolidColorBrush(Colors.LightSeaGreen);
                        break;
                    case Enum.Status.NeedFuelAndTreatment:
                        color = new SolidColorBrush(Colors.MistyRose);
                        break;
                    default:
                        color = new SolidColorBrush(Colors.White);
                        break;
                }

                return color;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
