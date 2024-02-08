namespace Hyperbar.Windows;

public partial class SettingsViewModel :
    ObservableCollectionViewModel<INavigationViewModel>
{
    public SettingsViewModel(IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory, 
        IMediator mediator, 
        IDisposer disposer,
        IViewModelTemplateFactory templateFactory) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        Add<GeneralSettingsNavigationViewModel>("General");
        Add<WidgetSettingsNavigationViewModel>("Widgets");

        TemplateFactory = templateFactory;
    }

    public IViewModelTemplateFactory TemplateFactory { get; }
}
