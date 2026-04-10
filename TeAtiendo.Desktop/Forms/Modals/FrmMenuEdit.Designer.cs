namespace TeAtiendo.Desktop.Forms.Modals
{
    partial class FrmMenuEdit
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tblForm;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlRoot = new Panel();
            tblForm = new TableLayoutPanel();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            flowButtons = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnCancelar = new Button();
            lblTitle = new Label();
            errorProvider1 = new ErrorProvider(components);
            pnlRoot.SuspendLayout();
            tblForm.SuspendLayout();
            flowButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();

            pnlRoot.Controls.Add(tblForm);
            pnlRoot.Controls.Add(flowButtons);
            pnlRoot.Controls.Add(lblTitle);
            pnlRoot.Dock = DockStyle.Fill;
            pnlRoot.Location = new Point(0, 0);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Padding = new Padding(16);
            pnlRoot.Size = new Size(520, 300);
            pnlRoot.TabIndex = 0;

            tblForm.ColumnCount = 2;
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblForm.Controls.Add(lblNombre, 0, 0);
            tblForm.Controls.Add(txtNombre, 1, 0);
            tblForm.Controls.Add(lblDescripcion, 0, 1);
            tblForm.Controls.Add(txtDescripcion, 1, 1);
            tblForm.Dock = DockStyle.Top;
            tblForm.Location = new Point(16, 50);
            tblForm.Name = "tblForm";
            tblForm.Padding = new Padding(0, 12, 0, 12);
            tblForm.RowCount = 2;
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            tblForm.Size = new Size(488, 100);
            tblForm.TabIndex = 0;

            lblNombre.Dock = DockStyle.Fill;
            lblNombre.Location = new Point(3, 12);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(114, 34);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            lblNombre.TextAlign = ContentAlignment.MiddleLeft;

            txtNombre.Dock = DockStyle.Fill;
            txtNombre.Location = new Point(123, 15);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(362, 23);
            txtNombre.TabIndex = 1;

            lblDescripcion.Dock = DockStyle.Fill;
            lblDescripcion.Location = new Point(3, 46);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(114, 110);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Descripción";
            lblDescripcion.TextAlign = ContentAlignment.MiddleLeft;
 
            txtDescripcion.Dock = DockStyle.Fill;
            txtDescripcion.Location = new Point(123, 49);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(362, 104);
            txtDescripcion.TabIndex = 3;

            flowButtons.Controls.Add(btnGuardar);
            flowButtons.Controls.Add(btnCancelar);
            flowButtons.Dock = DockStyle.Bottom;
            flowButtons.FlowDirection = FlowDirection.RightToLeft;
            flowButtons.Location = new Point(16, 230);
            flowButtons.Name = "flowButtons";
            flowButtons.Size = new Size(488, 54);
            flowButtons.TabIndex = 1;
            flowButtons.WrapContents = false;

            btnGuardar.Location = new Point(365, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 40);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";

            btnCancelar.Location = new Point(239, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Location = new Point(16, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(488, 34);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Menú";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            errorProvider1.ContainerControl = this;

            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(520, 300);
            Controls.Add(pnlRoot);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMenuEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Menú";
            pnlRoot.ResumeLayout(false);
            tblForm.ResumeLayout(false);
            tblForm.PerformLayout();
            flowButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }
    }
}