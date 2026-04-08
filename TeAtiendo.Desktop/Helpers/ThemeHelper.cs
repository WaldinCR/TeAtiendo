namespace TeAtiendo.Desktop.Helpers
{
    public static class ThemeHelper
    {
        public static readonly Color Primary = ColorTranslator.FromHtml("#F37021");
        public static readonly Color PrimaryDark = ColorTranslator.FromHtml("#D45E0F");
        public static readonly Color PrimaryLight = Color.FromArgb(25, 243, 112, 33);

        public static readonly Color BgMain = ColorTranslator.FromHtml("#FDF9F4");
        public static readonly Color BgCard = Color.White;
        public static readonly Color BgSidebar = ColorTranslator.FromHtml("#2D1B15");
        public static readonly Color BgSidebarHover = ColorTranslator.FromHtml("#3D2B25");
        public static readonly Color BgSidebarActive = Color.FromArgb(40, 243, 112, 33);

        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#2D1B15");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#7A5548");
        public static readonly Color TextLight = ColorTranslator.FromHtml("#B89488");
        public static readonly Color TextWhite = Color.White;

        public static readonly Color Border = Color.FromArgb(20, 45, 27, 21);
        public static readonly Color BorderMedium = Color.FromArgb(36, 45, 27, 21);

        public static readonly Color Success = ColorTranslator.FromHtml("#1FAD6E");
        public static readonly Color SuccessBg = Color.FromArgb(25, 31, 173, 110);
        public static readonly Color Danger = ColorTranslator.FromHtml("#DC3545");
        public static readonly Color DangerBg = Color.FromArgb(25, 220, 53, 69);
        public static readonly Color Warning = ColorTranslator.FromHtml("#FFC107");
        public static readonly Color Info = ColorTranslator.FromHtml("#0DCAF0");

        public static readonly Font FontTitle = new("Georgia", 20F, FontStyle.Bold);
        public static readonly Font FontSubtitle = new("Georgia", 14F, FontStyle.Bold);
        public static readonly Font FontBody = new("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBodyBold = new("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontSmall = new("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font FontSidebar = new("Segoe UI", 11F, FontStyle.Regular);
        public static readonly Font FontSidebarActive = new("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font FontButton = new("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontHeader = new("Segoe UI", 12F, FontStyle.Bold);

        public static void EstilizarFormulario(Form form)
        {
            form.BackColor = BgMain;
            form.Font = FontBody;
            form.ForeColor = TextPrimary;
        }

        public static void EstilizarPanel(Panel panel, bool esCard = false)
        {
            panel.BackColor = esCard ? BgCard : BgMain;
            if (esCard)
            {
                panel.Padding = new Padding(20);
            }
        }

        public static void EstilizarBotonPrimario(Button btn)
        {
            btn.BackColor = Primary;
            btn.ForeColor = TextWhite;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = FontButton;
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;
            btn.Padding = new Padding(20, 0, 20, 0);

            btn.MouseEnter += (s, e) => btn.BackColor = PrimaryDark;
            btn.MouseLeave += (s, e) => btn.BackColor = Primary;
        }

        public static void EstilizarBotonSecundario(Button btn)
        {
            btn.BackColor = BgCard;
            btn.ForeColor = TextPrimary;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = BorderMedium;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = FontButton;
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;

            btn.MouseEnter += (s, e) => btn.BackColor = BgMain;
            btn.MouseLeave += (s, e) => btn.BackColor = BgCard;
        }

        public static void EstilizarBotonPeligro(Button btn)
        {
            btn.BackColor = Danger;
            btn.ForeColor = TextWhite;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = FontButton;
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;
        }

        public static void EstilizarBotonExito(Button btn)
        {
            btn.BackColor = Success;
            btn.ForeColor = TextWhite;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = FontButton;
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;
        }

        public static void EstilizarTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = BgCard;
            txt.ForeColor = TextPrimary;
            txt.Font = FontBody;
            txt.Height = 36;
        }

        public static void EstilizarComboBox(ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = BgCard;
            cmb.ForeColor = TextPrimary;
            cmb.Font = FontBody;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static void EstilizarDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = BgCard;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Border;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;

            
            dgv.ColumnHeadersDefaultCellStyle.BackColor = BgMain;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextSecondary;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBodyBold;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = BgMain;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextSecondary;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            
            dgv.DefaultCellStyle.BackColor = BgCard;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = FontBody;
            dgv.DefaultCellStyle.SelectionBackColor = PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            dgv.RowTemplate.Height = 42;

            
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 248, 243);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = PrimaryLight;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimary;
        }

        public static void EstilizarLabel(Label lbl, bool esSecundario = false)
        {
            lbl.ForeColor = esSecundario ? TextSecondary : TextPrimary;
            lbl.Font = esSecundario ? FontSmall : FontBody;
        }

        public static void EstilizarTitulo(Label lbl)
        {
            lbl.ForeColor = TextPrimary;
            lbl.Font = FontTitle;
        }

        public static void EstilizarSubtitulo(Label lbl)
        {
            lbl.ForeColor = TextPrimary;
            lbl.Font = FontSubtitle;
        }

        public static Panel CrearCard(int x, int y, int width, int height)
        {
            var panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = BgCard,
                Padding = new Padding(20),
            };
            panel.Paint += (s, e) =>
            {
                using var pen = new Pen(Border, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            };
            return panel;
        }

        public static Panel CrearStatCard(string titulo, string valor, Color colorAccent, int x, int y, int width = 220, int height = 100)
        {
            var card = CrearCard(x, y, width, height);

            var lblTitulo = new Label
            {
                Text = titulo,
                ForeColor = TextSecondary,
                Font = FontSmall,
                Location = new Point(20, 16),
                AutoSize = true
            };

            var lblValor = new Label
            {
                Text = valor,
                ForeColor = colorAccent,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                Location = new Point(20, 40),
                AutoSize = true
            };

            var accentBar = new Panel
            {
                BackColor = colorAccent,
                Size = new Size(4, height),
                Location = new Point(0, 0)
            };

            card.Controls.AddRange(new Control[] { accentBar, lblTitulo, lblValor });
            return card;
        }
    }
}