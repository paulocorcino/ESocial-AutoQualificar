namespace E_Social_Auto_Qualificar.view
{
    partial class Licenca
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtkey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnewkey = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "2. Envie o numero da chave abaixo";
            // 
            // txtkey
            // 
            this.txtkey.BackColor = System.Drawing.Color.White;
            this.txtkey.Location = new System.Drawing.Point(15, 121);
            this.txtkey.Multiline = true;
            this.txtkey.Name = "txtkey";
            this.txtkey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtkey.Size = new System.Drawing.Size(261, 44);
            this.txtkey.TabIndex = 3;
            this.txtkey.Enter += new System.EventHandler(this.txtkey_Enter);
            this.txtkey.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtkey_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "3. Você recebera um arquivo de validação, insira-o \r\nno campo abaixo e clique em " +
    "Registrar.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "4. Para cada novo computador que for utilizar, apenas\r\n solicite uma chave adicio" +
    "nal sem custo.";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Entre em contato e compre a licença ou para obter\r\numa chave adicional sem cus" +
    "tos.\r\n\r\nCel......: (79) 8118-9757\r\nE-Mail: paulo@corcino.com.br \r\nSkype.: corcin" +
    "os";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtnewkey
            // 
            this.txtnewkey.Location = new System.Drawing.Point(12, 218);
            this.txtnewkey.Name = "txtnewkey";
            this.txtnewkey.Size = new System.Drawing.Size(261, 20);
            this.txtnewkey.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(207, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 24);
            this.button1.TabIndex = 7;
            this.button1.Text = "Registrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Licenca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 319);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtnewkey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtkey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Licenca";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Licença";
            this.Load += new System.EventHandler(this.Licenca_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtkey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtnewkey;
        private System.Windows.Forms.Button button1;
    }
}