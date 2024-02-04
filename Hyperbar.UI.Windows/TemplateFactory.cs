using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.UI.Windows;

public class TemplateFactory(IEnumerable<IContentTemplateDescriptor> descriptors,
    IServiceProvider provider) :
    ITemplateFactory
{
    public object? Create(object key)
    {
        if (descriptors.FirstOrDefault(x => x.Key == key)
            is IContentTemplateDescriptor descriptor)
        {
            if (provider.GetRequiredKeyedService(descriptor.TemplateType,
                descriptor.Key) is { } template)
            {
                return template;
            }
        }

        return default;
    }
}