using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class ViewModelTemplateFactory(IViewModelTemplateDescriptorProvider descriptors,
    IServiceProvider services) :
    IViewModelTemplateFactory
{
    public object? Create(object key)
    {
        if (descriptors.Get(key)
            is IViewModelTemplateDescriptor descriptor)
        {
            if (services.GetRequiredKeyedService(descriptor.TemplateType,
                descriptor.Key) is { } template)
            {
                return template;
            }
        }

        return default;
    }
}