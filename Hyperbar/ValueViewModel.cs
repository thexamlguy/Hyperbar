using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class ValueViewModel<TValue>(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private TValue? value;
}
