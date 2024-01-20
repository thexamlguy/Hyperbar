namespace Hyperbar.Widget.Contextual;

public class ContextualWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder.Configure<ContextualWidgetConfiguration>(args =>
        {
            args.Id = Guid.Parse("d3030852-8d4a-4fbb-9aa5-96dff3dfa06c");
            args.Name = "Contextual commands";

        }).ConfigureServices(args =>
        {
            args.AddWidgetTemplate<ContextualWidgetViewModel>();
        });
}