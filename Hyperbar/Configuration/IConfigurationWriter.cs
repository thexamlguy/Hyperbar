
namespace Hyperbar.Options
{
    public interface IConfigurationWriter<TConfiguration>
        where TConfiguration : 
        class, new()
    {
        void Write(Action<TConfiguration?>? updateDelegate = null);

        void Write(TConfiguration? value);
    }
}