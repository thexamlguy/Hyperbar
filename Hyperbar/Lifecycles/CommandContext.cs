﻿
namespace Hyperbar;

public class CommandContext(IServiceProvider serviceProvider) :
    ICommandContext
{
    public IServiceProvider ServiceProvider => serviceProvider;
}