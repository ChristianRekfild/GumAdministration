using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GumAdministration.Converters;

public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isReversed = parameter?.ToString() == "True";
        bool isVisible = value != null;
        if (isReversed) isVisible = !isVisible;
        return isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}