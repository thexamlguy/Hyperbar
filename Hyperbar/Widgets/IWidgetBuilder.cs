using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetBuilder
{
    IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);

    IWidgetHost Build();
}

public interface IWidgetBuilder<TConfiguration> : 
    IWidgetBuilder
    where TConfiguration :
    WidgetConfiguration,
    new()
{

}