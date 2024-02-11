namespace Hyperbar;

public interface IViewModelTemplate
{
    object Key { get; set; }

    Type TemplateType { get; set; }

    Type ViewModelType { get; set; }
}