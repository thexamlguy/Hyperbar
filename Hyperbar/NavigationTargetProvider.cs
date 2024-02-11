using Hyperbar.UI.Windows;

namespace Hyperbar;

public class NavigationTargetProvider(NavigationTargetCollection navigationTargets) :
    INavigationTargetProvider
{
    public object? Get(string name) =>
        navigationTargets.TryGetValue(name, out object? target) ? target : default;
}
