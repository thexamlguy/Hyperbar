namespace Hyperbar;

public record KeyAcceleratorRequest(VirtualKey Key,
    VirtualKey[]? Modifiers = null) :
    IRequest;
