namespace Hyperbar;

public class DefaultConfiguration<TConfiguration>(TConfiguration? configuration = null) 
    where TConfiguration :
    class
{
    public TConfiguration? Configuration => configuration;
}
