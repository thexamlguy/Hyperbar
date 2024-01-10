using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.Windows.UI;

public class TemplateGenerator : DataTemplateSelector
{
    protected override DataTemplate SelectTemplateCore(object item)
    {
        string xamlString = @"
                <DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                xmlns:ui='using:Hyperbar.Windows.UI'>
                    <ui:TemplateGeneratorControl />
                </DataTemplate>";

        return (DataTemplate)XamlReader.Load(xamlString);
    }

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
    {
        string xamlString = @"
                <DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                xmlns:ui='using:Hyperbar.Windows.UI'>
                    <ui:TemplateGeneratorControl />
                </DataTemplate>";

        return (DataTemplate)XamlReader.Load(xamlString);
    }
}
