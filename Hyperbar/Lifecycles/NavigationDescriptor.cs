namespace Hyperbar;

public record NavigationDescriptor : 
    INavigationDescriptor
{
    public required Type Type { get; set; }
}

