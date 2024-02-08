﻿namespace Hyperbar;

public class ViewModelTemplateDescriptorProvider(IEnumerable<IViewModelTemplateDescriptor> descriptors) :
    IViewModelTemplateDescriptorProvider
{
    public IViewModelTemplateDescriptor? Get(object key)
    {
        if (descriptors.FirstOrDefault(x => x.Key == key)
            is IViewModelTemplateDescriptor descriptor)
        {
            return descriptor;
        }

        return default;
    }
}