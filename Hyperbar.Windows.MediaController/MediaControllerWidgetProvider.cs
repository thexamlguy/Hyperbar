using Hyperbar.Windows.MediaController;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperbar.Windows.Primary;

public class MediaControllerWidgetProvider :
    IWidgetProvider
{
    public void Create(HostBuilderContext comtext, IServiceCollection services) =>
            services.AddWidgetTemplate<MediaControllerWidgetViewModel, MediaControllerWidgetView>()
                .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                .AddContentTemplate<MediaControllerViewModel, MediaControllerView>()
                .AddContentTemplate<MediaInformationViewModel, MediaInformationView>();

}