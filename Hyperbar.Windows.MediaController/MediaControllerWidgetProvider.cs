using Hyperbar.Windows.MediaController;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows.Primary;

public class MediaControllerWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) =>
            services.AddWidgetTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
                .AddSingleton<Queue<MediaController>>()
                .AddSingleton<IInitializer, MediaControllerManager>()
                .AddContentTemplate<MediaControllerViewModel, MediaControllerView>();

}