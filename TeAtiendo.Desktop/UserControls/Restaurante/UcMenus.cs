using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeAtiendo.Application.DTOs.Menu;
using TeAtiendo.Desktop.Core.Services;
using TeAtiendo.Desktop.Core.Ui;
using TeAtiendo.Desktop.Forms.Modals;
using TeAtiendo.Desktop.Helpers;
using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.UserControls
{
    public partial class UcMenus : UserControl
    {
        private readonly MenuService _service;

        private readonly BindingList<MenuDto> _items = new();
        private readonly BindingSource _bs = new();

        private readonly DataGridView _grid = new() { Dock = DockStyle.Fill, AutoGenerateColumns = false };
        private readonly TextBox _txtBuscar = new() { PlaceholderText = "Buscar por nombre..." };

        private readonly Button _btnLoad = new() { Text = "Cargar" };
        private readonly Button _btnCrear = new() { Text = "Crear" };
        private readonly Button _btnEditar = new() { Text = "Editar" };
        private readonly Button _btnEliminar = new() { Text = "Eliminar" };

        public UcMenus()
        {
            var factory = new ServiceFactory();
            _service = factory.MenuService();

            BuildUi();
        }

        private void BuildUi()
        {
            Dock = DockStyle.Fill;

            _bs.DataSource = _items;
            _grid.DataSource = _bs;

            ThemeHelper.EstilizarDataGridView(_grid);
            ThemeHelper.EstilizarBotonPrimario(_btnCrear);
            ThemeHelper.EstilizarBotonSecundario(_btnEditar);
            ThemeHelper.EstilizarBotonPeligro(_btnEliminar);
            ThemeHelper.EstilizarBotonSecundario(_btnLoad);

            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "RestauranteId",
                DataPropertyName = "RestauranteId",
                Width = 240
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Creación",
                DataPropertyName = "FechaCreacion",
                Width = 160
            });

            var top = new TableLayoutPanel { Dock = DockStyle.Top, Height = 48, ColumnCount = 2 };
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420));
            top.Controls.Add(_txtBuscar, 0, 0);

            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
            actions.Controls.AddRange(new Control[] { _btnEliminar, _btnEditar, _btnCrear, _btnLoad });
            top.Controls.Add(actions, 1, 0);

            Controls.Add(_grid);
            Controls.Add(top);

            _btnLoad.Click += async (_, __) => await LoadAll();
            _btnCrear.Click += async (_, __) => await Crear();
            _btnEditar.Click += async (_, __) => await Editar();
            _btnEliminar.Click += async (_, __) => await Eliminar();

            _txtBuscar.TextChanged += (_, __) => ApplyFilter();

            _ = LoadAll();
        }

        private async Task LoadAll()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _items.Clear();
                var data = await _service.ObtenerTodosAsync();

                var rid = SessionManager.RestauranteActualId;
                if (rid != Guid.Empty)
                    data = data.Where(x => x.RestauranteId == rid).ToList();

                foreach (var m in data) _items.Add(m);

                ApplyFilter();
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ApplyFilter()
        {
            var term = _txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(term))
            {
                _bs.DataSource = _items;
                _grid.Refresh();
                return;
            }

            var filtered = _items.Where(x => x.Nombre.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
            _bs.DataSource = filtered;
            _grid.Refresh();
        }

        private MenuDto? Selected()
            => _grid.CurrentRow?.DataBoundItem as MenuDto;

        private async Task Crear()
        {
            var rid = SessionManager.RestauranteActualId;
            if (rid == Guid.Empty)
            {
                MessageBox.Show("Selecciona un restaurante en el módulo 'Restaurantes' antes de crear menús.",
                    "Restaurante requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var f = new FrmMenuEdit(rid);
            if (f.ShowDialog(FindForm()) != DialogResult.OK) return;

            try
            {
                var created = await _service.CrearAsync(f.ResultCreate);
                if (created != null)
                {
                    _items.Insert(0, created);
                    ApplyFilter();
                }
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "No se pudo crear",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Editar()
        {
            var selected = Selected();
            if (selected is null)
            {
                MessageBox.Show("Selecciona un menú.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rid = SessionManager.RestauranteActualId != Guid.Empty ? SessionManager.RestauranteActualId : selected.RestauranteId;

            using var f = new FrmMenuEdit(rid, selected);
            if (f.ShowDialog(FindForm()) != DialogResult.OK) return;

            try
            {
                var updated = await _service.ActualizarAsync(selected.Id, f.ResultUpdate);
                if (updated != null)
                {
                    for (int i = 0; i < _items.Count; i++)
                    {
                        if (_items[i].Id == updated.Id)
                        {
                            _items[i] = updated;
                            _grid.Refresh();
                            ApplyFilter();
                            break;
                        }
                    }
                }
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "No se pudo actualizar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Eliminar()
        {
            var selected = Selected();
            if (selected is null)
            {
                MessageBox.Show("Selecciona un menú.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Eliminar menú?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                var ok = await _service.EliminarAsync(selected.Id);
                if (ok)
                {
                    var item = _items.FirstOrDefault(x => x.Id == selected.Id);
                    if (item != null) _items.Remove(item);
                    ApplyFilter();
                }
            }
            catch (ApiServiceException ex)
            {
                MessageBox.Show(UIErrorHandler.ToFriendlyMessage(ex), "No se pudo eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}