using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hyperbar.Desktop;

public class TemplateFactory(ITemplateGeneratorFactory factory,
    IEnumerable<IDataTemplateDescriptor> descriptors,
    IServiceProvider provider) :
    DataTemplateSelector,
    ITemplateFactory
{
    protected override DataTemplate SelectTemplateCore(object item) => factory.Create();

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => factory.Create();

    public object? Create(object key)
    {
        if (descriptors.FirstOrDefault(x => x.Key == key) is IDataTemplateDescriptor descriptor)
        {
            if (provider.GetRequiredKeyedService(descriptor.TemplateType, descriptor.Key) is { } template)
            {
                return template;
            }
        }

        return default;
    }
}