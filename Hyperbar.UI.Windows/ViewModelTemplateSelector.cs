using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.UI.Windows;

public class ViewModelTemplateSelector :
    DataTemplateSelector, 
    IViewModelTemplateSelector
{
    protected override DataTemplate SelectTemplateCore(object item)
    {
        return item is IObservableViewModel observableViewModel && observableViewModel.ServiceProvider.GetService<IViewModelTemplateProvider>()
                is IViewModelTemplateProvider viewModelTemplateProvider
            ? viewModelTemplateProvider.Get(item.GetType().Name) is IViewModelTemplate viewModelTemplate
                ? CreateDataTemplate(viewModelTemplate)
                : new DataTemplate()
            : new DataTemplate();
    }

    protected override DataTemplate SelectTemplateCore(object item,
        DependencyObject container) =>
        SelectTemplateCore(item);

    private static DataTemplate CreateDataTemplate(IViewModelTemplate template)
    {
        string xamlString = @$"
                <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                              xmlns:ui=""using:{template.ViewType.Namespace}"">
                      <ui:{template.ViewType.Name} />
                </DataTemplate>";

        return (DataTemplate)XamlReader.Load(xamlString);
    }
}
