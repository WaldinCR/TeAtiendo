using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TeAtiendo.Desktop.Services;
using TeAtiendo.Desktop.Core.Services;
using TeAtiendo.Desktop.Helpers;

namespace TeAtiendo.Desktop.UserControls
{
    public partial class UcDashboard : UserControl
    {
        private readonly ReservaService _reservaService;
        private readonly OrdenService _ordenService;
        private readonly NotificacionService _notiService;

        private readonly Label _lblSaludo = new();
        private readonly Label _lblSub = new();

        private readonly Panel panelCards = new();
        private readonly Label lblRes = new();
        private readonly Label lblOrd = new();
        private readonly Label lblCap = new();

        private readonly Panel panelAcciones = new();
        private readonly Button btnNuevaOrden = new();
        private readonly Button btnNuevaReserva = new();
        private readonly Button btnVerMenu = new();

        public UcDashboard()
        {
            var factory = new ServiceFactory();
            _reservaService = factory.ReservaService();
            _ordenService = factory.OrdenService();
            _notiService = factory.NotificacionService();

            BuildUi();
            _ = RefreshAll();
        }

        private void BuildUi()
        {
            Dock = DockStyle.Fill;
            BackColor = ThemeHelper.BgMain;

            _lblSaludo.Text = $"Bienvenido, {SessionManager.UsuarioActual?.Nombre ?? "Admin"}";
            _lblSaludo.Font = new Font("Georgia", 20, FontStyle.Bold);
            _lblSaludo.ForeColor = ThemeHelper.TextPrimary;
            _lblSaludo.Location = new Point(22, 14);
            _lblSaludo.AutoSize = true;

            _lblSub.Text = "Resumen general de tu restaurante hoy.";
            _lblSub.Font = ThemeHelper.FontSmall;
            _lblSub.ForeColor = ThemeHelper.TextSecondary;
            _lblSub.Location = new Point(22, 48);
            _lblSub.AutoSize = true;

            panelCards.BackColor = Color.Transparent;
            panelCards.Location = new Point(8, 90);
            panelCards.Size = new Size(600, 80);

            Panel card1 = BuildCard(lblRes, "Reservas (hoy)", "0", 0);
            Panel card2 = BuildCard(lblOrd, "Órdenes activas", "0", 1);
            Panel card3 = BuildCard(lblCap, "Capacidad", "50", 2);

            panelCards.Controls.Add(card1);
            panelCards.Controls.Add(card2);
            panelCards.Controls.Add(card3);

            var lblAcc = new Label
            {
                Text = "Acciones rápidas",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(22, 200),
                AutoSize = true
            };

            panelAcciones.BackColor = Color.Transparent;
            panelAcciones.Location = new Point(22, 235);
            panelAcciones.Size = new Size(500, 62);

            btnNuevaOrden.Text = "Nueva orden";
            btnNuevaOrden.BackColor = Color.FromArgb(180, 141, 50);
            btnNuevaOrden.ForeColor = Color.White;
            btnNuevaOrden.Font = ThemeHelper.FontBodyBold;
            btnNuevaOrden.Size = new Size(130, 36);
            btnNuevaOrden.FlatStyle = FlatStyle.Flat;

            btnNuevaReserva.Text = "Nueva reserva";
            btnNuevaReserva.BackColor = Color.White;
            btnNuevaReserva.ForeColor = ThemeHelper.BgSidebar;
            btnNuevaReserva.Font = ThemeHelper.FontBodyBold;
            btnNuevaReserva.Size = new Size(130, 36);
            btnNuevaReserva.Left = 150;
            btnNuevaReserva.FlatStyle = FlatStyle.Flat;

            btnVerMenu.Text = "Ver menú";
            btnVerMenu.BackColor = Color.White;
            btnVerMenu.ForeColor = ThemeHelper.BgSidebar;
            btnVerMenu.Font = ThemeHelper.FontBodyBold;
            btnVerMenu.Size = new Size(130, 36);
            btnVerMenu.Left = 300;
            btnVerMenu.FlatStyle = FlatStyle.Flat;

            panelAcciones.Controls.Add(btnNuevaOrden);
            panelAcciones.Controls.Add(btnNuevaReserva);
            panelAcciones.Controls.Add(btnVerMenu);

            Controls.Add(_lblSaludo);
            Controls.Add(_lblSub);
            Controls.Add(panelCards);
            Controls.Add(lblAcc);
            Controls.Add(panelAcciones);
        }

        private Panel BuildCard(Label lbl, string title, string value, int order)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(170, 80),
                Location = new Point(200 * order, 0),
                Margin = new Padding(8)
            };
            lbl.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lbl.ForeColor = ThemeHelper.BgSidebar;
            lbl.Text = value;
            lbl.Location = new Point(20, 30);
            lbl.AutoSize = true;

            var lblT = new Label
            {
                Text = title,
                Font = ThemeHelper.FontSmall,
                ForeColor = ThemeHelper.TextSecondary,
                Location = new Point(16, 10),
                AutoSize = true
            };

            card.Controls.Add(lblT);
            card.Controls.Add(lbl);
            return card;
        }

        private async Task RefreshAll()
        {
            try
            {
                var reservas = await _reservaService.ObtenerTodosAsync();
                lblRes.Text = reservas?.Count.ToString() ?? "0";
                var ordenes = await _ordenService.ObtenerTodosAsync();
                lblOrd.Text = ordenes?.Count.ToString() ?? "0";
                lblCap.Text = "50";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error cargando el dashboard:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}