using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetViewModelEnumerator(IWidgetHost host, 
    IPublisher publisher) :
    INotificationHandler<Enumerate<IWidgetViewModel>>
{
    public async Task Handle(Enumerate<IWidgetViewModel> notification,
        CancellationToken cancellationToken)
    {
        if (host.Services.GetServices<IWidgetViewModel>() is IEnumerable<IWidgetViewModel> viewModels)
        {
            foreach (IWidgetViewModel viewModel in viewModels)
            {
                await publisher.PublishAsync(new Create<IWidgetViewModel>(viewModel),
                    nameof(IWidgetHostViewModel), cancellationToken);
            }
        }
    }   
}