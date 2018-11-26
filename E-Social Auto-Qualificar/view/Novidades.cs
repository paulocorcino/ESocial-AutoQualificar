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
    public partial class Novidades : Form
    {
        public string[] TxtNovidades { get; set; }
        string _lnkdownload = "";
        string _versao = "";

        public Novidades()
        {
            InitializeComponent();
        }

        private void Novidades_Load(object sender, EventArgs e)
        {
            List<string> _shw = new List<string>();
            _lnkdownload = "";

            for (int i = 0; i < TxtNovidades.Length; i++)
            {
                if (TxtNovidades[i].Contains("[download]") && TxtNovidades[i].Contains("http://"))
                {
                    //linha do download
                    _lnkdownload = TxtNovidades[i].Replace("[download]", "");
                }
                else
                {
                    if (TxtNovidades[i].Contains("[versao]"))
                    {
                        _versao = TxtNovidades[i].Replace("[versao]", "").Trim();
                    }

                    _shw.Add(TxtNovidades[i].Replace("[versao]","").Replace("[download]","").Replace("<br>", ""));
                }
            }

            txtNews.Lines = _shw.ToArray();

            if (_lnkdownload != "" && String.Compare(E_Social_Auto_Qualificar.model.dbconnect.versao, _versao) < 0)
            {
                btndownload.Visible = true;
            }

        }
               

        private void btndownload_Click(object sender, EventArgs e)
        {
            try
            {                
                System.Diagnostics.Process.Start(_lnkdownload);
                
            }
            catch (Exception ex)
            {
            }
        }
    }
}
