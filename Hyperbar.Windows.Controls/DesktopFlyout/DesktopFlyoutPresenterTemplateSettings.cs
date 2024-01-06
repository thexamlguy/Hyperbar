﻿using Microsoft.UI.Xaml;

namespace Hyperbar.Windows.Controls;

public class DesktopFlyoutPresenterTemplateSettings : DependencyObject
{
    public static readonly DependencyProperty HeightProperty =
        DependencyProperty.Register(nameof(Height),
            typeof(double), typeof(DesktopFlyoutPresenterTemplateSettings),
            new PropertyMetadata(0d));

    public static readonly DependencyProperty NegativeHeightProperty =
        DependencyProperty.Register(nameof(NegativeHeight),
            typeof(double), typeof(DesktopFlyoutPresenterTemplateSettings),
            new PropertyMetadata(0d));

    public static readonly DependencyProperty NegativeWidthProperty =
        DependencyProperty.Register(nameof(NegativeWidth),
            typeof(double), typeof(DesktopFlyoutPresenterTemplateSettings),
            new PropertyMetadata(0d));

    public static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register(nameof(Width),
            typeof(double), typeof(DesktopFlyoutPresenterTemplateSettings),
            new PropertyMetadata(0d));

    public double Height
    {
        get => (double)GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }
    public double NegativeHeight
    {
        get => (double)GetValue(NegativeHeightProperty);
        set => SetValue(NegativeHeightProperty, value);
    }

    public double NegativeWidth
    {
        get => (double)GetValue(NegativeWidthProperty);
        set => SetValue(NegativeWidthProperty, value);
    }

    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }
}
