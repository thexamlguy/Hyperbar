using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Hyperbar;

public class ObservableViewModel : 
    ObservableObject,
    IObservableViewModel
{
    private bool isInitialized;

    public ObservableViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer)
    {
        ServiceProvider = serviceProvider;
        ServiceFactory = serviceFactory;
        Publisher = publisher;
        Disposer = disposer;

        subscriber.Add(this);
    }

    public IDisposer Disposer { get; }

    public ICommand InitializeCommand =>
        new AsyncRelayCommand(CoreInitializeAsync);

    public IPublisher Publisher { get; }

    public IServiceFactory ServiceFactory { get; }

    public IServiceProvider ServiceProvider { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Disposer.Dispose(this);
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
