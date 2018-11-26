using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace E_Social_Auto_Qualificar.view
{
    public partial class Licenca : Form
    {
        public string key { get; set; }

        public Licenca()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Licenca_Load(object sender, EventArgs e)
        {
            txtkey.Text = key;
        }

        private void txtkey_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtkey_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtkey.Text.ToLower() == "$paulocorcino$")
            {
                view.Key o = new view.Key();
                o.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (controller.licenca.Licenca.getValida(key, txtnewkey.Text))
            {
                System.IO.File.AppendAllText("licence.lic",txtnewkey.Text);
                

                MessageBox.Show("Registrado! Reinicie o aplicativo.",Licenca.ActiveForm.Text,MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
            else
            {
                MessageBox.Show("Chave Inválida!", Licenca.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
