namespace E_Social_Auto_Qualificar.view
{
    partial class Captcha
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
            this.txtCaptha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.imgCap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgCap)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCaptha
            // 
            this.txtCaptha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptha.Location = new System.Drawing.Point(25, 168);
            this.txtCaptha.Name = "txtCaptha";
            this.txtCaptha.Size = new System.Drawing.Size(80, 26);
            this.txtCaptha.TabIndex = 1;
            this.txtCaptha.Enter += new System.EventHandler(this.txtCaptha_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Informe os caracteres que aparecem acima";
            // 
            // button2
            // 
            this.button2.AccessibleName = "confirmar";
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.button2.Image = global::E_Social_Auto_Qualificar.Properties.Resources.cancel;
            this.button2.Location = new System.Drawing.Point(61, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 26);
            this.button2.TabIndex = 5;
            this.button2.Text = "Parar Operação";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleName = "confirmar";
            this.btnRefresh.Image = global::E_Social_Auto_Qualificar.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(184, 163);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(49, 38);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button1
            // 
            this.button1.AccessibleName = "confirmar";
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = global::E_Social_Auto_Qualificar.Properties.Resources.accept;
            this.button1.Location = new System.Drawing.Point(120, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 38);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imgCap
            // 
            this.imgCap.Location = new System.Drawing.Point(12, 12);
            this.imgCap.Name = "imgCap";
            this.imgCap.Size = new System.Drawing.Size(240, 120);
            this.imgCap.TabIndex = 0;
            this.imgCap.TabStop = false;
            // 
            // Captcha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 261);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCaptha);
            this.Controls.Add(this.imgCap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Captcha";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captcha";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Captcha_FormClosed);
            this.Load += new System.EventHandler(this.Captcha_Load);
            this.Shown += new System.EventHandler(this.Captcha_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.imgCap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgCap;
        private System.Windows.Forms.TextBox txtCaptha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button button2;
    }
}