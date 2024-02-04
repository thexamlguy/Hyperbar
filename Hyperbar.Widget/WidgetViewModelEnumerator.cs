using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetViewModelEnumerator(IWidgetHost host, 
    IMediator mediator) :
    INotificationHandler<Enumerate<IWidgetViewModel>>
{
    public async Task Handle(Enumerate<IWidgetViewModel> notification,
        CancellationToken cancellationToken)
    {
        if (host.Services.GetServices<IWidgetViewModel>() is IEnumerable<IWidgetViewModel> viewModels)
        {
            foreach (IWidgetViewModel viewModel in viewModels)
            {
                await mediator.PublishAsync(new Create<IWidgetViewModel>(viewModel),
                    nameof(IWidgetHostViewModel), cancellationToken);
            }
        }
    }   
}