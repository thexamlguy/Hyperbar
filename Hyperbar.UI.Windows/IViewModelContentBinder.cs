using Microsoft.UI.Xaml;

namespace Hyperbar.UI.Windows;

public interface IViewModelContentBinder
{
    void Bind(FrameworkElement view, 
        object context);
}
