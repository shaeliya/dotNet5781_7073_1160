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
    public class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum.Status && value != null)
            {
                var status = (Enum.Status)value;
                var image = "carTreatment.jpg";

                switch (status)
                {
                    case Enum.Status.ReadyToGo:
                        image = "ReadyToGo.jpg";
                        break;
                    case Enum.Status.Refueling:
                        image = "CarFuel.jpg";
                        break;
                    case Enum.Status.InTreatment:
                        image = "CarTreat.jpg";
                        break;
                    case Enum.Status.MidTravel:
                        image = "CarTravel.png";
                        break;
                    case Enum.Status.NeedFuel:
                        image = "NeedFuel.jpg";
                        break;
                    case Enum.Status.NeedTreatment:
                        image = "CarNeedTreatment.jpg";
                        break;
                    case Enum.Status.NeedFuelAndTreatment:
                        image = "ExclamationMark.png";
                        break;
                    default:
                        image = "ExclamationMark.png";
                        break;
                }

                return image;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
