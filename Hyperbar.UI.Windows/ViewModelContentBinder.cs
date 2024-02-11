using Microsoft.UI.Xaml;
using System.Reflection;

namespace Hyperbar.UI.Windows;

public class ViewModelContentBinder(NavigationTargetCollection contents) : 
    IViewModelContentBinder
{
    public void Bind(FrameworkElement view,
        object context)
    {
        if (context.GetType().GetCustomAttributes<NavigationTargetAttribute>()
            is IEnumerable<NavigationTargetAttribute> attributes)
        {
            foreach (NavigationTargetAttribute attribute in attributes)
            {
                if (view.FindName(attribute.Name) is FrameworkElement content)
                {
                    contents.Add(attribute.Name, content);
                    void HandleUnloaded(object sender, RoutedEventArgs args)
                    {
                        view.Unloaded -= HandleUnloaded;
                        contents.Remove(attribute.Name);
                    }

                    view.Unloaded += HandleUnloaded;
                }
            }
        }
    }
}
