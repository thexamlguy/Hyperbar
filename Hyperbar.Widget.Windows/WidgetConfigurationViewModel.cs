using CommunityToolkit.Mvvm.ComponentModel;
using Hyperbar.UI.Windows;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Hyperbar.Widget.Windows;

public partial class WidgetConfigurationViewModel<TConfiguration, TValue> :
    ValueViewModel<TValue>, 
    INotificationHandler<Changed<TConfiguration>> 
    where TConfiguration : 
    class
{
    private readonly Func<TConfiguration, TValue> read;
    private readonly Func<TValue, TConfiguration> write;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private string? title;

    public WidgetConfigurationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        IDisposer disposer,
        ISubscriber subscriber,
        string? title,
        Func<TConfiguration, TValue> read,
        Func<TValue, TConfiguration> write) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        this.title = title;
        this.read = read;
        this.write = write;
    }

    public WidgetConfigurationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        IDisposer disposer,
        ISubscriber subscriber,
        string? title,
        string? description,
        Func<TConfiguration, TValue> read,
        Func<TValue, TConfiguration> write) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        this.title = title;
        this.description = description;

        this.read = read;
        this.write = write;
    }

    public Task Handle(Changed<TConfiguration> args, 
        CancellationToken cancellationToken = default)
    {
        if (args.Value is TConfiguration configuration)
        {
            Value = read.Invoke(configuration);
        }

        return Task.CompletedTask;
    }

    protected override void OnChanged(TValue? value)
    {
        base.OnChanged(value);
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
            bool>>("Widget", (Func<WidgetAvailability, bool>)(config => config.Value));
    }

    public IViewModelTemplateSelector ViewModelTemplateSelector { get; }
}