using Hyperbar.UI.Windows;

namespace Hyperbar.Widget.Windows;

public class WidgetConfigurationViewModel<TConfiguration, TValue> :
    ValueViewModel<TValue>, 
    INotificationHandler<Changed<TConfiguration>> 
    where TConfiguration : 
    class
{
    private readonly Func<TConfiguration, TValue> valueFactory;

    public WidgetConfigurationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        IDisposer disposer,
        Func<TConfiguration, TValue> valueFactory) : base(serviceProvider, serviceFactory, publisher, disposer)
    {
        this.valueFactory = valueFactory;
    }

    public Task Handle(Changed<TConfiguration> args, 
        CancellationToken cancellationToken = default)
    {
        if (args.Value is TConfiguration configuration)
        {
            valueFactory.Invoke(configuration);
        }

        return Task.CompletedTask;
    }
}

public class WidgetConfigurationViewModel :
    ObservableCollectionViewModel<IObservableViewModel>
{
    public WidgetConfigurationViewModel(IViewModelTemplateSelector viewModelTemplateSelector,
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        ViewModelTemplateSelector = viewModelTemplateSelector;

        Add<WidgetConfigurationViewModel<WidgetAvailability,
            bool>>((Func<WidgetAvailability, bool>)(config => config.Value));
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }
}