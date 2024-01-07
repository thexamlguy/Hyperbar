using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.Windows;

public abstract class ValueConverter<TSource, TTarget> :
    MarkupExtension,
    IValueConverter
{
    protected override object ProvideValue(IXamlServiceProvider serviceProvider)
    {
        return this;
    }

    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        return ConvertTo((TSource)value, targetType, parameter, language);
    }

    public object? ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return ConvertBackTo((TTarget)value, targetType, parameter, language);
    }

    public TTarget? Convert(TSource value)
    {
        return ConvertTo(value, null, null, null);
    }

    public TSource? ConvertBack(TTarget value)
    {
        return ConvertBackTo(value, null, null, null);
    }

    protected virtual TTarget? ConvertTo(TSource value, Type? targetType, object? parameter, string? language)
    {
        return default;
    }

    protected virtual TSource? ConvertBackTo(TTarget value, Type? targetType, object? parameter, string? language)
    {
        return default;
    }
}
