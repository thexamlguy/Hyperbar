using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Lifecycles;

public interface IWidgetBuilder
{
    void Create(IServiceCollection services);
}
