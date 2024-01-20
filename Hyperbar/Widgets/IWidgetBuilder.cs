using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetBuilder
{
    WidgetConfiguration Configuration { get; }

    IServiceCollection Services { get; }
}