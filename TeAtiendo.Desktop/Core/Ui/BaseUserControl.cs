using System.Windows.Forms;

namespace TeAtiendo.Desktop.Core.Ui
{
    public class BaseUserControl : UserControl
    {
        protected UiState State { get; } = new UiState();

        protected void SetBusy(bool busy)
        {
            State.IsBusy = busy;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
            Enabled = !busy;
        }
    }
}