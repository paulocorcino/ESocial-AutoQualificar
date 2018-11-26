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
    public partial class Opcoes : Form
    {
        model.Configuracao c;
        model.dtoConfiguracao dto;
        string versao;

        public Opcoes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Opcoes_Load(object sender, EventArgs e)
        {
            dto = new model.dtoConfiguracao();
            c = dto.GetConfiguracao();

            rdoOp1.Checked = (c.MODOCHK == 'W');
            rdoOp2.Checked = (c.MODOCHK == 'L');
            versao = c.VERSAO;
            chkSound.Checked = c.SOMCAPTCHA;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.VERSAO = versao;
            c.MODOCHK = (rdoOp1.Checked ? 'W' : (rdoOp2.Checked ? 'L' : 'W'));
            c.SOMCAPTCHA = chkSound.Checked;
            c = dto.SaveConfiguracao(c);
            if (c == null)
                MessageBox.Show("Houve uma falha ao salvar, verifique se alguem está modificando as configurações.", Opcoes.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void btnDesbloq_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("**** ATENÇÃO ****\nTodo os usuários de rede devem estar fora do sistema. Confirma a execução?", CargaDados.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                model.dbconnect dbconn = new model.dbconnect();
                dbconn.QueryExecute("update funcionarios set SYS_IN_USE = 0 where SYS_IN_USE = 1");                
                dbconn = null;

                MessageBox.Show("Tarefa finalizada!", CargaDados.ActiveForm.Text, MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
            }
        }
    }
}
