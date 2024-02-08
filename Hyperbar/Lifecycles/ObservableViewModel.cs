using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Hyperbar;

public class ObservableViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) : 
    ObservableObject,
    IObservableViewModel
{
    private bool isInitialized;

    public IDisposer Disposer => disposer;

    public ICommand InitializeCommand =>
            new AsyncRelayCommand(CoreInitializeAsync);

    public IMediator Mediator => mediator;

    public IServiceFactory ServiceFactory => serviceFactory;

    public IServiceProvider ServiceProvider => serviceProvider;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        disposer.Dispose(this);
    }

    public virtual Task InitializeAsync() => Task.CompletedTask;

    private async Task CoreInitializeAsync()
    {
        if (isInitialized)
        {
            return;
        }

        isInitialized = true;
        await InitializeAsync();
    }
}
