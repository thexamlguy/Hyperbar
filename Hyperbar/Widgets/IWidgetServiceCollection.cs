using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetServiceCollection
{
    IServiceCollection Services { get; }
}
