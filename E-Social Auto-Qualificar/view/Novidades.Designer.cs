namespace E_Social_Auto_Qualificar.view
{
    partial class Novidades
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
            this.txtNews = new System.Windows.Forms.TextBox();
            this.btndownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNews
            // 
            this.txtNews.BackColor = System.Drawing.Color.White;
            this.txtNews.Location = new System.Drawing.Point(12, 12);
            this.txtNews.Multiline = true;
            this.txtNews.Name = "txtNews";
            this.txtNews.ReadOnly = true;
            this.txtNews.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNews.Size = new System.Drawing.Size(260, 215);
            this.txtNews.TabIndex = 1;
            // 
            // btndownload
            // 
            this.btndownload.Location = new System.Drawing.Point(72, 233);
            this.btndownload.Name = "btndownload";
            this.btndownload.Size = new System.Drawing.Size(138, 23);
            this.btndownload.TabIndex = 2;
            this.btndownload.Text = "Baixe a nova versão";
            this.btndownload.UseVisualStyleBackColor = true;
            this.btndownload.Visible = false;
            this.btndownload.Click += new System.EventHandler(this.btndownload_Click);
            // 
            // Novidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 289);
            this.Controls.Add(this.btndownload);
            this.Controls.Add(this.txtNews);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Novidades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Novidades";
            this.Load += new System.EventHandler(this.Novidades_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNews;
        private System.Windows.Forms.Button btndownload;
    }
}