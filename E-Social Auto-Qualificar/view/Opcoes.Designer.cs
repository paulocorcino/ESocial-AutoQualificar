namespace E_Social_Auto_Qualificar.view
{
    partial class Opcoes
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
            this.rdoOp1 = new System.Windows.Forms.RadioButton();
            this.gpoValid = new System.Windows.Forms.GroupBox();
            this.rdoOp2 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkSound = new System.Windows.Forms.CheckBox();
            this.btnDesbloq = new System.Windows.Forms.Button();
            this.gpoValid.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoOp1
            // 
            this.rdoOp1.AutoSize = true;
            this.rdoOp1.Location = new System.Drawing.Point(6, 19);
            this.rdoOp1.Name = "rdoOp1";
            this.rdoOp1.Size = new System.Drawing.Size(78, 17);
            this.rdoOp1.TabIndex = 0;
            this.rdoOp1.TabStop = true;
            this.rdoOp1.Text = "Modo Web";
            this.rdoOp1.UseVisualStyleBackColor = true;
            // 
            // gpoValid
            // 
            this.gpoValid.Controls.Add(this.rdoOp2);
            this.gpoValid.Controls.Add(this.rdoOp1);
            this.gpoValid.Location = new System.Drawing.Point(12, 12);
            this.gpoValid.Name = "gpoValid";
            this.gpoValid.Size = new System.Drawing.Size(129, 66);
            this.gpoValid.TabIndex = 1;
            this.gpoValid.TabStop = false;
            this.gpoValid.Text = "Modo Validção";
            // 
            // rdoOp2
            // 
            this.rdoOp2.AutoSize = true;
            this.rdoOp2.Enabled = false;
            this.rdoOp2.Location = new System.Drawing.Point(6, 42);
            this.rdoOp2.Name = "rdoOp2";
            this.rdoOp2.Size = new System.Drawing.Size(76, 17);
            this.rdoOp2.TabIndex = 1;
            this.rdoOp2.TabStop = true;
            this.rdoOp2.Text = "Modo Lote";
            this.rdoOp2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = global::E_Social_Auto_Qualificar.Properties.Resources.disk;
            this.button1.Location = new System.Drawing.Point(156, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "Salvar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::E_Social_Auto_Qualificar.Properties.Resources.cancel;
            this.button2.Location = new System.Drawing.Point(229, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancelar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkSound
            // 
            this.chkSound.AutoSize = true;
            this.chkSound.Location = new System.Drawing.Point(12, 84);
            this.chkSound.Name = "chkSound";
            this.chkSound.Size = new System.Drawing.Size(132, 17);
            this.chkSound.TabIndex = 4;
            this.chkSound.Text = "Som no exibir Captcha";
            this.chkSound.UseVisualStyleBackColor = true;
            // 
            // btnDesbloq
            // 
            this.btnDesbloq.Image = global::E_Social_Auto_Qualificar.Properties.Resources.asterisk_yellow;
            this.btnDesbloq.Location = new System.Drawing.Point(156, 17);
            this.btnDesbloq.Name = "btnDesbloq";
            this.btnDesbloq.Size = new System.Drawing.Size(149, 31);
            this.btnDesbloq.TabIndex = 5;
            this.btnDesbloq.Text = "Desbloquear Registros";
            this.btnDesbloq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDesbloq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDesbloq.UseVisualStyleBackColor = true;
            this.btnDesbloq.Click += new System.EventHandler(this.btnDesbloq_Click);
            // 
            // Opcoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 161);
            this.ControlBox = false;
            this.Controls.Add(this.btnDesbloq);
            this.Controls.Add(this.chkSound);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gpoValid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Opcoes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opções";
            this.Load += new System.EventHandler(this.Opcoes_Load);
            this.gpoValid.ResumeLayout(false);
            this.gpoValid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoOp1;
        private System.Windows.Forms.GroupBox gpoValid;
        private System.Windows.Forms.RadioButton rdoOp2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkSound;
        private System.Windows.Forms.Button btnDesbloq;
    }
}