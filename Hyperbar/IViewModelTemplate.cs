namespace Hyperbar;

public interface IViewModelTemplate
{
    object Key { get; set; }

    Type ViewType { get; set; }

    Type ViewModelType { get; set; }
}