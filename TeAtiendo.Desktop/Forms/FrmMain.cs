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
        private Label lblNotifBadge = null!;

        private readonly List<Button> _sidebarButtons = new();
        private Button? _btnActivo;

        private System.Windows.Forms.Timer _timerNotif = null!;

        public FrmMain()
        {
            InitializeComponent();
            _ = CargarNotificacionesAsync();
        }

        private void InitializeComponent()
        {
            this.Text = "Te Atiendo - Panel de Gestion";
            this.Size = new Size(1280, 750);
            this.MinimumSize = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemeHelper.BgMain;
            this.WindowState = FormWindowState.Maximized;

            pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 240,
                BackColor = ThemeHelper.BgSidebar
            };

            var pnlLogo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.Transparent
            };
            pnlLogo.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var fontLogo = new Font("Segoe UI", 18F, FontStyle.Bold);
                using var brushWhite = new SolidBrush(Color.White);
                using var brushOrange = new SolidBrush(ThemeHelper.Primary);
                g.DrawString("Te", fontLogo, brushWhite, 20, 20);
                g.DrawString("Atiendo", fontLogo, brushOrange, 55, 20);
            };

            var sepLogo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = Color.FromArgb(40, 255, 255, 255)
            };

            var pnlSidebarMenu = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 10, 0, 0)
            };

            int yPos = 10;

            AgregarBotonSidebar(pnlSidebarMenu, "Dashboard", ref yPos, () => Navegar<UcDashboard>("Dashboard"));
            AgregarBotonSidebar(pnlSidebarMenu, "Restaurantes", ref yPos, () => Navegar<UcRestaurantes>("Restaurantes"));
            AgregarBotonSidebar(pnlSidebarMenu, "Menus", ref yPos, () => Navegar<UcMenus>("Menus"));
            AgregarBotonSidebar(pnlSidebarMenu, "Mesas", ref yPos, () => Navegar<UcMesas>("Mesas"));
            AgregarBotonSidebar(pnlSidebarMenu, "Disponibilidad", ref yPos, () => Navegar<UcDisponibilidades>("Disponibilidad"));
            AgregarBotonSidebar(pnlSidebarMenu, "Reservas", ref yPos, () => Navegar<UcReservas>("Reservas"));
            AgregarBotonSidebar(pnlSidebarMenu, "Ordenes", ref yPos, () => Navegar<UcOrdenes>("Ordenes"));
            AgregarBotonSidebar(pnlSidebarMenu, "Pagos", ref yPos, () => Navegar<UcPagos>("Pagos"));
            AgregarBotonSidebar(pnlSidebarMenu, "Notificaciones", ref yPos, () => Navegar<UcNotificaciones>("Notificaciones"));

            if (SessionManager.EsAdmin)
            {
                yPos += 15;
                var lblAdmin = new Label
                {
                    Text = "ADMINISTRACION",
                    ForeColor = Color.FromArgb(100, 255, 255, 255),
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    Location = new Point(20, yPos),
                    AutoSize = true
                };
                pnlSidebarMenu.Controls.Add(lblAdmin);
                yPos += 25;

                AgregarBotonSidebar(pnlSidebarMenu, "Usuarios", ref yPos, () => Navegar<UcUsuarios>("Usuarios"));
                AgregarBotonSidebar(pnlSidebarMenu, "Auditoria", ref yPos, () => Navegar<UcAuditoria>("Auditoria"));
                AgregarBotonSidebar(pnlSidebarMenu, "Moderacion", ref yPos, () => Navegar<UcModeracion>("Moderacion"));
            }

            yPos += 15;
            var sepConfig = new Panel
            {
                Location = new Point(20, yPos),
                Size = new Size(200, 1),
                BackColor = Color.FromArgb(40, 255, 255, 255)
            };
            pnlSidebarMenu.Controls.Add(sepConfig);
            yPos += 12;

            AgregarBotonSidebar(pnlSidebarMenu, "Configuracion", ref yPos, () => Navegar<UcConfiguracion>("Configuracion"));

            var pnlLogout = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.Transparent
            };

            var btnLogout = new Button
            {
                Text = "Cerrar Sesion",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(180, 255, 255, 255),
                Font = ThemeHelper.FontSidebar,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 220, 53, 69);
            btnLogout.Click += (s, e) =>
            {
                if (MessageBox.Show("Desea cerrar sesion?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SessionManager.CerrarSesion();
                    this.Close();
                }
            };
            pnlLogout.Controls.Add(btnLogout);

            pnlSidebar.Controls.Add(pnlSidebarMenu);
            pnlSidebar.Controls.Add(sepLogo);
            pnlSidebar.Controls.Add(pnlLogo);
            pnlSidebar.Controls.Add(pnlLogout);

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ThemeHelper.BgCard,
                Padding = new Padding(25, 0, 25, 0)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeHelper.Border, 1);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            lblHeaderTitulo = new Label
            {
                Text = "Dashboard",
                Font = ThemeHelper.FontSubtitle,
                ForeColor = ThemeHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(25, 18)
            };

            var nombreUsuario = SessionManager.UsuarioActual?.Nombre ?? "Usuario";
            var rolTexto = SessionManager.EsAdmin ? "Administrador" : "Restaurante";

            lblUsuarioNombre = new Label
            {
                Text = $"{nombreUsuario}  |  {rolTexto}",
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.TextSecondary,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            lblNotifBadge = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = ThemeHelper.Danger,
                Size = new Size(24, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Visible = false,
                Cursor = Cursors.Hand
            };
            lblNotifBadge.Click += (s, e) => Navegar<UcNotificaciones>("Notificaciones");

            pnlHeader.Controls.AddRange(new Control[] { lblHeaderTitulo, lblUsuarioNombre, lblNotifBadge });

            pnlHeader.Resize += (s, e) =>
            {
                lblUsuarioNombre.Location = new Point(pnlHeader.Width - lblUsuarioNombre.Width - 30, 20);
                lblNotifBadge.Location = new Point(pnlHeader.Width - lblUsuarioNombre.Width - 60, 20);
            };

            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.BgMain,
                Padding = new Padding(25)
            };

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlSidebar);

            _timerNotif = new System.Windows.Forms.Timer { Interval = 30000 };
            _timerNotif.Tick += async (s, e) => await CargarNotificacionesAsync();
            _timerNotif.Start();

            if (_sidebarButtons.Count > 0)
            {
                _sidebarButtons[0].PerformClick();
            }
        }

        private void AgregarBotonSidebar(Panel container, string texto, ref int yPos, Action onClick)
        {
            var btn = new Button
            {
                Text = "   " + texto,
                Location = new Point(8, yPos),
                Size = new Size(224, 42),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                Font = ThemeHelper.FontSidebar,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                Padding = new Padding(12, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ThemeHelper.BgSidebarHover;

            btn.Click += (s, e) =>
            {
                if (_btnActivo != null)
                {
                    _btnActivo.BackColor = Color.Transparent;
                    _btnActivo.ForeColor = Color.FromArgb(200, 255, 255, 255);
                    _btnActivo.Font = ThemeHelper.FontSidebar;
                }

                btn.BackColor = ThemeHelper.BgSidebarActive;
                btn.ForeColor = ThemeHelper.Primary;
                btn.Font = ThemeHelper.FontSidebarActive;
                _btnActivo = btn;

                onClick();
            };

            container.Controls.Add(btn);
            _sidebarButtons.Add(btn);
            yPos += 46;
        }

        private void Navegar<T>(string titulo) where T : UserControl, new()
        {
            lblHeaderTitulo.Text = titulo;
            pnlContent.Controls.Clear();

            var uc = new T { Dock = DockStyle.Fill };
            pnlContent.Controls.Add(uc);
        }

        private async Task CargarNotificacionesAsync()
        {
            try
            {
                if (SessionManager.UsuarioActual == null) return;

               
                var noLeidas = await AppBootstrapper.Notificaciones.ObtenerNoLeidasAsync(SessionManager.UsuarioActual.Id);
                var count = noLeidas.Count;

                this.Invoke(() =>
                {
                    if (count > 0)
                    {
                        lblNotifBadge.Text = count > 99 ? "99+" : count.ToString();
                        lblNotifBadge.Visible = true;
                    }
                    else
                    {
                        lblNotifBadge.Visible = false;
                    }
                });
            }
            catch
            {
               
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _timerNotif?.Stop();
            _timerNotif?.Dispose();
            base.OnFormClosed(e);
        }
    }
}