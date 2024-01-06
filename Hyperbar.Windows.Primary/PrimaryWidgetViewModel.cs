namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel :
    WidgetViewModelBase
{
    public PrimaryWidgetViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IMediator mediator) : base(templateFactory, serviceFactory)
    {
        Add<WidgetButtonViewModel>("Start", new Action(() => mediator.Send(new KeyAcceleratorCommand(VirtualKey.LeftWindows))));

        //Add<WidgetButtonViewModel>("test 2", new Action(() => { }));
        //Add<WidgetButtonViewModel>("test 4", new Action(() => { }));
        //Add<WidgetButtonViewModel>("test 5", new Action(() => { }));
    }
}