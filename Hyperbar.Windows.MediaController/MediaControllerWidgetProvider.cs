using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;

public class MediaControllerWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) =>
            services.AddWidgetTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
                .AddSingleton<IInitializer, MediaControllerManager>()
                .AddTransient<IServiceScopeFactory<MediaController>, ServiceScopeFactory<MediaController>>()
                .AddTransient<IServiceScopeProvider<MediaController>, ServiceScopeProvider<MediaController>>()
                .AddCache<MediaController, IServiceScope>()
                .AddTransient<IFactory<GlobalSystemMediaTransportControlsSession, MediaController?>, MediaControllerFactory>()
                .AddHandler<MediaControllerHandler>()
                .AddTransient<IFactory<MediaControllerViewModel?>, MediaControllerViewModelFactory>()
                .AddCache<MediaControllerViewModel>()
                .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                .AddContentTemplate<MediaInformationViewModel, MediaInformationView>();
}