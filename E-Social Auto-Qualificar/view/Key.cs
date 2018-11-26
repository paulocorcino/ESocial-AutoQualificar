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
    public partial class Key : Form
    {
        public Key()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _newkey = clientkey.Text.Trim() + controller.licenca.Licenca.keymaster;
            _newkey = controller.licenca.Licenca.EncryptMd5(_newkey);

            newkey.Text = _newkey;
        }
    }
}
