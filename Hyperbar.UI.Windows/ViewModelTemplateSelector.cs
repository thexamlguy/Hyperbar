using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.UI.Windows;

public class ImplicitTemplate :
    MarkupExtension
{
    protected override object ProvideValue(IXamlServiceProvider serviceProvider) =>
        new ImplicitTemplateSelector();

    internal class ImplicitTemplateSelector :
        DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, 
            DependencyObject container)
        {
   

            string xamlString = @"
                <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                              xmlns:ui=""using:Hyperbar.UI.Windows"">
                      <ui:ViewModelTemplatePresenter VerticalContentAlignment=""Stretch""
                                            HorizontalContentAlignment=""Stretch"" 
                                            HorizontalAlignment=""Stretch"" 
                                            VerticalAlignment=""Stretch""/>
                </DataTemplate>";

            return (DataTemplate)XamlReader.Load(xamlString);
        }
    }
}

public class ViewModelTemplate :
    MarkupExtension
{
    protected override object ProvideValue(IXamlServiceProvider serviceProvider) => 
        new ViewModelTemplateSelector();

    internal class ViewModelTemplateSelector :
        DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is IObservableViewModel observableViewModel)
            {
                if (observableViewModel.ServiceProvider.GetService<IViewModelTemplateDescriptorProvider>()
                    is ViewModelTemplateDescriptorProvider descriptors)
                {
                    if (descriptors.Get(item.GetType().Name) is IViewModelTemplateDescriptor descriptor)
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

            return new DataTemplate();
        }

        protected override DataTemplate SelectTemplateCore(object item, 
            DependencyObject container)
        {
            if (item is IObservableViewModel observableViewModel)
            {
                if (observableViewModel.ServiceProvider.GetService<IViewModelTemplateDescriptorProvider>()
                    is ViewModelTemplateDescriptorProvider descriptors)
                {
                    if (descriptors.Get(item.GetType().Name) is IViewModelTemplateDescriptor descriptor)
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

            return new DataTemplate();
        }
    }
}
