namespace Hyperbar;

public record KeyAccelerator(VirtualKey Key,
    VirtualKey[]? Modifiers = null) :
    IRequest;
