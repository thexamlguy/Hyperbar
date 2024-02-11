using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class NavigateHandler(IServiceProvider provider,
    IPublisher publisher,
    INavigationProvider navigationProvider,
    IViewModelTemplateProvider viewModelTemplateProvider,
    INavigationTargetProvider navigationTargetProvider) :
    INotificationHandler<Navigate>
{
    public async Task Handle(Navigate args, 
        CancellationToken cancellationToken)
    {

        if (viewModelTemplateProvider.Get(args.Name)
            is IViewModelTemplate viewModelTemplate)
        {
            if (provider.GetRequiredKeyedService(viewModelTemplate.TemplateType,
                viewModelTemplate.Key) is object view &&
                provider.GetRequiredKeyedService(viewModelTemplate.ViewModelType,
                viewModelTemplate.Key) is object viewModel)
            {
                if ((args.TargetName is not null ?
                    navigationTargetProvider.Get(args.TargetName) : view) is object target)
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

