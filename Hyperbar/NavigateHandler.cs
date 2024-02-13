using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class NavigateHandler(IServiceProvider provider,
    IPublisher publisher,
    INavigationProvider navigationProvider,
    IViewModelTemplateProvider viewModelTemplateProvider,
    INavigationTargetProvider navigationTargetProvider,
    IProxyService<INavigationTargetProvider> proxyNavigationTargetProvider) :
    INotificationHandler<Navigate>
{
    public async Task Handle(Navigate args, 
        CancellationToken cancellationToken)
    {
        if (viewModelTemplateProvider.Get(args.Name)
            is IViewModelTemplate viewModelTemplate)
        {
            if (provider.GetRequiredKeyedService(viewModelTemplate.ViewType,
                viewModelTemplate.Key) is object view &&
                provider.GetRequiredKeyedService(viewModelTemplate.ViewModelType,
                viewModelTemplate.Key) is object viewModel)
            {
                object? target = args.TargetName is not null
                    ? navigationTargetProvider.TryGet(args.TargetName, out target) || proxyNavigationTargetProvider.Proxy.TryGet(args.TargetName, out target)
                        ? target
                        : null
                    : view;

                if (target is not null)
                {
                    if (navigationProvider.Get(target.GetType()) 
                        is INavigation navigation)
                    {
                        Type navigateType = typeof(Navigate<>).MakeGenericType(navigation.Type);
                        if (Activator.CreateInstance(navigateType, [target, view, viewModel]) is object navigate)
                        {
                            await publisher.PublishAsync(navigate, cancellationToken);
                        }
                    }
                }
            }
        }
    }
}

