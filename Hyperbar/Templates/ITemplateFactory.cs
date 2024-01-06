namespace Hyperbar;

public interface ITemplateFactory
{
    object? Create(object key);
}