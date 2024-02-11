using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.UI.Windows;

public class ViewModelTemplateSelector(IViewModelTemplateProvider descriptors) :
    DataTemplateSelector, 
    IViewModelTemplateSelector
{
    protected override DataTemplate SelectTemplateCore(object item)
    {
        return descriptors.Get(item.GetType().Name) is Hyperbar.IViewModelTemplate descriptor
            ? CreateDataTemplate(descriptor)
            : new DataTemplate();
    }

    protected override DataTemplate SelectTemplateCore(object item,
        DependencyObject container) =>
        SelectTemplateCore(item);

    private static DataTemplate CreateDataTemplate(Hyperbar.IViewModelTemplate descriptor)
    {
        string xamlString = @$"
                <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                              xmlns:ui=""using:{descriptor.TemplateType.Namespace}"">
                      <ui:{descriptor.TemplateType.Name} />
                </DataTemplate>";

        return (DataTemplate)XamlReader.Load(xamlString);
    }
}
