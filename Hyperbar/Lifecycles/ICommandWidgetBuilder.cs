using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Lifecycles;

public interface ICommandWidgetBuilder
{
    void Create(IServiceCollection services);
}
