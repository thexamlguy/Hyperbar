﻿using Hyperbar.Desktop.Contextual;
using Hyperbar.Desktop.Controls;
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

                services.AddDataTemplate<CommandViewModel, CommandView>("Commands");

                // Commands
                services.AddSomething<ContextualCommandBuilder>();
 
                services.AddTransient(provider =>
                {
                    IEnumerable<ICommandViewModel> Resolve(IServiceProvider services)
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