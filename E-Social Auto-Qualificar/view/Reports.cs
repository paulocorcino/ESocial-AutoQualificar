using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E_Social_Auto_Qualificar.model;

namespace E_Social_Auto_Qualificar.view
{
    public partial class Reports : Form
    {
        controller.reports.Reports rport = null;
        public bool isLicenciado { get; set; }

        List<FuncionarioGrid> _funcionariofelecionado = null; 

        public void FuncionarioSelecionado (object _v){
            _funcionariofelecionado = (List<FuncionarioGrid>)_v;
        }
        

        public Reports()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //forma a extensão por tipo            
            savedialog.Filter = (rdoHTML.Checked ? "Arquivo HTML|*.html" : (rdoPDF.Checked ? "Arquivo PDF|*.pdf" : (rdoCSV.Checked || rdoCSVr.Checked ? "Arquivo CSV|*.csv" : "Arquivo HTML|*.html")));
            savedialog.DefaultExt = (rdoHTML.Checked ? "html" : (rdoPDF.Checked ? "pdf" : (rdoCSV.Checked ? "csv" : (rdoCSVr.Checked ? "csvr" : "html"))));

            string filtro = (rdoErro.Checked ? "erro" : (rdoNaoProc.Checked ? "nproc" : (rdoProc.Checked ? "proc" : (rdoTodos.Checked ? "todos" : "erro"))));

            rport = new controller.reports.Reports();

            if (savedialog.ShowDialog() == DialogResult.OK)
            {   

                rport.Exportar(savedialog.DefaultExt, filtro, savedialog.FileName, isLicenciado);
                if (rport.isAlive)
                {
                    //thread
                    pbrLoad.Visible = true;
                    btncancelar.Enabled = false;
                    btnSave.Enabled = false;

                    timerSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show(rport.Erro, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void timerSave_Tick(object sender, EventArgs e)
        {
            if (!rport.isAlive)
            {
                timerSave.Enabled = false;

                if (!String.IsNullOrEmpty(rport.Erro))
                    MessageBox.Show(rport.Erro, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            if(_funcionariofelecionado.Count > 0){
                rdoSelecionado.Enabled = true;
                rdoSelecionado.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
