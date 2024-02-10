using Hyperbar.UI.Windows;

namespace Hyperbar.Windows;

public partial class SettingsViewModel :
    ObservableCollectionViewModel<INavigationViewModel>
{
    public SettingsViewModel(IViewModelTemplate template,
        IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;

        Add<GeneralSettingsNavigationViewModel>("General");
        Add<WidgetSettingsNavigationViewModel>("Widgets");
    }

    public IViewModelTemplate Template { get; }
}
