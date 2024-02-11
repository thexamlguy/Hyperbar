using System.Windows.Input;

namespace Hyperbar;

public interface IInitialization
{
    ICommand InitializeCommand { get; }

    Task InitializeAsync();
}
