using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.UI.Windows;

public class ViewModelTemplate : 
    MarkupExtension
{
    protected override object ProvideValue(IXamlServiceProvider serviceProvider) =>
        new ViewModelTemplateSelector();

    internal class ViewModelTemplateSelector : 
        DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item) =>
            item is IObservableViewModel observableViewModel &&
            observableViewModel.ServiceProvider.GetService<IViewModelTemplateDescriptorProvider>()
                is ViewModelTemplateDescriptorProvider descriptors &&
            descriptors.Get(item.GetType().Name) is IViewModelTemplateDescriptor descriptor
                ? CreateDataTemplate(descriptor)
                : new DataTemplate();

        protected override DataTemplate SelectTemplateCore(object item, 
            DependencyObject container) =>
            SelectTemplateCore(item);

        private DataTemplate CreateDataTemplate(IViewModelTemplateDescriptor descriptor)
        {
            string xamlString = @$"
                <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                              xmlns:ui=""using:{descriptor.TemplateType.Namespace}"">
                      <ui:{descriptor.TemplateType.Name} />
                </DataTemplate>";

            return (DataTemplate)XamlReader.Load(xamlString);
        }
    }
}
