namespace Hyperbar.Windows;

public class WidgetSettingsNavigationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    string text) :
    NavigationViewModel<WidgetNavigationViewModel>(serviceProvider, serviceFactory, mediator, disposer, text)
{

}
