using TeAtiendo.Desktop.Helpers;

namespace TeAtiendo.Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            ThemeHelper.ApplyTheme();
            AppBootstrapper.Run();
        }
    }
}