using Microsoft.Extensions.DependencyInjection;
using Windows.Media.Control;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder.Create()
            .Configuration<MediaControllerWidgetConfiguration>(args =>
            {
                args.Name = "Media controller";
            })
            .UseViewModelTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
            .ConfigureServices(args =>
            {
                args.AddSingleton<IInitializer, MediaControllerManager>()
                    .AddTransient<IServiceScopeFactory<MediaController>, ServiceScopeFactory<MediaController>>()
                    .AddTransient<IServiceScopeProvider<MediaController>, ServiceScopeProvider<MediaController>>()
                    .AddCache<MediaController, IServiceScope>()
                    .AddTransient<IFactory<GlobalSystemMediaTransportControlsSession, MediaController?>, MediaControllerFactory>()
                    .AddHandler<MediaControllerHandler>()
                    .AddTransient<IFactory<MediaController, MediaControllerViewModel?>, MediaControllerViewModelFactory>()
                    .AddCache<MediaController, MediaControllerViewModel>()
                    .AddHandler<MediaControllerPlaybackStatusHandler>()
                    .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                    .AddContentTemplate<MediaInformationViewModel, MediaInformationView>()
                    .AddContentTemplate<MediaButtonViewModel, MediaButtonView>();
            });
}