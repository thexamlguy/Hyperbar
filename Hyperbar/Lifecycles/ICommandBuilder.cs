using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Lifecycles;

public interface ICommandBuilder
{
    void Create(IServiceCollection services);
}
