namespace Hyperbar;

public interface INavigationTargetProvider
{
    object? Get(string name);
}