using System.ComponentModel;
using System.Windows.Forms;
using TeAtiendo.Desktop.Core.Services;
using TeAtiendo.Desktop.Core.Ui;
using TeAtiendo.Desktop.Helpers;
using TeAtiendo.Desktop.Models.Responses;
using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.UserControls
{
    public partial class UcRestaurantes : UserControl
    {
        private readonly RestauranteService _service;

        private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        private readonly BindingList<RestauranteResponse> _items = new();

        private RestauranteResponse? _current;

        private readonly TextBox _txtNombre = new() { PlaceholderText = "Nombre" };
        private readonly TextBox _txtDireccion = new() { PlaceholderText = "Dirección" };
        private readonly TextBox _txtTelefono = new() { PlaceholderText = "Teléfono" };
        private readonly TextBox _txtCorreo = new() { PlaceholderText = "Correo" };

        private readonly DateTimePicker _dtApertura = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        private readonly DateTimePicker _dtCierre = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };

        private readonly Button _btnLoad = new() { Text = "Cargar" };
        private readonly Button _btnNew = new() { Text = "Nuevo" };
        private readonly Button _btnSave = new() { Text = "Guardar" };
        private readonly Button _btnDelete = new() { Text = "Eliminar" };

        public UcRestaurantes()
        {
            var factory = new ServiceFactory();
            _service = factory.RestauranteService();

            _grid.DataSource = _items;
            _grid.SelectionChanged += (_, __) =>
            {
                if (_grid.CurrentRow?.DataBoundItem is RestauranteResponse r)
                {
                    _current = r;
                    BindToForm();

                    SessionManager.SetRestauranteActual(r.Id, r.Nombre ?? "");
                }
            };

            _btnLoad.Click += async (_, __) => await LoadAll();
            _btnNew.Click += (_, __) => NewItem();
            _btnSave.Click += async (_, __) => await Save();
            _btnDelete.Click += async (_, __) => await Delete();

            var form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 140,
                ColumnCount = 4,
                RowCount = 3
            };
            form.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++) form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            form.Controls.Add(_txtNombre, 0, 0);
            form.Controls.Add(_txtDireccion, 1, 0);
            form.Controls.Add(_txtTelefono, 2, 0);
            form.Controls.Add(_txtCorreo, 3, 0);

            form.Controls.Add(new Label { Text = "Apertura", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 0, 1);
            form.Controls.Add(_dtApertura, 1, 1);
            form.Controls.Add(new Label { Text = "Cierre", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 2, 1);
            form.Controls.Add(_dtCierre, 3, 1);

            var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };
            buttons.Controls.AddRange(new Control[] { _btnLoad, _btnNew, _btnSave, _btnDelete });

            Controls.Add(_grid);
            Controls.Add(buttons);
            Controls.Add(form);
        }

        private async Task LoadAll()
        {
            try
            {
                _items.Clear();
                var data = await _service.GetAllAsync();
                foreach (var r in data) _items.Add(r);
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewItem()
        {
            _current = new RestauranteResponse
            {
                Id = Guid.Empty,
                HorarioApertura = new TimeOnly(8, 0),
                HorarioCierre = new TimeOnly(18, 0)
            };

            SessionManager.ClearRestauranteActual();
            BindToForm();
        }

        private async Task Save()
        {
            if (_current is null) return;

            ReadFromForm(_current);

            if (!ValidationHelper.TryValidate(_current, out var msg))
            {
                MessageBox.Show(msg, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_current.HorarioApertura >= _current.HorarioCierre)
            {
                MessageBox.Show("El horario de apertura debe ser menor al horario de cierre.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_current.Id == Guid.Empty)
                {
                    var created = await _service.CreateAsync(_current);
                    if (created != null) _items.Insert(0, created);
                }
                else
                {
                    var updated = await _service.UpdateAsync(_current.Id, _current);
                    if (updated != null) Replace(updated);
                }
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Delete()
        {
            if (_current is null || _current.Id == Guid.Empty) return;

            var userId = SessionManager.UsuarioActual?.Id ?? Guid.Empty;
            if (userId == Guid.Empty)
            {
                MessageBox.Show("No hay usuario en sesión.", "Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Eliminar restaurante?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                await _service.DeleteAsync(_current.Id, userId);
                var toRemove = _items.FirstOrDefault(x => x.Id == _current.Id);
                if (toRemove != null) _items.Remove(toRemove);
                _current = null;
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "No se pudo eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindToForm()
        {
            if (_current is null) return;

            _txtNombre.Text = _current.Nombre ?? "";
            _txtDireccion.Text = _current.Direccion ?? "";
            _txtTelefono.Text = _current.Telefono ?? "";
            _txtCorreo.Text = _current.Correo ?? "";

            _dtApertura.Value = DateTime.Today.Add(_current.HorarioApertura.ToTimeSpan());
            _dtCierre.Value = DateTime.Today.Add(_current.HorarioCierre.ToTimeSpan());
        }

        private void ReadFromForm(RestauranteResponse r)
        {
            r.Nombre = _txtNombre.Text.Trim();
            r.Direccion = _txtDireccion.Text.Trim();
            r.Telefono = _txtTelefono.Text.Trim();
            r.Correo = _txtCorreo.Text.Trim();
            r.HorarioApertura = TimeOnly.FromDateTime(_dtApertura.Value);
            r.HorarioCierre = TimeOnly.FromDateTime(_dtCierre.Value);
        }

        private void Replace(RestauranteResponse updated)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == updated.Id)
                {
                    _items[i] = updated;
                    _grid.Refresh();
                    return;
                }
            }
        }
    }
}