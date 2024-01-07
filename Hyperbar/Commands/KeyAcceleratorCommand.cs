namespace Hyperbar;

public record KeyAcceleratorCommand(VirtualKey Key,
    VirtualKey[]? Modifiers = null) :
    IRequest;
