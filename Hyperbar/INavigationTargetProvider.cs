namespace Hyperbar;

public interface INavigationTargetProvider
{
    object? Get(string name);

    bool TryGet(string name, 
        out object? value);
}