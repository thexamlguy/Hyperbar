using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows;

public class TemplateFactory(ITemplateGeneratorFactory factory,
    IEnumerable<IContentTemplateDescriptor> descriptors,
    IServiceProvider provider) :
    DataTemplateSelector,
    ITemplateFactory
{
    protected override DataTemplate SelectTemplateCore(object item) => factory.Create();

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => factory.Create();

    public object? Create(object key)
    {
        if (descriptors.FirstOrDefault(x => x.Key == key) is IContentTemplateDescriptor descriptor)
        {
            if (provider.GetRequiredKeyedService(descriptor.TemplateType, descriptor.Key) is { } template)
            {
                return template;
            }
        }

        return default;
    }
}