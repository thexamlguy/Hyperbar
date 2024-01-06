using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetProvider
{
    void Create(IServiceCollection services);
}