using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetBuilder
{
    IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);

    IWidgetHost Build();
}