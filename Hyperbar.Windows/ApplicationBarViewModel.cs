using Hyperbar.UI.Windows;

namespace Hyperbar.Widget;


[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class ApplicationBarViewModel :
    ObservableCollectionViewModel<IDisposable>
{
    public ApplicationBarViewModel(IViewModelTemplate template, 
        IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;

        Add<PrimaryViewModel>(0);
        Add<SecondaryViewModel>(1);
    }

    public IViewModelTemplate Template { get; }
}