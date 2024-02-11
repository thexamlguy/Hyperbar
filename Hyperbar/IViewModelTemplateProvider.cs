namespace Hyperbar;

public interface IViewModelTemplateProvider
{
    IViewModelTemplate? Get(object key);
}
