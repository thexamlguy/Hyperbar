﻿using Hyperbar.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows;

public class AppInitializer([FromKeyedServices(nameof(CommandViewModel))] CommandView view,
    [FromKeyedServices(nameof(CommandViewModel))] CommandViewModel viewModel,
    DesktopFlyout desktopFlyout) :
    IInitializer
{
    public Task InitializeAsync()
    {
        view.DataContext = viewModel;

        desktopFlyout.Placement = DesktopFlyoutPlacement.Top;
        desktopFlyout.Content = view;

        return Task.CompletedTask;
    }
}