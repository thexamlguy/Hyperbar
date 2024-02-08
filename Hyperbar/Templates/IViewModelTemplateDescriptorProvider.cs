namespace Hyperbar;

public interface IViewModelTemplateDescriptorProvider
{
    IViewModelTemplateDescriptor? Get(object key);
}
