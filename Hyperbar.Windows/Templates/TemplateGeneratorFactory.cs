using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.Windows;

public class TemplateGeneratorFactory : 
    ITemplateGeneratorFactory
{
    public DataTemplate Create()
    {
        string xamlString = @"
                <DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                                xmlns:desktop='using:Hyperbar.Windows'>
                    <desktop:TemplateGeneratorControl />
                </DataTemplate>";

        return (DataTemplate)XamlReader.Load(xamlString);
    }
}
