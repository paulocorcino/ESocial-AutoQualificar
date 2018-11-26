namespace E_Social_Auto_Qualificar.view
{
    partial class CargaDados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnProc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.srcCSV = new System.Windows.Forms.TextBox();
            this.btnCSV = new System.Windows.Forms.Button();
            this.openCSVFun = new System.Windows.Forms.OpenFileDialog();
            this.chkIncrement = new System.Windows.Forms.CheckBox();
            this.chkSobrescrever = new System.Windows.Forms.CheckBox();
            this.btncancelar = new System.Windows.Forms.Button();
            this.timercarga = new System.Windows.Forms.Timer(this.components);
            this.lblload = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnProc
            // 
            this.btnProc.Image = global::E_Social_Auto_Qualificar.Properties.Resources.cog_go;
            this.btnProc.Location = new System.Drawing.Point(321, 90);
            this.btnProc.Name = "btnProc";
            this.btnProc.Size = new System.Drawing.Size(85, 26);
            this.btnProc.TabIndex = 11;
            this.btnProc.Text = "Processar";
            this.btnProc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProc.UseVisualStyleBackColor = true;
            this.btnProc.Click += new System.EventHandler(this.btnProc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Selecione um arquivo com a relação de funcionários.";
            // 
            // srcCSV
            // 
            this.srcCSV.Location = new System.Drawing.Point(15, 39);
            this.srcCSV.Name = "srcCSV";
            this.srcCSV.Size = new System.Drawing.Size(367, 20);
            this.srcCSV.TabIndex = 9;
            // 
            // btnCSV
            // 
            this.btnCSV.Image = global::E_Social_Auto_Qualificar.Properties.Resources.folder;
            this.btnCSV.Location = new System.Drawing.Point(388, 34);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(99, 29);
            this.btnCSV.TabIndex = 8;
            this.btnCSV.Text = "Funcionários";
            this.btnCSV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCSV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCSV.UseVisualStyleBackColor = true;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // openCSVFun
            // 
            this.openCSVFun.DefaultExt = "csv";
            this.openCSVFun.Filter = "Arquivo CSV|*.csv|Arquivo TXT|*.txt";
            // 
            // chkIncrement
            // 
            this.chkIncrement.AutoSize = true;
            this.chkIncrement.Checked = true;
            this.chkIncrement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncrement.Location = new System.Drawing.Point(15, 65);
            this.chkIncrement.Name = "chkIncrement";
            this.chkIncrement.Size = new System.Drawing.Size(124, 17);
            this.chkIncrement.TabIndex = 12;
            this.chkIncrement.Text = "Inclusão Incremental";
            this.chkIncrement.UseVisualStyleBackColor = true;
            // 
            // chkSobrescrever
            // 
            this.chkSobrescrever.AutoSize = true;
            this.chkSobrescrever.Location = new System.Drawing.Point(145, 65);
            this.chkSobrescrever.Name = "chkSobrescrever";
            this.chkSobrescrever.Size = new System.Drawing.Size(132, 17);
            this.chkSobrescrever.TabIndex = 13;
            this.chkSobrescrever.Text = "Sobrescrever se existir";
            this.chkSobrescrever.UseVisualStyleBackColor = true;
            // 
            // btncancelar
            // 
            this.btncancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancelar.Image = global::E_Social_Auto_Qualificar.Properties.Resources.cancel;
            this.btncancelar.Location = new System.Drawing.Point(412, 90);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(75, 27);
            this.btncancelar.TabIndex = 14;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btncancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btncancelar.UseVisualStyleBackColor = true;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // timercarga
            // 
            this.timercarga.Interval = 500;
            this.timercarga.Tick += new System.EventHandler(this.timercarga_Tick);
            // 
            // lblload
            // 
            this.lblload.AutoSize = true;
            this.lblload.Location = new System.Drawing.Point(12, 107);
            this.lblload.Name = "lblload";
            this.lblload.Size = new System.Drawing.Size(19, 13);
            this.lblload.TabIndex = 16;
            this.lblload.Text = "....";
            this.lblload.Visible = false;
            // 
            // CargaDados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 129);
            this.ControlBox = false;
            this.Controls.Add(this.lblload);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.chkSobrescrever);
            this.Controls.Add(this.chkIncrement);
            this.Controls.Add(this.btnProc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.srcCSV);
            this.Controls.Add(this.btnCSV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CargaDados";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Carga de Dados";
            this.Load += new System.EventHandler(this.CargaDados_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox srcCSV;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.OpenFileDialog openCSVFun;
        private System.Windows.Forms.CheckBox chkIncrement;
        private System.Windows.Forms.CheckBox chkSobrescrever;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.Timer timercarga;
        private System.Windows.Forms.Label lblload;
    }
}