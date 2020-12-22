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
                var image = string.Empty;

                switch (status)
                {
                    case Enum.Status.ReadyToGo:
                        image = "Resources/Images/ReadyToGo.jpg";
                        break;
                    case Enum.Status.Refueling:
                        image = "Resources/Images/CarFuel.jpg";
                        break;
                    case Enum.Status.InTreatment:
                        image = "Resources/Images/CarTreat.jpg";
                        break;
                    case Enum.Status.MidTravel:
                        image = "Resources/Images/CarTravel.png";
                        break;
                    case Enum.Status.NeedFuel:
                        image = "Resources/Images/NeedFuel.jpg";
                        break;
                    case Enum.Status.NeedTreatment:
                        image = "Resources/Images/CarNeedTreatment.jpg";
                        break;
                    case Enum.Status.NeedFuelAndTreatment:
                        image = "Resources/Images/ExclamationMark.png";
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
