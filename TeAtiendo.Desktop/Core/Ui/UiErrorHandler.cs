using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.Core.Ui;

public static class UiErrorHandler
{
    public static void Show(Exception ex, string title = "Error")
    {
        var msg = ex is ApiException ? ex.Message : "Ocurrió un error inesperado.";
        MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}