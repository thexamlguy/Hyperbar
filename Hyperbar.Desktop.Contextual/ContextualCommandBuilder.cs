using Hyperbar.Extensions;
using Hyperbar.Lifecycles;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Desktop.Contextual;

public class ContextualCommandBuilder : 
    ICommandBuilder
{
    public void Create(IServiceCollection services)
    {
        services.AddCommandTemplate<ContextualCommandViewModel, ContextualCommandView>();
    }
}
