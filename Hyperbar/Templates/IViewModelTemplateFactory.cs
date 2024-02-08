namespace Hyperbar;

public interface IViewModelTemplateFactory
{
    object? Create(object key);
}