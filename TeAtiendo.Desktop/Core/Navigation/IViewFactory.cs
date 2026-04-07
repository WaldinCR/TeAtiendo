using TeAtiendo.Desktop.Core.Services;

namespace TeAtiendo.Desktop.Core.Navigation;

public interface IViewFactory
{
    ServiceFactory Services { get; }
    T Create<T>() where T : UserControl;
}