using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.UI.Windows;

public class ViewModelBinder
{
    public void Bind(object viewModel, 
        FrameworkElement view) 
    {
        view.DataContext ??= viewModel;

    }
}
public class ViewModelTemplate(IViewModelTemplateDescriptorProvider descriptors) :
    DataTemplateSelector, 
    IViewModelTemplate
{
    protected override DataTemplate SelectTemplateCore(object item)
    {
        return descriptors.Get(item.GetType().Name) is IViewModelTemplateDescriptor descriptor
            ? CreateDataTemplate(descriptor)
            : new DataTemplate();
    }

    protected override DataTemplate SelectTemplateCore(object item,
        DependencyObject container) =>
        SelectTemplateCore(item);

    private static DataTemplate CreateDataTemplate(IViewModelTemplateDescriptor descriptor)
    {
        string xamlString = @$"
                <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                              xmlns:ui=""using:{descriptor.TemplateType.Namespace}"">
                      <ui:{descriptor.TemplateType.Name} />
                </DataTemplate>";

        return (DataTemplate)XamlReader.Load(xamlString);
    }
}
