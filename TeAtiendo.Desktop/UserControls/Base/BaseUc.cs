using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.UserControls.Base;

public abstract class BaseUc : UserControl
{
    protected async Task RunSafeAsync(Func<Task> action, bool showError = true)
    {
        try
        {
            UseWaitCursor = true;
            Enabled = false;
            await action();
        }
        catch (ApiException ex)
        {
            if (showError)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            if (showError)
                MessageBox.Show("La solicitud tardó demasiado (timeout). Intenta de nuevo.", "Timeout",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            if (showError)
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Enabled = true;
            UseWaitCursor = false;
        }
    }
    protected async Task<T?> RunSafeAsync<T>(Func<Task<T?>> action, bool showError = true)
    {
        try
        {
            UseWaitCursor = true;
            Enabled = false;
            return await action();
        }
        catch (ApiException ex)
        {
            if (showError)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return default;
        }
        catch (TaskCanceledException)
        {
            if (showError)
                MessageBox.Show("La solicitud tardó demasiado (timeout). Intenta de nuevo.", "Timeout",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return default;
        }
        catch (Exception ex)
        {
            if (showError)
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            return default;
        }
        finally
        {
            Enabled = true;
            UseWaitCursor = false;
        }
    }
}