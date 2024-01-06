namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetViewModel :
    WidgetViewModelBase
{
    public ContextualWidgetViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory) : base(templateFactory, serviceFactory)
    {
        Add<WidgetButtonViewModel>();
        Add<WidgetButtonViewModel>();
    }
}