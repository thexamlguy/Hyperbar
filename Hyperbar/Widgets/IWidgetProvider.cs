using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar;

public interface IWidgetProvider
{
    void Create(HostBuilderContext context, IServiceCollection services);
}