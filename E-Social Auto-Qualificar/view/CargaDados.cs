using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using E_Social_Auto_Qualificar.controller;

namespace E_Social_Auto_Qualificar.view
{
    public partial class CargaDados : Form
    {
        controller.ProcFile pf = null;

        public CargaDados()
        {
            InitializeComponent();
        }

        private void btnProc_Click(object sender, EventArgs e)
        {
           
            btnProc.Enabled = false;

            // se nulo preencher com o default


            DialogResult result = System.Windows.Forms.MessageBox.Show("Você deseja processar este arquivo?" + (chkIncrement.Checked ? "" : " Os dados existentes no banco de dados serão apagados, se existir mais de um usuário no sistema pedir para sair."), CargaDados.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
               
                pf = new ProcFile();
                pf.ProcFileCSV(srcCSV.Text, chkIncrement.Checked, chkSobrescrever.Checked);

                if (pf.isAlive)
                {
                    //thread                    
                    lblload.Visible = true;
                    btnCSV.Enabled = false;
                    

                    timercarga.Enabled = true;
                }
                else
                {
                    MessageBox.Show(pf.Erro, CargaDados.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
                


            }
            else
            {
                btnProc.Enabled = true;
                return;
            }

            
                
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            if (openCSVFun.ShowDialog() == DialogResult.OK)
            {
                srcCSV.Text = openCSVFun.FileName;
            }
        }

        private void CargaDados_Load(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timercarga_Tick(object sender, EventArgs e)
        {
            if (!pf.isAlive)
            {
                timercarga.Enabled = false;
              
                btnProc.Enabled = true;
                btnCSV.Enabled = true;

                if (!String.IsNullOrEmpty(pf.Erro))
                    MessageBox.Show(pf.Erro, CargaDados.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Importaçã finalizada. Processados: " + pf.NumCountProc.ToString() + " / Ignorados: " + pf.NumCountIg.ToString(), CargaDados.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.None);


                pf = null;
            }

            try
            {
                lblload.Text = pf.NumLinesproc;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
