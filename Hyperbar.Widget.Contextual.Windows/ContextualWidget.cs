namespace Hyperbar.Widget.Contextual.Windows;

public class ContextualWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder.Create()
            .UseConfiguration<ContextualWidgetConfiguration>(args =>
            {
                args.Name = "Contextual commands";

            })
            .UseViewModel<ContextualWidgetViewModel>()
            .ConfigureServices(args =>
            {
                args.AddWidgetTemplate<ContextualWidgetViewModel>();
            });
}