using System;
using System.Windows.Forms;

namespace TeAtiendo.Desktop.UserControls.Restaurante
{
    public partial class UcMesas : UserControl
    {
        private readonly BindingSource bs = new();
        private DataGridView dgv = null!;
        private Button btnCrear = null!;
        private Button btnEditar = null!;
        private Button btnEliminar = null!;

        public UcMesas()
        {
            InitializeComponent();
            BuildUI();
            LoadMock();
        }

        private void BuildUI()
        {
            Dock = DockStyle.Fill;

            var pnlActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(10),
                WrapContents = false
            };

            btnCrear = new Button { Text = "Crear" };
            btnEditar = new Button { Text = "Editar" };
            btnEliminar = new Button { Text = "Eliminar" };

            pnlActions.Controls.AddRange(new Control[] { btnCrear, btnEditar, btnEliminar });

            dgv = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
            dgv.DataSource = bs;

            Controls.Add(dgv);
            Controls.Add(pnlActions);

            btnCrear.Click += (_, __) =>
            {
                MessageBox.Show("Aquí abrirías el formulario/modal de Mesa (pendiente).");
            };
        }

        private void LoadMock()
        {
            bs.DataSource = new[]
            {
                new { Numero = 1, Capacidad = 4, Disponible = true },
                new { Numero = 2, Capacidad = 2, Disponible = false }
            };
        }
    }
}