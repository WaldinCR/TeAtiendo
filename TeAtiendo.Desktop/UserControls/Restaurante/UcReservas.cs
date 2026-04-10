using System;
using System.Windows.Forms;
using TeAtiendo.Desktop.Helpers;

namespace TeAtiendo.Desktop.UserControls
{
    public partial class UcReservas : UserControl
    {
        private DataGridView dgv = null!;
        private Button btnConfirmar = null!;
        private Button btnRechazar = null!;

        public UcReservas()
        {
            InitializeComponent();
            BuildUI();
            CargarMock();
        }

        private void BuildUI()
        {
            Dock = DockStyle.Fill;
            BackColor = ThemeHelper.BgMain;

            var pnlTop = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10),
                WrapContents = false
            };

            btnConfirmar = new Button { Text = "Confirmar", Width = 120, Height = 40 };
            btnRechazar = new Button { Text = "Rechazar", Width = 120, Height = 40 };
            ThemeHelper.EstilizarBotonPrimario(btnConfirmar);
            ThemeHelper.EstilizarBotonPeligro(btnRechazar);

            pnlTop.Controls.AddRange(new Control[] { btnConfirmar, btnRechazar });

            dgv = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
            ThemeHelper.EstilizarDataGridView(dgv);

            Controls.Add(dgv);
            Controls.Add(pnlTop);
        }

        private void CargarMock()
        {
            dgv.DataSource = new[]
            {
                new { Cliente = "Juan", Fecha = DateTime.Now.AddHours(2), Personas = 2, Estado = "Pendiente" },
                new { Cliente = "Ana",  Fecha = DateTime.Now.AddDays(1),  Personas = 4, Estado = "Confirmada" },
            };
        }
    }
}