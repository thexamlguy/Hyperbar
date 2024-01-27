using System.Text.Json.Serialization;

namespace Hyperbar.Widget.Primary.Windows;

[JsonDerivedType(typeof(KeyAcceleratorCommandConfiguration), typeDiscriminator: "KeyAcceleratorCommand")]
[JsonDerivedType(typeof(ProcessCommandConfiguration), typeDiscriminator: "ProcessCommand")]
public class PrimaryCommandConfiguration
{
    public List<PrimaryCommandConfiguration> Commands { get; set; } = [];

    public required string Icon { get; set; }

    public required Guid Id { get; set; }

    public required int Order { get; set; }

    public required string Text { get; set; }
}