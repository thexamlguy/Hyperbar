using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetBuilder
{
    void Create(IServiceCollection services);
}