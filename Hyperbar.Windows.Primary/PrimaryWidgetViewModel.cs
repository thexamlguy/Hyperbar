namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetViewModel :
    WidgetViewModelBase
{
    public PrimaryWidgetViewModel(ITemplateFactory templateFactory, 
        IServiceFactory serviceFactory) : base(templateFactory, serviceFactory)
    {
        ;

        Add<WidgetButtonViewModel>("test 1", new Action(() => { 
        
        }));
        Add<WidgetButtonViewModel>("test 2", new Action(() => { }));
        Add<WidgetButtonViewModel>("test 4", new Action(() => { }));
        Add<WidgetButtonViewModel>("test 5", new Action(() => { }));
    }
}

