using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows.UI;

public class DataTemplateConverter : 
    ValueConverter<object, DataTemplateSelector> 
{
    protected override DataTemplateSelector? ConvertTo(object value, 
        Type? targetType, 
        object? parameter, 
        string? language)
    {
        return new TemplateGenerator();
    }
}
