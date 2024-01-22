using Hyperbar.Widgets;
using Microsoft.Extensions.DependencyInjection;
using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;

public class MediaControllerWidget :
    IWidget
{
    public IWidgetBuilder Create() =>
        WidgetBuilder<MediaControllerWidgetConfiguration>.Configure(args =>
        {
            args.Name = "Media controller";
        }).ConfigureServices(args =>
        {
            args.AddWidgetTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
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