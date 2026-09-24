using System;
using System.Globalization;
using Avalonia.Data.Converters;
using EasyPrice.Services;

namespace EasyPrice.Converters;

public class DolarToBs : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var ValueDolar = (double)value;
        var DolarPrice = SettingsService.Settings.DolarPrice;
        var res = ValueDolar * DolarPrice;
        return res;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}