using Hyperbar.Windows.Controls;
using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace Hyperbar.Windows;

public partial class App : 
    Application
{
    public App() => InitializeComponent();

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        IHost? host = Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("Settings.json", true);

                config.Build();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<AppService>();

                services.AddTransient<IInitializer, AppInitializer>();
                services.AddTransient<DesktopFlyout>();

                services.AddTransient<ITemplateFactory, TemplateFactory>();
                services.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                services.AddContentTemplate<CommandViewModel, CommandView>();

                //services.AddCommand<ContextualCommandWidgetBuilder>("");
                //services.AddWidget<PrimaryCommandWidgetBuilder>("");

                services.AddTransient(provider =>
                {
                    static IEnumerable<IWidgetViewModel> Resolve(IServiceProvider services)
                    {
                        foreach (IWidgetContext commandContext in services.GetServices<IWidgetContext>())
                        {
                            if (commandContext.ServiceProvider.GetService<IWidgetViewModel>() is 
                                IWidgetViewModel commandViewModel)
                            {
                                yield return commandViewModel;
                            }
                        }
                    }

                    return Resolve(provider);                
                });
            })
            .Build();

        await host.RunAsync();
    }
}
