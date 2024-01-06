using Hyperbar.Lifecycles;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Primary;

public class PrimaryCommandWidgetBuilder :
    ICommandWidgetBuilder
{
    public void Create(IServiceCollection services)
    {
        services.AddWritableConfiguration<PrimaryCommandConfiguration>()
            .AddCommandTemplate<PrimaryCommandWidgetViewModel, PrimaryCommandWidgetView>();
    }
}

