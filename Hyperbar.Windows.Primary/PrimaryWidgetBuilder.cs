using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetBuilder :
    IWidgetBuilder
{
    public void Create(IServiceCollection services) => services.AddConfiguration<PrimaryWidgetConfiguration>()
            .AddWidgetTemplate<PrimaryWidgetViewModel>();
}

