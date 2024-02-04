using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace Hyperbar.UI.Windows;

public class GridExtension
{
    public static readonly DependencyProperty GridColumnBindingPathProperty =
        DependencyProperty.RegisterAttached("GridColumnBindingPath", 
            typeof(string), typeof(GridExtension),
                new PropertyMetadata(null, OnGridBindingPathPropertyChanged));

    public static readonly DependencyProperty GridRowBindingPathProperty =
        DependencyProperty.RegisterAttached("GridRowBindingPath", 
            typeof(string), typeof(GridExtension),
                new PropertyMetadata(null, OnGridBindingPathPropertyChanged));

    public static string GetGridColumnBindingPath(DependencyObject dependencyObject) => 
        (string)dependencyObject.GetValue(GridColumnBindingPathProperty);

    public static string GetGridRowBindingPath(DependencyObject dependencyObject) =>
        (string)dependencyObject.GetValue(GridRowBindingPathProperty);

    public static void SetGridColumnBindingPath(DependencyObject dependencyObject, string value) =>
            dependencyObject.SetValue(GridColumnBindingPathProperty, value);
    public static void SetGridRowBindingPath(DependencyObject dependencyObject, string value) =>
        dependencyObject.SetValue(GridRowBindingPathProperty, value);

    private static void OnGridBindingPathPropertyChanged(DependencyObject dependencyObject, 
        DependencyPropertyChangedEventArgs args)
    {
        if (args.NewValue is string propertyPath)
        {
            DependencyProperty gridProperty =
                args.Property == GridColumnBindingPathProperty
                ? Grid.ColumnProperty
                : Grid.RowProperty;

            BindingOperations.SetBinding(
                dependencyObject,
                gridProperty,
                new Binding { Path = new PropertyPath(propertyPath) });
        }
    }
}
