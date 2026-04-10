using System.Windows.Forms;
using TeAtiendo.Desktop.Forms;

namespace TeAtiendo.Desktop.Helpers
{
    public static class AppBootstrapper
    {
        public static void Run()
        {
            using var login = new FrmLogin();
            if (login.ShowDialog() != DialogResult.OK) return;

            System.Windows.Forms.Application.Run(new FrmMain());
        }
    }
}