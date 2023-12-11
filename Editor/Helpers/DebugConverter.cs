using System;
using System.Linq;
using System.Reflection;
using Avalonia.Data.Converters;

namespace AvaloniaEditor.Helpers
{
  public class DebugConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value == null)
      {
        return "Debug Output: null";
      }

      var type = value.GetType();
      var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      var propertyValues = properties
          .Select(prop => $"{prop.Name} = {prop.GetValue(value)}")
          .ToArray();

      var output = $"Debug Output: {type.Name} - {string.Join(", ", propertyValues)}";
      Console.WriteLine(output);
      return output;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      Console.WriteLine($"Debug Output: {value.ToString()}");
      return $"Debug Output: {value}"; ;
    }
  }
}