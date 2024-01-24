using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

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