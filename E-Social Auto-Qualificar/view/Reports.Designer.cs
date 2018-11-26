namespace E_Social_Auto_Qualificar.view
{
    partial class Reports
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
            this.grpTp = new System.Windows.Forms.GroupBox();
            this.rdoCSVr = new System.Windows.Forms.RadioButton();
            this.rdoCSV = new System.Windows.Forms.RadioButton();
            this.rdoPDF = new System.Windows.Forms.RadioButton();
            this.rdoHTML = new System.Windows.Forms.RadioButton();
            this.grpSit = new System.Windows.Forms.GroupBox();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.rdoProc = new System.Windows.Forms.RadioButton();
            this.rdoNaoProc = new System.Windows.Forms.RadioButton();
            this.rdoErro = new System.Windows.Forms.RadioButton();
            this.savedialog = new System.Windows.Forms.SaveFileDialog();
            this.timerSave = new System.Windows.Forms.Timer(this.components);
            this.pbrLoad = new System.Windows.Forms.ProgressBar();
            this.btncancelar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.rdoSelecionado = new System.Windows.Forms.RadioButton();
            this.grpTp.SuspendLayout();
            this.grpSit.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTp
            // 
            this.grpTp.Controls.Add(this.rdoCSVr);
            this.grpTp.Controls.Add(this.rdoCSV);
            this.grpTp.Controls.Add(this.rdoPDF);
            this.grpTp.Controls.Add(this.rdoHTML);
            this.grpTp.Location = new System.Drawing.Point(12, 12);
            this.grpTp.Name = "grpTp";
            this.grpTp.Size = new System.Drawing.Size(125, 114);
            this.grpTp.TabIndex = 0;
            this.grpTp.TabStop = false;
            this.grpTp.Text = "Tipo de saida";
            // 
            // rdoCSVr
            // 
            this.rdoCSVr.AutoSize = true;
            this.rdoCSVr.Location = new System.Drawing.Point(14, 67);
            this.rdoCSVr.Name = "rdoCSVr";
            this.rdoCSVr.Size = new System.Drawing.Size(102, 17);
            this.rdoCSVr.TabIndex = 4;
            this.rdoCSVr.Text = "CSV (Resumido)";
            this.rdoCSVr.UseVisualStyleBackColor = true;
            // 
            // rdoCSV
            // 
            this.rdoCSV.AutoSize = true;
            this.rdoCSV.Location = new System.Drawing.Point(14, 91);
            this.rdoCSV.Name = "rdoCSV";
            this.rdoCSV.Size = new System.Drawing.Size(104, 17);
            this.rdoCSV.TabIndex = 3;
            this.rdoCSV.Text = "CSV (Detalhado)";
            this.rdoCSV.UseVisualStyleBackColor = true;
            // 
            // rdoPDF
            // 
            this.rdoPDF.AutoSize = true;
            this.rdoPDF.Checked = true;
            this.rdoPDF.Location = new System.Drawing.Point(14, 19);
            this.rdoPDF.Name = "rdoPDF";
            this.rdoPDF.Size = new System.Drawing.Size(46, 17);
            this.rdoPDF.TabIndex = 2;
            this.rdoPDF.TabStop = true;
            this.rdoPDF.Text = "PDF";
            this.rdoPDF.UseVisualStyleBackColor = true;
            // 
            // rdoHTML
            // 
            this.rdoHTML.AutoSize = true;
            this.rdoHTML.Location = new System.Drawing.Point(14, 43);
            this.rdoHTML.Name = "rdoHTML";
            this.rdoHTML.Size = new System.Drawing.Size(55, 17);
            this.rdoHTML.TabIndex = 1;
            this.rdoHTML.Text = "HTML";
            this.rdoHTML.UseVisualStyleBackColor = true;
            // 
            // grpSit
            // 
            this.grpSit.Controls.Add(this.rdoSelecionado);
            this.grpSit.Controls.Add(this.rdoTodos);
            this.grpSit.Controls.Add(this.rdoProc);
            this.grpSit.Controls.Add(this.rdoNaoProc);
            this.grpSit.Controls.Add(this.rdoErro);
            this.grpSit.Location = new System.Drawing.Point(143, 12);
            this.grpSit.Name = "grpSit";
            this.grpSit.Size = new System.Drawing.Size(152, 114);
            this.grpSit.TabIndex = 4;
            this.grpSit.TabStop = false;
            this.grpSit.Text = "Situação";
            // 
            // rdoTodos
            // 
            this.rdoTodos.AutoSize = true;
            this.rdoTodos.Location = new System.Drawing.Point(14, 72);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(55, 17);
            this.rdoTodos.TabIndex = 4;
            this.rdoTodos.Text = "Todos";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // rdoProc
            // 
            this.rdoProc.AutoSize = true;
            this.rdoProc.Location = new System.Drawing.Point(14, 53);
            this.rdoProc.Name = "rdoProc";
            this.rdoProc.Size = new System.Drawing.Size(86, 17);
            this.rdoProc.TabIndex = 3;
            this.rdoProc.Text = "Processados";
            this.rdoProc.UseVisualStyleBackColor = true;
            // 
            // rdoNaoProc
            // 
            this.rdoNaoProc.AutoSize = true;
            this.rdoNaoProc.Location = new System.Drawing.Point(14, 34);
            this.rdoNaoProc.Name = "rdoNaoProc";
            this.rdoNaoProc.Size = new System.Drawing.Size(109, 17);
            this.rdoNaoProc.TabIndex = 2;
            this.rdoNaoProc.Text = "Não Processados";
            this.rdoNaoProc.UseVisualStyleBackColor = true;
            // 
            // rdoErro
            // 
            this.rdoErro.AutoSize = true;
            this.rdoErro.Checked = true;
            this.rdoErro.Location = new System.Drawing.Point(14, 15);
            this.rdoErro.Name = "rdoErro";
            this.rdoErro.Size = new System.Drawing.Size(73, 17);
            this.rdoErro.TabIndex = 1;
            this.rdoErro.TabStop = true;
            this.rdoErro.Text = "Com Erros";
            this.rdoErro.UseVisualStyleBackColor = true;
            // 
            // savedialog
            // 
            this.savedialog.RestoreDirectory = true;
            this.savedialog.Title = "Relatórios";
            // 
            // timerSave
            // 
            this.timerSave.Interval = 500;
            this.timerSave.Tick += new System.EventHandler(this.timerSave_Tick);
            // 
            // pbrLoad
            // 
            this.pbrLoad.Location = new System.Drawing.Point(42, 132);
            this.pbrLoad.Name = "pbrLoad";
            this.pbrLoad.Size = new System.Drawing.Size(67, 23);
            this.pbrLoad.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbrLoad.TabIndex = 9;
            this.pbrLoad.Visible = false;
            // 
            // btncancelar
            // 
            this.btncancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancelar.Image = global::E_Social_Auto_Qualificar.Properties.Resources.cancel;
            this.btncancelar.Location = new System.Drawing.Point(223, 132);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(75, 27);
            this.btncancelar.TabIndex = 8;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btncancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btncancelar.UseVisualStyleBackColor = true;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::E_Social_Auto_Qualificar.Properties.Resources.disk;
            this.btnSave.Location = new System.Drawing.Point(143, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Salvar";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rdoSelecionado
            // 
            this.rdoSelecionado.AutoSize = true;
            this.rdoSelecionado.Enabled = false;
            this.rdoSelecionado.Location = new System.Drawing.Point(14, 91);
            this.rdoSelecionado.Name = "rdoSelecionado";
            this.rdoSelecionado.Size = new System.Drawing.Size(89, 17);
            this.rdoSelecionado.TabIndex = 5;
            this.rdoSelecionado.Text = "Selecionados";
            this.rdoSelecionado.UseVisualStyleBackColor = true;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 171);
            this.ControlBox = false;
            this.Controls.Add(this.pbrLoad);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpSit);
            this.Controls.Add(this.grpTp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reports";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relatórios";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Reports_Load);
            this.grpTp.ResumeLayout(false);
            this.grpTp.PerformLayout();
            this.grpSit.ResumeLayout(false);
            this.grpSit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTp;
        private System.Windows.Forms.RadioButton rdoCSV;
        private System.Windows.Forms.RadioButton rdoPDF;
        private System.Windows.Forms.RadioButton rdoHTML;
        private System.Windows.Forms.GroupBox grpSit;
        private System.Windows.Forms.RadioButton rdoProc;
        private System.Windows.Forms.RadioButton rdoNaoProc;
        private System.Windows.Forms.RadioButton rdoErro;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.SaveFileDialog savedialog;
        private System.Windows.Forms.Timer timerSave;
        private System.Windows.Forms.ProgressBar pbrLoad;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.RadioButton rdoCSVr;
        private System.Windows.Forms.RadioButton rdoSelecionado;
    }
}