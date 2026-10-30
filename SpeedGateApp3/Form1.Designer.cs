namespace SpeedGateApp3
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatusMsg1 = new System.Windows.Forms.Label();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.cbMotivo = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.lblInstruccion = new System.Windows.Forms.Label();
            this.lblStatusMsg2 = new System.Windows.Forms.Label();
            this.lblStatusMsg3 = new System.Windows.Forms.Label();
            this.lblStatusMsg4 = new System.Windows.Forms.Label();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlStatus
            // 
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.lblStatusMsg4);
            this.pnlStatus.Controls.Add(this.lblStatusMsg3);
            this.pnlStatus.Controls.Add(this.lblStatusMsg2);
            this.pnlStatus.Controls.Add(this.lblStatusMsg1);
            this.pnlStatus.Location = new System.Drawing.Point(241, 295);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(1563, 671);
            this.pnlStatus.TabIndex = 0;
            // 
            // lblStatusMsg1
            // 
            this.lblStatusMsg1.AutoSize = true;
            this.lblStatusMsg1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMsg1.Location = new System.Drawing.Point(674, 18);
            this.lblStatusMsg1.Name = "lblStatusMsg1";
            this.lblStatusMsg1.Size = new System.Drawing.Size(188, 25);
            this.lblStatusMsg1.TabIndex = 6;
            this.lblStatusMsg1.Text = "En espera de lectura...";
            // 
            // txtMatricula
            // 
            this.txtMatricula.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatricula.Location = new System.Drawing.Point(870, 242);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(286, 32);
            this.txtMatricula.TabIndex = 1;
            // 
            // cbMotivo
            // 
            this.cbMotivo.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMotivo.FormattingEnabled = true;
            this.cbMotivo.Items.AddRange(new object[] {
            "Estudio libre / Tareas",
            "Préstamo de libros",
            "Devolución"});
            this.cbMotivo.Location = new System.Drawing.Point(765, 140);
            this.cbMotivo.Name = "cbMotivo";
            this.cbMotivo.Size = new System.Drawing.Size(477, 42);
            this.cbMotivo.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(731, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(546, 48);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Control de Acceso a Biblioteca";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivo.Location = new System.Drawing.Point(958, 99);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(98, 35);
            this.lblMotivo.TabIndex = 4;
            this.lblMotivo.Text = "Motivo:";
            // 
            // lblInstruccion
            // 
            this.lblInstruccion.AutoSize = true;
            this.lblInstruccion.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstruccion.Location = new System.Drawing.Point(825, 202);
            this.lblInstruccion.Name = "lblInstruccion";
            this.lblInstruccion.Size = new System.Drawing.Size(367, 25);
            this.lblInstruccion.TabIndex = 5;
            this.lblInstruccion.Text = "Acerque el código QR o escriba la matrícula:";
            // 
            // lblStatusMsg2
            // 
            this.lblStatusMsg2.AutoSize = true;
            this.lblStatusMsg2.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMsg2.Location = new System.Drawing.Point(506, 75);
            this.lblStatusMsg2.Name = "lblStatusMsg2";
            this.lblStatusMsg2.Size = new System.Drawing.Size(72, 98);
            this.lblStatusMsg2.TabIndex = 7;
            this.lblStatusMsg2.Text = "..";
            // 
            // lblStatusMsg3
            // 
            this.lblStatusMsg3.AutoSize = true;
            this.lblStatusMsg3.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMsg3.Location = new System.Drawing.Point(297, 277);
            this.lblStatusMsg3.Name = "lblStatusMsg3";
            this.lblStatusMsg3.Size = new System.Drawing.Size(29, 37);
            this.lblStatusMsg3.TabIndex = 8;
            this.lblStatusMsg3.Text = "..";
            // 
            // lblStatusMsg4
            // 
            this.lblStatusMsg4.AutoSize = true;
            this.lblStatusMsg4.Font = new System.Drawing.Font("Impact", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMsg4.Location = new System.Drawing.Point(499, 456);
            this.lblStatusMsg4.Name = "lblStatusMsg4";
            this.lblStatusMsg4.Size = new System.Drawing.Size(43, 59);
            this.lblStatusMsg4.TabIndex = 10;
            this.lblStatusMsg4.Text = "..";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1853, 1009);
            this.Controls.Add(this.lblInstruccion);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbMotivo);
            this.Controls.Add(this.txtMatricula);
            this.Controls.Add(this.pnlStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Control de Acceso a Biblioteca";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.ComboBox cbMotivo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.Label lblInstruccion;
        private System.Windows.Forms.Label lblStatusMsg1;
        private System.Windows.Forms.Label lblStatusMsg2;
        private System.Windows.Forms.Label lblStatusMsg4;
        private System.Windows.Forms.Label lblStatusMsg3;
    }
}

