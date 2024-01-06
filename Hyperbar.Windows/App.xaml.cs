using Hyperbar.Windows.Contextual;
using Hyperbar.Windows.Controls;
using Hyperbar.Windows.Primary;
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

                services.AddDataTemplate<CommandViewModel, CommandView>();

                services.AddCommand<ContextualCommandWidgetBuilder>("");
                services.AddCommand<PrimaryCommandWidgetBuilder>("");

                services.AddTransient(provider =>
                {
                    static IEnumerable<ICommandWidgetViewModel> Resolve(IServiceProvider services)
                    {
                        foreach (ICommandWidgetContext commandContext in services.GetServices<ICommandWidgetContext>())
                        {
                            if (commandContext.ServiceProvider.GetService<ICommandWidgetViewModel>() is 
                                ICommandWidgetViewModel commandViewModel)
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
