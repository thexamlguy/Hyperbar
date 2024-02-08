namespace Hyperbar.Windows;

public class WidgetNavigationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    string text) :
    NavigationViewModel(serviceProvider, serviceFactory, mediator, disposer, text)
{
}
