using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Hyperbar.UI.Windows;

public class ViewModelTemplatePresenter :
    ContentPresenter
{
    public ViewModelTemplatePresenter()
    {
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(FrameworkElement sender, 
        DataContextChangedEventArgs args)
    {
        //if (DataContext is IViewModelTemplate templatedViewModel)
        //{
        //    Content = templatedViewModel.TemplateFactory
        //        .Create(DataContext.GetType().Name);
        //}
    }
}