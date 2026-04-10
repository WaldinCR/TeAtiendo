using System;
using System.Windows.Forms;
using TeAtiendo.Desktop.UserControls;

namespace TeAtiendo.Desktop.Core.Navigation
{
    public sealed class ViewFactory : IViewFactory
    {
        public UserControl Create(string viewKey)
        {
            return viewKey switch
            {
                "dashboard" => new UcDashboard(),
                "restaurantes" => new UcRestaurantes(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewKey), $"ViewKey desconocida: {viewKey}")
            };
        }
    }
}