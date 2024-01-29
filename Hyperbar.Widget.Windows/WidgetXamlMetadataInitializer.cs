using Microsoft.UI.Xaml.Markup;

namespace Hyperbar.Widget.Windows;

public class WidgetXamlMetadataInitializer(IWidgetAssembly widgetAssembly,
    IList<IXamlMetadataProvider> xamlMetadataProviders) :
    IInitializer
{
    public Task InitializeAsync()
    {
        foreach (IXamlMetadataProvider xamlMetadataProvider in widgetAssembly.Assembly.ExportedTypes
            .Where(type => type.IsAssignableTo(typeof(IXamlMetadataProvider)))
            .Select(metadataType => (IXamlMetadataProvider)Activator.CreateInstance(metadataType)!))
        {
            xamlMetadataProviders.Add(xamlMetadataProvider);
        }

        return Task.CompletedTask;
    }
}
