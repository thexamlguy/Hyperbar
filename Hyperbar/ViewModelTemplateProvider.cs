namespace Hyperbar;

public class ViewModelTemplateProvider(IEnumerable<IViewModelTemplate> viewModelTemplates) :
    IViewModelTemplateProvider
{
    public IViewModelTemplate? Get(object key)
    {
        if (viewModelTemplates.FirstOrDefault(x => x.Key.Equals(key))
            is IViewModelTemplate viewModelTemplate)
        {
            return viewModelTemplate;
        }

        return default;
    }
}
