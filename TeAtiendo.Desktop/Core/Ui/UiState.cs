namespace TeAtiendo.Desktop.Core.Ui;

public sealed class UiState
{
    public bool IsLoading { get; private set; }
    public string? ErrorMessage { get; private set; }

    public void SetLoading(bool value)
    {
        IsLoading = value;
        if (value) ErrorMessage = null;
    }

    public void SetError(string message)
    {
        IsLoading = false;
        ErrorMessage = message;
    }

    public void ClearError() => ErrorMessage = null;
}