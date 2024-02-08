namespace Hyperbar.Windows;

public class GeneralSettingsNavigationViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    string text) :
    NavigationViewModel(serviceProvider, serviceFactory, mediator, disposer, text)
{
}