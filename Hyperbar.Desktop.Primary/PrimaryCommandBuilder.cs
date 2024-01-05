using Hyperbar.Extensions;
using Hyperbar.Lifecycles;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Desktop.Primary;

public class PrimaryCommandBuilder :
    ICommandBuilder
{
    public void Create(IServiceCollection services)
    {
        services.AddCommandTemplate<PrimaryCommandViewModel, PrimaryCommandView>();
    }
}

