﻿using Hyperbar.Desktop.Contextual;
using Hyperbar.Desktop.Controls;
using Hyperbar.Desktop.Primary;
using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;

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
            .ConfigureServices(services =>
            {
                services.AddHostedService<AppService>();

                services.AddTransient<IInitializer, AppInitializer>();
                services.AddTransient<DesktopFlyout>();

                services.AddTransient<ITemplateFactory, TemplateFactory>();
                services.AddTransient<ITemplateGeneratorFactory, TemplateGeneratorFactory>();

                services.AddDataTemplate<CommandViewModel, CommandView>();

                services.AddCommandBuilder<ContextualCommandBuilder>("Contexual.Commands");
                services.AddCommandBuilder<PrimaryCommandBuilder>("Primary.Command");

                services.AddTransient(provider =>
                {
                    static IEnumerable<ICommandViewModel> Resolve(IServiceProvider services)
                    {
                        foreach (ICommandContext commandContext in services.GetServices<ICommandContext>())
                        {
                            if (commandContext.ServiceProvider.GetService<ICommandViewModel>() is 
                                ICommandViewModel commandViewModel)
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
