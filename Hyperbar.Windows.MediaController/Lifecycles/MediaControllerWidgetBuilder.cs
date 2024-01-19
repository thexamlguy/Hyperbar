using Microsoft.Extensions.DependencyInjection;
using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;

public class MediaControllerWidgetBuilder :
    IWidgetBuilder
{
    public void Create(IServiceCollection services) =>
            WidgetBuilder.Config(services, config =>
            {
                config.Id = Guid.Parse("1667a800-ec5a-4d39-aa75-4f5ee95bb9f1");
                config.Name = "Media controller";

                services.AddWidgetTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
                    .AddSingleton<IInitializer, MediaControllerManager>()
                    .AddTransient<IServiceScopeFactory<MediaController>, ServiceScopeFactory<MediaController>>()
                    .AddTransient<IServiceScopeProvider<MediaController>, ServiceScopeProvider<MediaController>>()
                    .AddCache<MediaController, IServiceScope>()
                    .AddTransient<IFactory<GlobalSystemMediaTransportControlsSession, MediaController?>, MediaControllerFactory>()
                    .AddHandler<MediaControllerHandler>()
                    .AddTransient<IFactory<MediaController, MediaControllerViewModel?>, MediaControllerViewModelFactory>()
                    .AddCache<MediaController, MediaControllerViewModel>()
                    .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                    .AddContentTemplate<MediaInformationViewModel, MediaInformationView>()
                    .AddContentTemplate<MediaButtonViewModel, MediaButtonView>();
            });
}