namespace Hyperbar.Windows;

public class SettingsButtonViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableViewModel(serviceFactory, mediator, disposer),
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
