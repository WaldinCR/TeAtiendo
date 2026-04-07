using TeAtiendo.Desktop.Core.Services;
using TeAtiendo.Desktop.UserControls;

namespace TeAtiendo.Desktop.Core.Navigation;

public sealed class ViewFactory : IViewFactory
{
    public ServiceFactory Services { get; }

    public ViewFactory(ServiceFactory services)
    {
        Services = services;
    }

    public T Create<T>() where T : UserControl
    {
        return Activator.CreateInstance<T>();
    }
}