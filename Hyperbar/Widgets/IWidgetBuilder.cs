using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IWidgetBuilder
{
    IWidgetHost Build();

    IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);
}

public interface IWidgetBuilder<TConfiguration> : 
    IWidgetBuilder
    where TConfiguration :
    WidgetConfiguration,
    new()
{

}