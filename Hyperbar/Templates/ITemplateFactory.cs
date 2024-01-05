namespace Hyperbar.Templates;

public interface ITemplateFactory
{
    object? Create(object key);
}
