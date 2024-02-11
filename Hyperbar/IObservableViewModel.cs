namespace Hyperbar;

public interface IObservableViewModel :
    IInitialization,
    IDisposable
{
    public IDisposer Disposer { get; }

    public IPublisher Publisher { get; }

    public IServiceFactory ServiceFactory { get; }

    public IServiceProvider ServiceProvider { get; }
}