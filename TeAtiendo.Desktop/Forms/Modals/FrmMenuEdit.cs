using System;
using System.Windows.Forms;
using TeAtiendo.Application.DTOs.Menu;
using TeAtiendo.Desktop.Helpers;

namespace TeAtiendo.Desktop.Forms.Modals
{
    public partial class FrmMenuEdit : Form
    {
        public CreateMenuDto ResultCreate { get; private set; } = new() { Nombre = "", RestauranteId = Guid.Empty };
        public UpdateMenuDto ResultUpdate { get; private set; } = new();

        private readonly Guid _restauranteId;
        private readonly MenuDto? _existing;

        public FrmMenuEdit(Guid restauranteId, MenuDto? existing = null)
        {
            _restauranteId = restauranteId;
            _existing = existing;

            InitializeComponent();
            ApplyTheme();
            LoadExistingIfAny();
            WireEvents();
        }

        private void ApplyTheme()
        {
            ThemeHelper.EstilizarFormulario(this);

            lblTitle.Font = ThemeHelper.FontSubtitle;
            lblTitle.ForeColor = ThemeHelper.TextPrimary;

            ThemeHelper.EstilizarBotonPrimario(btnGuardar);
            ThemeHelper.EstilizarBotonSecundario(btnCancelar);
        }

        private void LoadExistingIfAny()
        {
            Text = _existing is null ? "Crear Menú" : "Editar Menú";
            lblTitle.Text = _existing is null ? "Crear Menú" : "Editar Menú";

            if (_existing != null)
            {
                txtNombre.Text = _existing.Nombre;
                txtDescripcion.Text = _existing.Descripcion ?? "";
            }
        }

        private void WireEvents()
        {
            btnCancelar.Click += (_, __) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            btnGuardar.Click += (_, __) =>
            {
                if (!ValidateForm()) return;

                if (_existing is null)
                {
                    ResultCreate = new CreateMenuDto
                    {
                        RestauranteId = _restauranteId,
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim()
                    };
                }
                else
                {
                    ResultUpdate = new UpdateMenuDto
                    {
                        RestauranteId = _restauranteId,
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim()
                    };
                }

                DialogResult = DialogResult.OK;
                Close();
            };
        }

        private bool ValidateForm()
        {
            errorProvider1.Clear();

            if (_restauranteId == Guid.Empty)
            {
                MessageBox.Show("RestauranteId inválido. Selecciona un restaurante.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "El nombre es requerido.");
                return false;
            }

            if (txtNombre.Text.Trim().Length < 3)
            {
                errorProvider1.SetError(txtNombre, "Mínimo 3 caracteres.");
                return false;
            }

            return true;
        }
    }
}