using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.Windows.UI;

public class TemplateGeneratorControl :
    ContentControl
{
    public TemplateGeneratorControl()
    {
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (DataContext is ITemplatedViewModel templatedViewModel)
        {
            Content = templatedViewModel.TemplateFactory.Create(DataContext.GetType().Name);
        }
        else
        {

        }
    }
}