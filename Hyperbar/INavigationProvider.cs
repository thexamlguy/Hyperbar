namespace Hyperbar;

public interface INavigationProvider
{
    INavigation? Get(Type type);
}
