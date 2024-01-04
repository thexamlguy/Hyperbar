using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface ICommandBuilder
{
    void Create(IServiceCollection services);
}
