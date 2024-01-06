namespace Hyperbar;

public record KeyAcceleratorCommand(string Key, 
    string[]? Modifiers = null) :
    ICommand;
