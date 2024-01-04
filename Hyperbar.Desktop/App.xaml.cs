using Hyperbar.Desktop.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;

namespace Hyperbar.Desktop;

public partial class App : 
    Application
{
    public App() => InitializeComponent();

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        IHost? host = Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices(args => 
            {
                args.AddTransient<IInitializer, AppInitializer>();
                args.AddTransient<DesktopFlyout>();

                args.AddTransient<ITemplateFactory, TemplateFactory>();
                args.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                args.AddDataTemplate<CommandViewModel, CommandView>("Commands");
                args.AddDataTemplate<ContextualCommandViewModel, ContextualCommandView>();

                args.AddHostedService<AppService>();
            })
            .Build();
        
        await host.RunAsync();
    }
}
