using System.Text.Json.Serialization;

namespace Hyperbar.Windows.Primary;

[JsonDerivedType(typeof(KeyAcceleratorCommandConfiguration), typeDiscriminator: "KeyAcceleratorCommand")]
[JsonDerivedType(typeof(ProcessCommandConfiguration), typeDiscriminator: "ProcessCommand")]
public class PrimaryCommandConfiguration
{
    public string? Icon { get; set; }
}