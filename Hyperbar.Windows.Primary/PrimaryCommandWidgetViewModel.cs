using Hyperbar.Lifecycles;
using Hyperbar.Templates;
using Windows.System;

namespace Hyperbar.Windows.Primary;

public class PrimaryCommandWidgetViewModel :
    ICommandWidgetViewModel,
    ITemplatedViewModel
{
    public PrimaryCommandWidgetViewModel(ITemplateFactory templateFactory, 
        IWritableConfiguration<PrimaryCommandConfiguration> configuration)
    {
        TemplateFactory = templateFactory;

        configuration.Write(args => { args.Add(new KeyAcceleratorCommand { Key = $"138" , Modifiers = [$"{VirtualKey.LeftWindows}"] }); });
        configuration.Write(args => { args.Add(new KeyAcceleratorCommand { Key = $"{VirtualKey.Tab}", Modifiers = [$"{VirtualKey.LeftWindows}"] }); });
        configuration.Write(args => { args.Add(new KeyAcceleratorCommand { Key = $"{VirtualKey.L}", Modifiers = [$"{VirtualKey.LeftWindows}", $"{VirtualKey.Control}"] }); });
    }

    public ITemplateFactory TemplateFactory { get; }
}

