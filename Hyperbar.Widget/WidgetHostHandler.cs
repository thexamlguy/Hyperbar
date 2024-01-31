using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHostHandler(IMediator mediator) :
    INotificationHandler<Created<IWidgetHost>>
{
    //public async Task Handle(Started<IWidgetHost> notification,
    //    CancellationToken cancellationToken)
    //{
    //    if (notification.Value is IWidgetHost host)
    //    {
    //        if (host.Services.GetService<IWidgetViewModel>() is IWidgetViewModel viewModel)
    //        {
    //            await mediator.PublishAsync(new Created<IWidgetViewModel>(viewModel),
    //                nameof(WidgetViewModel), cancellationToken);
    //        }
    //    }
    //}

    public async Task Handle(Created<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (host.Services.GetServices<IInitialization>() is
                IEnumerable<IInitialization> initializations)
            {
                foreach (IInitialization initialization in initializations)
                {
                    await initialization.InitializeAsync();
                }
            }
        }
    }
}