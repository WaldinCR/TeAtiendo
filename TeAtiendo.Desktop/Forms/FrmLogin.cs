using TeAtiendo.Desktop.Helpers;
using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.Forms
{
    public partial class FrmLogin : Form
    {
        
        private Panel pnlIzquierdo = null!;
        private Panel pnlDerecho = null!;
        private TextBox txtCorreo = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Label lblError = null!;
        private Label lblCargando = null!;

        
        private readonly ApiService _apiService;
        private readonly AuthService _authService;

        public FrmLogin()
        {
            _apiService = new ApiService();
            _authService = new AuthService(_apiService);
            InitializeComponent();
        }

        private void InitializeComponent()
        {

            this.Text = "Te Atiendo - Iniciar Sesion";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = ThemeHelper.BgMain;


            pnlIzquierdo = new Panel
            {
                Dock = DockStyle.Left,
                Width = 380,
                BackColor = ThemeHelper.BgSidebar
            };
            pnlIzquierdo.Paint += PnlIzquierdo_Paint;


            pnlDerecho = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.BgMain,
                Padding = new Padding(60, 40, 60, 40)
            };

            
            var lblBienvenido = new Label
            {
                Text = "Bienvenido",
                Font = ThemeHelper.FontTitle,
                ForeColor = ThemeHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(60, 60)
            };


            var lblSubtitulo = new Label
            {
                Text = "Ingresa tus credenciales para continuar",
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.TextSecondary,
                AutoSize = true,
                Location = new Point(60, 95)
            };

            
            var lblCorreo = new Label
            {
                Text = "Correo electronico",
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(60, 150)
            };

            
            txtCorreo = new TextBox
            {
                Location = new Point(60, 175),
                Size = new Size(380, 36),
                PlaceholderText = "usuario@correo.com"
            };
            ThemeHelper.EstilizarTextBox(txtCorreo);

            
            var lblPassword = new Label
            {
                Text = "Contrasena",
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextPrimary,
                AutoSize = true,
                Location = new Point(60, 225)
            };

            
            txtPassword = new TextBox
            {
                Location = new Point(60, 250),
                Size = new Size(380, 36),
                UseSystemPasswordChar = true,
                PlaceholderText = "Tu contrasena"
            };
            ThemeHelper.EstilizarTextBox(txtPassword);

            
            btnLogin = new Button
            {
                Text = "INICIAR SESION",
                Location = new Point(60, 315),
                Size = new Size(380, 45)
            };
            ThemeHelper.EstilizarBotonPrimario(btnLogin);
            btnLogin.Click += BtnLogin_Click;

            
            lblError = new Label
            {
                Text = "",
                ForeColor = ThemeHelper.Danger,
                Font = ThemeHelper.FontSmall,
                Location = new Point(60, 370),
                Size = new Size(380, 40),
                Visible = false
            };

            
            lblCargando = new Label
            {
                Text = "Conectando con el servidor...",
                ForeColor = ThemeHelper.TextSecondary,
                Font = ThemeHelper.FontSmall,
                Location = new Point(60, 370),
                AutoSize = true,
                Visible = false
            };

            
            pnlDerecho.Controls.AddRange(new Control[]
            {
                lblBienvenido, lblSubtitulo,
                lblCorreo, txtCorreo,
                lblPassword, txtPassword,
                btnLogin, lblError, lblCargando
            });

            
            this.Controls.Add(pnlDerecho);
            this.Controls.Add(pnlIzquierdo);

            
            this.AcceptButton = btnLogin;

            
            this.Shown += (s, e) => txtCorreo.Focus();
        }


        private void PnlIzquierdo_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            
            using var brush = new SolidBrush(Color.FromArgb(30, 243, 112, 33));
            g.FillEllipse(brush, -60, -60, 300, 300);
            g.FillEllipse(brush, 200, 350, 250, 250);

            
            using var fontLogo = new Font("Segoe UI", 32F, FontStyle.Bold);
            using var brushWhite = new SolidBrush(Color.White);
            g.DrawString("Te", fontLogo, brushWhite, 60, 180);


            using var brushOrange = new SolidBrush(ThemeHelper.Primary);
            using var fontLogo2 = new Font("Segoe UI", 32F, FontStyle.Bold);
            g.DrawString("Atiendo", fontLogo2, brushOrange, 120, 180);

            
            using var fontSub = new Font("Segoe UI", 11F, FontStyle.Regular);
            using var brushLight = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
            g.DrawString("Panel de Gestion", fontSub, brushLight, 62, 230);
            g.DrawString("Administra tu restaurante", fontSub, brushLight, 62, 255);
            g.DrawString("de forma inteligente", fontSub, brushLight, 62, 280);
        }


        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MostrarError("Ingresa tu correo electronico");
                txtCorreo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MostrarError("Ingresa tu contrasena");
                txtPassword.Focus();
                return;
            }

            
            btnLogin.Enabled = false;
            btnLogin.Text = "CONECTANDO...";
            lblError.Visible = false;
            lblCargando.Visible = true;

            try
            {
                var response = await _authService.LoginAsync(txtCorreo.Text.Trim(), txtPassword.Text);


                if (response == null)
                {
                    MostrarError("No se recibio respuesta del servidor");
                    return;
                }


                if (!response.Success || response.Token == null || response.User == null)
                {
                    MostrarError(response.Message ?? "Credenciales incorrectas");
                    return;
                }

                
                if (response.User.Rol == (int)Models.RolUsuario.Cliente)
                {
                    MostrarError("Esta aplicacion es solo para administradores y duenos de restaurantes");
                    return;
                }
               
                SessionManager.IniciarSesion(response.Token, response.User);

                this.Hide();
                var frmMain = new FrmMain();
                frmMain.FormClosed += (s, args) =>
                {
                    SessionManager.CerrarSesion();
                    this.Show();
                    txtPassword.Clear();
                    txtCorreo.Focus();
                };
                frmMain.Show();
            }
            catch (ApiException ex)
            {
                MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError($"Error inesperado: {ex.Message}");
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "INICIAR SESION";
                lblCargando.Visible = false;
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
            lblCargando.Visible = false;
        }
    }
}