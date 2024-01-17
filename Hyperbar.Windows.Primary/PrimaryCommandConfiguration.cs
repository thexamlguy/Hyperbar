using System.Text.Json.Serialization;

namespace Hyperbar.Windows.Primary;

[JsonDerivedType(typeof(KeyAcceleratorCommandConfiguration), typeDiscriminator: "KeyAcceleratorCommand")]
[JsonDerivedType(typeof(ProcessCommandConfiguration), typeDiscriminator: "ProcessCommand")]
public class PrimaryCommandConfiguration
{
    public required Guid Id { get; set; }

    public required string Icon { get; set; }

    public required string Text { get; set; }

    public List<PrimaryCommandConfiguration> Commands { get; set; } = [];
}