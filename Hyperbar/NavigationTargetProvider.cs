using Hyperbar.UI.Windows;

namespace Hyperbar;

public class NavigationTargetProvider(NavigationTargetCollection navigationTargets) :
    INavigationTargetProvider
{
    public object? Get(string name) =>
        navigationTargets.TryGetValue(name, out object? target) ? target : default;

    public bool TryGet(string name, 
        out object? value)
    {
        if (navigationTargets.TryGetValue(name,
            out object? target))
        {
            value = target;
            return true;
        }
        else
        {
            value = null;
            return false;
        }
    }
}
