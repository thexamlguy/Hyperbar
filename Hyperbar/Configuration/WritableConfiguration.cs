namespace Hyperbar;

public class WritableConfiguration<TConfiguration>(IConfigurationWriter<TConfiguration> writer) :
    IWritableConfiguration<TConfiguration>
    where TConfiguration :
    class, new()
{
    public void Write(Action<TConfiguration> updateDelegate) => writer.Write(updateDelegate);
}
