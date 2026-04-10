using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TeAtiendo.Desktop.Helpers;
using TeAtiendo.Desktop.UserControls;

namespace TeAtiendo.Desktop.Forms
{
    public partial class FrmMain : Form
    {
        private Panel pnlSidebar = null!;
        private Panel pnlHeader = null!;
        private Panel pnlContent = null!;
        private Label lblHeaderTitulo = null!;
        private Label lblUsuarioNombre = null!;
        private readonly List<Button> _sidebarButtons = new();
        private Button? _btnActivo;

        public FrmMain()
        {
            BuildShellUI();
        }

        private void BuildShellUI()
        {
            this.SuspendLayout();
            this.Text = "Te Atiendo - Panel de Gestión";
            this.Size = new Size(1280, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = ThemeHelper.BgMain;

            pnlSidebar = new Panel { Dock = DockStyle.Left, Width = 260, BackColor = ThemeHelper.BgSidebar };
            var pnlLogo = new Panel { Dock = DockStyle.Top, Height = 80 };
            pnlLogo.Paint += (s, e) => {
                using var f = new Font("Georgia", 20F, FontStyle.Bold);
                e.Graphics.DrawString("Te", f, Brushes.White, 25, 22);
                e.Graphics.DrawString("Atiendo", f, new SolidBrush(ThemeHelper.Primary), 65, 22);
            };

            var pnlMenu = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            int yPos = 10;
            AgregarBotonSidebar(pnlMenu, "Dashboard", ref yPos, () => Navegar<UcDashboard>("Resumen General"));
            AgregarBotonSidebar(pnlMenu, "Restaurantes", ref yPos, () => Navegar<UcRestaurantes>("Mis Restaurantes"));
            AgregarBotonSidebar(pnlMenu, "Reservas", ref yPos, () => Navegar<UcReservas>("Reservas Entrantes"));
            AgregarBotonSidebar(pnlMenu, "Menús", ref yPos, () => Navegar<UcMenus>("Gestión de Menús"));

            pnlSidebar.Controls.Add(pnlMenu);
            pnlSidebar.Controls.Add(pnlLogo);

            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 65, BackColor = Color.White };
            lblHeaderTitulo = new Label { Text = "Dashboard", Font = ThemeHelper.FontSubtitle, ForeColor = ThemeHelper.TextPrimary, Location = new Point(25, 20), AutoSize = true };
            lblUsuarioNombre = new Label
            {
                Text = $"{SessionManager.UsuarioActual?.Nombre} | {(SessionManager.EsAdmin ? "Admin" : "Restaurante")}",
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderTitulo);
            pnlHeader.Controls.Add(lblUsuarioNombre);
            pnlHeader.Resize += (s, e) => lblUsuarioNombre.Location = new Point(pnlHeader.Width - lblUsuarioNombre.Width - 25, 22);

            pnlContent = new Panel { Dock = DockStyle.Fill, BackColor = ThemeHelper.BgMain, Padding = new Padding(30) };

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlSidebar);

            if (_sidebarButtons.Count > 0) _sidebarButtons[0].PerformClick();
            this.ResumeLayout(false);
        }

        private void AgregarBotonSidebar(Panel container, string texto, ref int yPos, Action onClick)
        {
            var btn = new Button { Text = "      " + texto, Location = new Point(10, yPos), Size = new Size(240, 45), FlatStyle = FlatStyle.Flat, ForeColor = Color.White, Font = ThemeHelper.FontSidebar, TextAlign = ContentAlignment.MiddleLeft, Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => {
                if (_btnActivo != null) _btnActivo.BackColor = Color.Transparent;
                btn.BackColor = ThemeHelper.BgSidebarActive;
                _btnActivo = btn;
                onClick();
            };
            container.Controls.Add(btn);
            _sidebarButtons.Add(btn);
            yPos += 50;
        }

        private void Navegar<T>(string titulo) where T : UserControl, new()
        {
            lblHeaderTitulo.Text = titulo;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(new T { Dock = DockStyle.Fill });
        }
    }
}