using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TeAtiendo.Desktop.Helpers
{
    public static class ThemeHelper
    {
        // ==========================================
        // PALETA DE COLORES (PRO WEB STYLE)
        // ==========================================

        // Primarios
        public static readonly Color Primary = ColorTranslator.FromHtml("#B3522C");
        public static readonly Color PrimaryDark = ColorTranslator.FromHtml("#964324");
        public static readonly Color PrimaryLight = Color.FromArgb(25, 179, 82, 44);

        // Fondos
        public static readonly Color BgMain = ColorTranslator.FromHtml("#FDF9F4");   // Crema Web
        public static readonly Color BgCard = Color.White;                          // Tarjetas Blancas
        public static readonly Color BgWarm = ColorTranslator.FromHtml("#FAF5EF");
        public static readonly Color InputBg = ColorTranslator.FromHtml("#EDF3FF");  // Azulito Web

        // Sidebar
        public static readonly Color BgSidebar = ColorTranslator.FromHtml("#2D1B15");
        public static readonly Color BgSidebarHover = ColorTranslator.FromHtml("#3D2B25");
        public static readonly Color BgSidebarActive = Color.FromArgb(40, 179, 82, 44);

        // Textos
        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#2D1B15");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#7A5548");
        public static readonly Color TextTertiary = ColorTranslator.FromHtml("#A89080");
        public static readonly Color TextLight = Color.Gray;
        public static readonly Color TextWhite = Color.White;

        // Estados y Bordes
        public static readonly Color Border = ColorTranslator.FromHtml("#E8DDD4");
        public static readonly Color BorderMedium = ColorTranslator.FromHtml("#E8DDD4");
        public static readonly Color Success = ColorTranslator.FromHtml("#2E7D32");
        public static readonly Color SuccessBg = Color.FromArgb(25, 46, 125, 50);
        public static readonly Color Danger = Color.Firebrick;
        public static readonly Color DangerBg = Color.FromArgb(25, 198, 40, 40);
        public static readonly Color Warning = ColorTranslator.FromHtml("#F57F17");
        public static readonly Color Info = ColorTranslator.FromHtml("#1565C0");

        // ==========================================
        // TIPOGRAFÍA
        // ==========================================
        public static readonly Font FontTitle = new Font("Georgia", 22F, FontStyle.Bold);
        public static readonly Font FontSubtitle = new Font("Georgia", 14F, FontStyle.Bold);
        public static readonly Font FontBody = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBodyBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontSmall = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font FontButton = new Font("Segoe UI", 10F, FontStyle.Bold);

        // Tipografía Sidebar
        public static readonly Font FontSidebar = new Font("Segoe UI", 11F, FontStyle.Regular);
        public static readonly Font FontSidebarActive = new Font("Segoe UI", 11F, FontStyle.Bold);

        // ==========================================
        // MÉTODOS DE ESTILO (PRO)
        // ==========================================

        public static void ApplyTheme() { /* No-op para compatibilidad */ }

        public static void EstilizarFormulario(Form f)
        {
            f.BackColor = BgMain;
            f.Font = FontBody;
            f.ForeColor = TextPrimary;
        }

        public static void EstilizarBotonPro(Button btn, int radio = 15)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Primary;
            btn.ForeColor = Color.White;
            btn.Font = FontButton;
            btn.Cursor = Cursors.Hand;
            btn.Height = 45;

            AplicarBordesRedondeados(btn, radio);

            btn.MouseEnter += (s, e) => btn.BackColor = PrimaryDark;
            btn.MouseLeave += (s, e) => btn.BackColor = Primary;
        }

        public static void EstilizarBotonPrimario(Button b) => EstilizarBotonPro(b);

        public static void EstilizarContenedorInput(Panel pnl, TextBox txt)
        {
            pnl.BackColor = InputBg;
            pnl.Height = 45;
            AplicarBordesRedondeados(pnl, 15);

            txt.BackColor = InputBg;
            txt.BorderStyle = BorderStyle.None;
            txt.Font = new Font("Segoe UI", 11F);
            txt.ForeColor = TextPrimary;

            // Centrado vertical y horizontal con padding
            txt.Width = pnl.Width - 25;
            txt.Location = new Point(12, (pnl.Height - txt.Height) / 2);

            pnl.Click += (s, e) => txt.Focus();
        }

        // ==========================================
        // UTILIDADES (GDI+)
        // ==========================================

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static void AplicarBordesRedondeados(Control control, int radio)
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radio, radio));
        }

        public static void EstilizarDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = BgCard;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Border;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = BgCard;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextSecondary;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBodyBold;
            dgv.ColumnHeadersHeight = 44;

            dgv.DefaultCellStyle.BackColor = BgCard;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.RowTemplate.Height = 42;
        }

        public static void EstilizarBotonPeligro(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Danger;          // asegúrate que exista Danger en ThemeHelper
            b.ForeColor = Color.White;
            b.Font = FontButton;
            b.Cursor = Cursors.Hand;
        }

        public static void EstilizarBotonSecundario(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = BorderMedium;
            b.BackColor = BgCard;
            b.ForeColor = TextPrimary;
            b.Font = FontButton;
            b.Cursor = Cursors.Hand;
        }

    }
}