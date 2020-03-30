using RS_SHOP_Dev.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RS_SHOP_Dev.Views.ValueConverters
{
    public class ButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result;
            if (!(value==null))
            {
                result = value.ToString().StartsWith("F") ? AppResources.OrderGetQR : AppResources.OrderTrack;
            }
            else
                result = AppResources.OrderGetQR;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().StartsWith("F") ? "GET QR" : "TRACK";
        }
    }
}
