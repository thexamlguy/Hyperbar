namespace Hyperbar;

public partial class ValueViewModel<TValue>(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, disposer)
{
    public TValue? Value { get; set; }
}
