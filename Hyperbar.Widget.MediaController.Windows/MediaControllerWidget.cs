using Microsoft.Extensions.DependencyInjection;
using Windows.Media.Control;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder.Create()
            .UseConfiguration<MediaControllerWidgetConfiguration>(args =>
            {
                args.Name = "Media controller";
            })
            .UseViewModelTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
            .ConfigureServices(args =>
            {
                args.AddHostedService<MediaControllerService>()
                    .AddTransient<IServiceScopeFactory<MediaController>, ServiceScopeFactory<MediaController>>()
                    .AddTransient<IServiceScopeProvider<MediaController>, ServiceScopeProvider<MediaController>>()
                    .AddCache<MediaController, IServiceScope>()
                    .AddTransient<IFactory<GlobalSystemMediaTransportControlsSession, MediaController?>, MediaControllerFactory>()
                    .AddHandler<MediaControllerHandler>()
                    .AddTransient<IFactory<MediaController, MediaControllerViewModel?>, MediaControllerViewModelFactory>()
                    .AddCache<MediaController, MediaControllerViewModel>()
                    .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                    .AddContentTemplate<MediaInformationViewModel, MediaInformationView>()
                    .AddContentTemplate<MediaButtonViewModel<MediaPreviousButton>, MediaButtonView>()
                    .AddContentTemplate<MediaButtonViewModel<MediaPlayPauseButton>, MediaButtonView>()
                    .AddContentTemplate<MediaButtonViewModel<MediaNextButton>, MediaButtonView>();
            });
}