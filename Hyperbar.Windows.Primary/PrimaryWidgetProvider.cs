using Hyperbar.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.Primary;

public class PrimaryWidgetProvider :
    IWidgetProvider
{
    public void Create(IServiceCollection services) => services.AddConfiguration<PrimaryWidgetConfiguration>()
            .AddTransient<PrimaryWidgetViewModelFactory>()
            .AddTransient(provider => provider.GetRequiredService<PrimaryWidgetViewModelFactory>().Create())
            .AddWidgetTemplate<PrimaryWidgetViewModel>();
}