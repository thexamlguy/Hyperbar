using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public interface IWidgetBuilder
{
    IWidgetHost Build();

    IWidgetBuilder Configuration<TConfiguration>(Action<TConfiguration> configurationDelegate)
        where TConfiguration :
        WidgetConfiguration,
        new();

    IWidgetBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);

    IWidgetBuilder UseViewModel<TViewModel>()
        where TViewModel :
        IWidgetViewModel;

    IWidgetBuilder UseViewModelTemplate<TViewModel, TTemplate>()
        where TViewModel :
        IWidgetViewModel;
}