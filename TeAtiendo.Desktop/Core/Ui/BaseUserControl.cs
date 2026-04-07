using TeAtiendo.Desktop.Core.Services;

namespace TeAtiendo.Desktop.Core.Ui;

public abstract class BaseUserControl : UserControl
{
    protected ServiceFactory Services { get; }
    protected UiState State { get; } = new();

    protected BaseUserControl(ServiceFactory services)
    {
        Services = services;
        this.Dock = DockStyle.Fill;
    }

    protected async Task RunSafeAsync(Func<Task> action, bool showError = true)
    {
        try
        {
            State.SetLoading(true);
            await action();
        }
        catch (Exception ex)
        {
            State.SetError(ex.Message);
            if (showError) UiErrorHandler.Show(ex);
        }
        finally
        {
            State.SetLoading(false);
        }
    }
}