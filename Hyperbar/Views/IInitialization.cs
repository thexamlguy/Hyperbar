using System.Windows.Input;

namespace Hyperbar;

public interface IInitialization
{
    ICommand Initialize { get; }

    Task InitializeAsync();
}
