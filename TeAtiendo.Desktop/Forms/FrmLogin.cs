using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TeAtiendo.Desktop.Helpers;
using TeAtiendo.Desktop.Models.Requests;
using TeAtiendo.Desktop.Services;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Forms
{
    public partial class FrmLogin : Form
    {
        private Panel _pnlLeft = null!;
        private Panel _pnlRight = null!;
        private TextBox _txtEmail = null!;
        private TextBox _txtPass = null!;
        private Button _btnLogin = null!;
        private Label _lblError = null!;
        private Label _lblLoader = null!;
        private Panel _pnlEmailCont = null!;
        private Panel _pnlPassCont = null!;

        private readonly ApiService _api;
        private readonly AuthService _auth;

        public FrmLogin()
        {
            var baseUrl = "http://localhost:5067";
            _api = new ApiService(baseUrl);
            _auth = new AuthService(_api);
            BuildUI();
        }

        private void BuildUI()
        {
            SuspendLayout();
            Text = "Te Atiendo — Iniciar Sesión";
            Size = new Size(950, 600);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = ThemeHelper.BgMain;

            _pnlLeft = new Panel { Dock = DockStyle.Left, Width = 420, BackColor = ThemeHelper.BgSidebar };
            _pnlLeft.Paint += PaintBrand;

            _pnlRight = new Panel { Dock = DockStyle.Fill, BackColor = ThemeHelper.BgMain };

            var lblTit = new Label { Text = "Bienvenido", Font = ThemeHelper.FontTitle, ForeColor = ThemeHelper.TextPrimary, Location = new Point(70, 70), AutoSize = true };
            var lblSub = new Label { Text = "Ingresa tus credenciales para continuar", Font = ThemeHelper.FontBody, ForeColor = ThemeHelper.TextSecondary, Location = new Point(70, 115), AutoSize = true };

            var lblE = MkLabel("Correo electrónico", new Point(70, 175));
            _pnlEmailCont = new Panel { Location = new Point(70, 200), Size = new Size(380, 48) };
            _txtEmail = new TextBox { PlaceholderText = "correo@ejemplo.com", TabIndex = 1, Name = "txtEmail" };
            _pnlEmailCont.Controls.Add(_txtEmail);
            ThemeHelper.EstilizarContenedorInput(_pnlEmailCont, _txtEmail);

            var lblP = MkLabel("Contraseña", new Point(70, 265));
            _pnlPassCont = new Panel { Location = new Point(70, 290), Size = new Size(380, 48) };
            _txtPass = new TextBox { PlaceholderText = "••••••••", UseSystemPasswordChar = true, TabIndex = 2, Name = "txtPass" };
            _pnlPassCont.Controls.Add(_txtPass);
            ThemeHelper.EstilizarContenedorInput(_pnlPassCont, _txtPass);

            _btnLogin = new Button { Text = "INICIAR SESIÓN", Location = new Point(70, 365), Size = new Size(380, 50), TabIndex = 3 };
            ThemeHelper.EstilizarBotonPro(_btnLogin, 20);
            _btnLogin.Click += BtnLogin_Click;

            _lblError = new Label { ForeColor = ThemeHelper.Danger, Font = ThemeHelper.FontSmall, Location = new Point(70, 425), Size = new Size(380, 45), Visible = false };
            _lblLoader = new Label { Text = "Validando...", ForeColor = ThemeHelper.TextSecondary, Font = ThemeHelper.FontSmall, Location = new Point(70, 425), AutoSize = true, Visible = false };

            _pnlRight.Controls.AddRange(new Control[] { lblTit, lblSub, lblE, _pnlEmailCont, lblP, _pnlPassCont, _btnLogin, _lblError, _lblLoader });
            Controls.Add(_pnlRight);
            Controls.Add(_pnlLeft);

            AcceptButton = _btnLogin;
            ResumeLayout(false);
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtEmail.Text) || string.IsNullOrWhiteSpace(_txtPass.Text))
            { MostrarError("Completa todos los campos"); return; }

            SetBusy(true);
            try
            {
                var response = await _auth.LoginAsync(new LoginRequest { Correo = _txtEmail.Text.Trim(), Password = _txtPass.Text });

                if (response != null && response.Success)
                {
                    if (SessionManager.UsuarioActual?.Rol == RolUsuario.Cliente)
                    { MostrarError("Acceso denegado: Solo personal."); return; }

                    this.Hide();
                    using (FrmMain main = new FrmMain()) { main.ShowDialog(); }
                    this.Close();
                }
                else { MostrarError(response?.Message ?? "Error de credenciales"); }
            }
            catch (Exception ex) { MostrarError("Error: " + ex.Message); }
            finally { SetBusy(false); }
        }

        private void SetBusy(bool b) { _btnLogin.Enabled = !b; _lblLoader.Visible = b; _lblError.Visible = !b && _lblError.Visible; }
        private void MostrarError(string m) { _lblError.Text = m; _lblError.Visible = true; }
        private static Label MkLabel(string t, Point l) => new() { Text = t, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = ThemeHelper.TextPrimary, AutoSize = true, Location = l };
        private void PaintBrand(object? s, PaintEventArgs e)
        {
            var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            using var fLogo = new Font("Georgia", 38F, FontStyle.Bold);
            g.DrawString("Te", fLogo, Brushes.White, 65, 190);
            g.DrawString("Atiendo", fLogo, new SolidBrush(ThemeHelper.Primary), 130, 190);
        }

        private void InitializeComponent()
        {

        }
    }
}