using System.Windows.Forms;

namespace TeAtiendo.Desktop.Core.Navigation
{
    public interface IViewFactory
    {
        UserControl Create(string viewKey);
    }
}