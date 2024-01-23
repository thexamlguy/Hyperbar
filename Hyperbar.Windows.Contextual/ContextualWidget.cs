namespace Hyperbar.Widget.Contextual;

public class ContextualWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder<ContextualWidgetConfiguration>.Configure(args =>
        {
            args.Name = "Contextual commands";

        }).ConfigureServices(args =>
        {
            args.AddWidgetTemplate<ContextualWidgetViewModel>();
        });
}