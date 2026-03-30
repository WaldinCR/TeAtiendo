namespace TeAtiendo.Web.Services;


public class ToastService
{
    public event Action<string, string>? OnShow;

    public void Show(string message, string type = "info")
    {
        OnShow?.Invoke(message, type);
    }

    public void Success(string message) => Show(message, "success");
    public void Error(string message) => Show(message, "error");
    public void Warning(string message) => Show(message, "warning");
    public void Info(string message) => Show(message, "info");
}
