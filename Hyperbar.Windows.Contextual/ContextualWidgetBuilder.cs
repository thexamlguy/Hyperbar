using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetBuilder :
    IWidgetBuilder
{
    public void Create(IServiceCollection services) =>
        WidgetBuilder.Config(services, config =>
        {
            config.Id = Guid.Parse("d3030852-8d4a-4fbb-9aa5-96dff3dfa06c");
            config.Name = "Contextual commands";

            services.AddWidgetTemplate<ContextualWidgetViewModel>();
        });
}