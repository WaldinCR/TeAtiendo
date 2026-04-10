namespace TeAtiendo.Desktop.Core.Ui
{
    public sealed class UiState
    {
        public bool IsBusy { get; set; }
        public string? Info { get; set; }
        public string? Error { get; set; }

        public void Clear()
        {
            IsBusy = false;
            Info = null;
            Error = null;
        }
    }
}