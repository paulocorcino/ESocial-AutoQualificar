using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using E_Social_Auto_Qualificar.model;

namespace E_Social_Auto_Qualificar
{
    public partial class Inicial : Form
    {
           
        model.dbconnect dbconn = new model.dbconnect();
        dtoConfiguracao dtoconfig = null;
        static string CurrPath = AppDomain.CurrentDomain.BaseDirectory;
        System.Threading.Thread t = null;
        Queue<string> _log = new Queue<string>();
        int counttick = 0;
        bool _pararvalidacao = false;
        int  _tpvalidacao = 0;

        //Validação        
        string _hddserial = "";
        bool _ieslicenciado = false;

        //Novidades
        bool _chkupdate = false;
        string[] _news;

        //Selecionado
        List<FuncionarioGrid> _selecionados = new List<FuncionarioGrid>();
        List<FuncionarioGrid> _listados = new List<FuncionarioGrid>();
        BindingSource _bindsourceConsulta = new BindingSource();
        BindingSource _bindsourceSelecionado = new BindingSource();
                      

        public Inicial()
        {
            InitializeComponent();                
        }
        
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            using (view.CargaDados cd = new view.CargaDados())
            {
                cd.ShowDialog();            
            }
            UpdateGraficos();
            this.TopMost = true;
         }

       

        private void btnStart_Click(object sender, EventArgs e)
        {
            ControleObjetos(false);

            ShNovidades();

            try
            {
                _tpvalidacao = (cbxAnalise.SelectedItem.ToString() == "Não Processados" ? 0 : (cbxAnalise.SelectedItem.ToString() == "Processados" ? 1 : 2));
            }
            catch (Exception ex)
            {
                cbxAnalise.SelectedItem = 0;
                _tpvalidacao = 0;
            }

            _pararvalidacao = false;
            
            //inicia uma thread
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(Processar);
            t = new System.Threading.Thread(ts);
            t.IsBackground = true;
            t.Start();

           
            timerExec.Enabled = true;
            timerExec.Start();
            
        }

     

       
        private void Inicial_Load(object sender, EventArgs e)
        {

            _hddserial = controller.licenca.Licenca.getKey1(controller.licenca.Licenca.getHdds(0).Trim());
            //_ieslicenciado = controller.licenca.Licenca.IsLicenciado(_hddserial);
            _ieslicenciado = true;

            if (!_ieslicenciado)
                Inicial.ActiveForm.Text = Inicial.ActiveForm.Text + " - *** Versão Demonstração *** ";
                        

            lblsobre.Text = lblsobre.Text.Replace("[version]", dbconnect.versao);

            var r = dbconn.CreateDB();
            if (!r)
            {
                MessageBox.Show("Falha ao criar banco de dados, veja as permissões.", Form.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //selecione o primeiro item
            cbxAnalise.SelectedIndex = 0;
            cboColumFilter.SelectedIndex = 0;            

            //Obtem o total
            UpdateGraficos();

            //Vincula o DataGrid ao DataSource
            dgConsulta.DataSource = _bindsourceConsulta;

            //Vincula o DataGrid ao DataSource 
            dgSelecionados.DataSource = _bindsourceSelecionado;

            
        }

        private void UpdateGraficos()
        {
            int total = 0;
            int totalproc = 0;
            int totalnproc = 0;
            int totalerro = 0;

            try
            {
                controller.Inicial _i = new controller.Inicial();
                _i.GetTotal(out total, out totalproc, out totalnproc, out totalerro);

            }
            catch (Exception ex)
            {
                //Nada
            }

            lblTotProc.Text = totalproc.ToString();
            lblTotNao.Text = totalnproc.ToString();
            lbltotErro.Text = totalerro.ToString();

            pbrTotProc.Value = 0;
            pbrTotNao.Value = 0;
            if(total > 0){
                double a = ((double)totalproc / (double)total) * 100.0;
                pbrTotProc.Value = (int)Math.Truncate(a);

                a = ((double)totalnproc / (double)total) * 100.0;
                pbrTotNao.Value = (int)Math.Truncate(a);
             }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
                Process.Start("http://bit.ly/CorcinoContribuicao");
            }
            catch (Exception ex)
            {
            }
        }

        private void opçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            using (view.Opcoes o = new view.Opcoes())
            {
                o.ShowDialog();
            }
            UpdateGraficos();
            this.TopMost = true;
        }

        private void ControleObjetos(bool _b)
        {
            btcancelar.Enabled = !_b;
            btcancelar.Visible = !_b;

            btnStart.Enabled = _b;
            btnLoadFile.Enabled = _b;
            menuStrip1.Enabled = _b;
            //pictureBox1.Enabled = _b;
            tbControler.Enabled = _b;
            this.TopMost = _b;
        }

        private void Processar()
        {
            bool repetir = true;
            dtoconfig = new dtoConfiguracao();
            var config = dtoconfig.GetConfiguracao();

            //prepara cadastros para validação
            if (_tpvalidacao > 0)
            {
                try
                {
                    dbconn._conn = dbconn.GetConn();

                    using (System.Data.SQLite.SQLiteTransaction transac = dbconn._conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        var sql = "UPDATE FUNCIONARIOS SET ";
                            sql += "COD_NIS_INV  = NULL,";
                            sql += "COD_CPF_INV  = NULL,";
                            sql += "COD_NOME_INV  = NULL,";
                            sql += "COD_DN_INV  = NULL,";
                            sql += "COD_CNIS_NIS  = NULL,";
                            sql += "COD_CNIS_DN  = NULL,";
                            sql += "COD_CNIS_OBITO  = NULL,";
                            sql += "COD_CNIS_CPF  = NULL,";
                            sql += "COD_CNIS_CPF_NAO_INF  = NULL,";
                            sql += "COD_CPF_NAO_CONSTA  = NULL,";
                            sql += "COD_CPF_NULO  = NULL,";
                            sql += "COD_CPF_CANCELADO  = NULL,";
                            sql += "COD_CPF_SUSPENSO  = NULL ,";
                            sql += "COD_CPF_DN  = NULL ,";
                            sql += "COD_CPF_NOME  = NULL ,";
                            sql += "COD_ORIENTACAO_CPF  = NULL ,";
                            sql += "COD_ORIENTACAO_NIS  = NULL,";
                            sql += "WEB_CPF_SIT = NULL,";
                            sql += "WEB_NIS_SIT = NULL,";
                            sql += "WEB_MSG_SIT = NULL,";
                            sql += "SYS_PROCESSADO = 0,";
                            sql += "SYS_PROBLEMS = 0 ";

                        //processados
                        if (_tpvalidacao == 1)
                            dbconn.QueryExecute(sql + " WHERE SYS_PROCESSADO = 1", null, "", dbconn._conn);

                        //com erros
                        if (_tpvalidacao == 2)
                            dbconn.QueryExecute(sql + " WHERE SYS_PROBLEMS = 1", null, "", dbconn._conn);

                        transac.Commit();
                    }

                    dbconn._conn.Close();
                }
                catch (Exception ex)
                {
                    Log("A base de dados está em uso, neste momento.");
                    return;
                }
            }

            //Conferencia Web
            if (config.MODOCHK == 'W')
            {

                controller.html.EnvelopeForm _envelopeform = null;
                controller.html.EnvelopeFormSession _envelopeformsession = null;
                List<controller.html.RetornoDataprev> listaretornos = null;
                List<model.Funcionarios> tmpfun;
                bool ret = true;

                controller.html.Qualificacao q = new controller.html.Qualificacao();


                do
                {
                    if (!_pararvalidacao)
                        ret = q.Inicializa(out _envelopeform, out _envelopeformsession);
                    else
                        ret = false;

                    //Erro o sistema deve parar
                    if (!ret || _envelopeform == null || _envelopeformsession == null)
                    {
                        if (_pararvalidacao)
                            Log("Processo interrompido pelo usuário.");
                        else
                            Log("O site deve estar fora do ar.");

                        return;
                    }

                    listaretornos = new List<controller.html.RetornoDataprev>();
                    controller.html.RetornoDataprev t = null;


                    model.dtoFuncionarios dto = new model.dtoFuncionarios();
                    List<Funcionarios> _f = null;

                     _f = dto.Get("", false, false, null, 10);                  

                    if (_f == null)
                    {
                        Log("Falha para obter os dados dos funcionários");
                        return;
                    }

                    tmpfun = new List<Funcionarios>();

                    //Adiciona a lista
                    foreach (Funcionarios f in _f)
                    {

                        //marca como em uso;
                        f.SYS_IN_USE = true;

                        //atualiza
                        dto.Update(f);

                        tmpfun.Add(f);
                    }

                    if (tmpfun.Count > 0)
                    {

                        foreach (Funcionarios f in tmpfun)
                        {

                            ret = q.ValidaCadastro(new controller.html.EnvelopePostCadastro(f.CPF, f.NIS, f.NOME, f.DN), _envelopeform, _envelopeformsession, out _envelopeformsession, out t);
                            if (ret == true)
                            {
                                if (!t.SYS_PROCESSADO)
                                    listaretornos.Add(t);
                                else
                                {
                                    //encontrou falhas
                                    f.COD_NIS_INV = t.COD_NIS_INV;
                                    f.COD_CPF_INV = t.COD_CPF_INV;
                                    f.COD_NOME_INV = t.COD_NOME_INV;
                                    f.COD_DN_INV = t.COD_DN_INV;
                                    f.COD_CNIS_NIS = t.COD_CNIS_NIS;
                                    f.COD_CNIS_DN = t.COD_CNIS_DN;
                                    f.COD_CNIS_OBITO = t.COD_CNIS_OBITO;
                                    f.COD_CNIS_CPF = t.COD_CNIS_CPF;
                                    f.COD_CNIS_CPF_NAO_INF = t.COD_CNIS_CPF_NAO_INF;
                                    f.COD_CPF_NAO_CONSTA = t.COD_CPF_NAO_CONSTA;
                                    f.COD_CPF_NULO = t.COD_CPF_NULO;
                                    f.COD_CPF_CANCELADO = t.COD_CPF_CANCELADO;
                                    f.COD_CPF_SUSPENSO = t.COD_CPF_SUSPENSO;
                                    f.COD_CPF_DN = t.COD_CPF_DN;
                                    f.COD_CPF_NOME = t.COD_CPF_NOME;
                                    f.COD_ORIENTACAO_CPF = t.COD_ORIENTACAO_CPF;
                                    f.COD_ORIENTACAO_NIS = t.COD_ORIENTACAO_NIS;
                                    f.WEB_CPF_SIT = t.WEB_CPF_SIT;
                                    f.WEB_NIS_SIT = t.WEB_NIS_SIT;
                                    f.WEB_MSG_SIT = t.WEB_MSG_SIT;
                                    f.SYS_IN_USE = false;
                                    f.SYS_PROBLEMS = t.SYS_PROBLEMS;
                                    f.SYS_PROCESSADO = t.SYS_PROCESSADO;
                                }
                            }
                            else
                            {
                                f.SYS_IN_USE = false;
                                Log(f.NOME + " - " + q.ErroMsg);
                            }

                            //libera o dado
                            dto.Update(f);
                            t = null;

                        }
                        
                        if (!_pararvalidacao) //se houver solicitacao de parar
                            ret = q.EnviaCadastro(listaretornos, _envelopeform, _envelopeformsession, out listaretornos, _ieslicenciado);
                        else
                            ret = false;

                        if (ret == false)
                        {
                            foreach (controller.html.RetornoDataprev rd in listaretornos)
                            {
                                var f = dto.Get(rd.CPF)[0];
                                if (f != null)
                                {
                                    f.SYS_PROBLEMS = false;
                                    f.SYS_IN_USE = false;
                                    f.SYS_PROCESSADO = false;
                                    dto.Update(f);
                                }

                            }

                            if (_pararvalidacao)
                            {
                                Log("Processo interrompido pelo usuário.");
                                return;
                            }
                            else
                                Log(q.ErroMsg);
                        }
                        else
                        {
                            foreach (controller.html.RetornoDataprev rd in listaretornos)
                            {
                                var f = dto.Get(rd.CPF)[0];
                                if (f != null)
                                {
                                    f.COD_NIS_INV = rd.COD_NIS_INV;
                                    f.COD_CPF_INV = rd.COD_CPF_INV;
                                    f.COD_NOME_INV = rd.COD_NOME_INV;
                                    f.COD_DN_INV = rd.COD_DN_INV;
                                    f.COD_CNIS_NIS = rd.COD_CNIS_NIS;
                                    f.COD_CNIS_DN = rd.COD_CNIS_DN;
                                    f.COD_CNIS_OBITO = rd.COD_CNIS_OBITO;
                                    f.COD_CNIS_CPF = rd.COD_CNIS_CPF;
                                    f.COD_CNIS_CPF_NAO_INF = rd.COD_CNIS_CPF_NAO_INF;
                                    f.COD_CPF_NAO_CONSTA = rd.COD_CPF_NAO_CONSTA;
                                    f.COD_CPF_NULO = rd.COD_CPF_NULO;
                                    f.COD_CPF_CANCELADO = rd.COD_CPF_CANCELADO;
                                    f.COD_CPF_SUSPENSO = rd.COD_CPF_SUSPENSO;
                                    f.COD_CPF_DN = rd.COD_CPF_DN;
                                    f.COD_CPF_NOME = rd.COD_CPF_NOME;
                                    f.COD_ORIENTACAO_CPF = rd.COD_ORIENTACAO_CPF;
                                    f.COD_ORIENTACAO_NIS = rd.COD_ORIENTACAO_NIS;
                                    f.WEB_CPF_SIT = rd.WEB_CPF_SIT;
                                    f.WEB_NIS_SIT = rd.WEB_NIS_SIT;
                                    f.WEB_MSG_SIT = rd.WEB_MSG_SIT;
                                    f.SYS_IN_USE = false;
                                    f.SYS_PROCESSADO = rd.SYS_PROCESSADO;
                                    f.SYS_PROBLEMS = rd.SYS_PROBLEMS;

                                    if (String.IsNullOrEmpty(f.WEB_CPF_SIT) || String.IsNullOrEmpty(f.WEB_NIS_SIT) || String.IsNullOrEmpty(f.WEB_MSG_SIT))
                                        if (f.SYS_PROCESSADO)
                                            f.SYS_PROCESSADO = false;



                                    dto.Update(f);

                                    Log(f.NOME + " - Processado.");
                                }

                            }
                        }

                        repetir = !q.Interromper;
                    }
                    else
                    {
                        repetir = false;
                    }

                    

                } while (repetir);
            }
            else
            {
                MessageBox.Show("Aguardamos a liberação da Dataprev para novas versões", Form.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
           
        }

        private void timerExec_Tick(object sender, EventArgs e)
        {
            if (!t.IsAlive)
            {
                //para e libera componentes
                ControleObjetos(true);
                UpdateGraficos();
                counttick = 0;

                lstLog.Items.Clear();
                lstLog.Items.AddRange(_log.ToArray());
                lstLog.TopIndex = lstLog.Items.Count - 1;

                timerExec.Enabled = false;
                timerExec.Stop();
                
                
            }

            if (counttick%10 == 0)
            {
                if (lstLog.Items.Count > 100)
                {
                    //Remove
                    _log.Dequeue();
                }
                lstLog.Items.Clear();
                lstLog.Items.AddRange(_log.ToArray());
                lstLog.TopIndex = lstLog.Items.Count - 1;

                

            }

            if (counttick % 30 == 0)
                UpdateGraficos();

            if (counttick > 3000)
                counttick = 0;

            counttick++;
           
        }

        private void Log(string _msg){

            //adiciona
            _log.Enqueue(_msg);           
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            using (view.Reports o = new view.Reports())
            {
                o.FuncionarioSelecionado(_selecionados);
                o.isLicenciado = _ieslicenciado;
                o.ShowDialog();
            }
            this.TopMost = true;
        }

        private void ajudaAtualizaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
                Process.Start("http://blog.corcino.com.br/?p=78");
            }
            catch (Exception ex)
            {
            }
        }

        private void licençaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            using (view.Licenca o = new view.Licenca())
            {
                o.key = _hddserial;
                o.ShowDialog();
            }
            this.TopMost = true;
        }

        private void btcancelar_Click(object sender, EventArgs e)
        {
            _pararvalidacao = true;
            btcancelar.Enabled = false;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
                Process.Start("http://bit.ly/CorcinoContribuicao");
            }
            catch (Exception ex)
            {
            }
        }

        private void tbControler_Selected(object sender, TabControlEventArgs e)
        {
            //executa se selecionado
            if (e.TabPage.Text == "Consultar")
            {
                var dto = new dtoFuncionarios();
                cboFiltro.Items.Clear();
                cboFiltro.Items.Add("");
                try
                {
                    cboFiltro.Items.AddRange(dto.GetListFiltros());
                }
                catch (Exception ex)
                {
                }
            }

            lblStatus.Text = "";
        }

        private void ShNovidades()
        {
            if (!_chkupdate)
            {
                //Novidades
                try
                {
                    var _downloadcontents = new System.Net.WebClient().DownloadString("https://docs.google.com/a/corcino.com.br/document/d/1Q7KqMDpXHZ7qwZ7LqhGIgUSTA3eLTUWoms4yJHNvd98/export?format=txt");
                    _news = _downloadcontents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    string _tversao = "";
                    foreach (string _v in _news)
                    {
                        if (_v.Contains("[versao]"))
                        {
                            _tversao = _v.Replace("[versao]", "").Trim();
                            break;
                        }
                    }

                    if (String.Compare(dbconnect.versao, _tversao) < 0)
                    {
                        mnuVersao.Visible = true;                        
                    }

                    _chkupdate = true;

                }
                catch (Exception ex)
                {
                }
            }
        }

        private void mnuVersao_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            using (view.Novidades o = new view.Novidades())
            {
                o.TxtNovidades = _news;
                o.ShowDialog();
            }
            this.TopMost = true;    
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            //Obtem o total
            UpdateGraficos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Validação
            if (String.IsNullOrEmpty(cboColumFilter.SelectedItem.ToString()))
            {
                MessageBox.Show("Selecione uma coluna para pesquisa.", Inicial.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty(txtTextoFiltro.Text) || txtTextoFiltro.Text.Length < 3)
            {
                MessageBox.Show("Digite no minimo 3 caracteres para pesquisar.", Inicial.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.TopMost = false;

            _listados.Clear();

            dbconn = null;
            dbconn = new dbconnect();
          
                        
            List<System.Data.SQLite.SQLiteParameter> l = new List<System.Data.SQLite.SQLiteParameter>();

            var sql = "SELECT CPF, NIS, NOME, DN, SYS_PROCESSADO, SYS_PROBLEMS, SYS_FILTRO FROM FUNCIONARIOS ";

            sql += " where " + cboColumFilter.SelectedItem.ToString() + " like @PESQ";

            l.Add(new System.Data.SQLite.SQLiteParameter("PESQ", txtTextoFiltro.Text.Replace('*','%')));

            if (cboFiltro.SelectedItem != null)
            {
                sql += " and SYS_FILTRO = @FILTRO";
                l.Add(new System.Data.SQLite.SQLiteParameter("FILTRO", cboFiltro.SelectedItem.ToString().Trim()));
            }
            
            sql = "SELECT * FROM (" + sql + ") tmp order by tmp.NOME";

            var dg = dbconn.QueryReader(sql, l);
            
            foreach (Dictionary<string, object> _d in dg)
            {
                _listados.Add(new FuncionarioGrid
                { 
                    CPF = _d["CPF"].ToString(), 
                    NIS = _d["NIS"].ToString(),
                    NOME = _d["NOME"].ToString(),
                    DN = string.Format("{0}/{1}/{2}", _d["DN"].ToString().Substring(0, 2), _d["DN"].ToString().Substring(2, 2), _d["DN"].ToString().Substring(4, 4)),
                    SITUACAO = (!Convert.ToBoolean(_d["SYS_PROCESSADO"]) ? "Não Processdo" : (Convert.ToBoolean(_d["SYS_PROBLEMS"])?"Com Erros":"OK"))
                });
            }
                   
            //Atualiza DataGrid
            if (_bindsourceConsulta.DataSource == null && _listados.Count > 0)
                _bindsourceConsulta.DataSource = _listados;
            else
                _bindsourceConsulta.ResetBindings(false);

            dgConsulta.Refresh();


            if (!(_listados.Count > 0))
            {
                MessageBox.Show("Nenhum registro encontrado.", Inicial.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            dbconn = null;
            

        }

        private void dgConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    int count = 0;
                    foreach (FuncionarioGrid _f in _listados)
                    {
                        if (count == e.RowIndex && !_selecionados.Exists(f => f.NOME == _f.NOME))
                        {
                            _selecionados.Add(_f);
                            //MessageBox.Show("Adicionado: " + _f.NOME, Inicial.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            lblStatus.Text = "Selecionado: " + _f.NOME;
                            AtualizaGradeSelecionados();
                            break;
                        }
                        else if (count == e.RowIndex && _selecionados.Exists(f => f.NOME == _f.NOME))
                        {
                            //MessageBox.Show("Você já adicionou: " + _f.NOME, Inicial.ActiveForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            lblStatus.Text = "Você já selecionou: " + _f.NOME;
                            break;
                        }

                        count++;
                    }
                }
            
        }

        private void AtualizaGradeSelecionados()
        {
            if (_bindsourceSelecionado.DataSource == null && _selecionados.Count > 0)
                _bindsourceSelecionado.DataSource = _selecionados;
            else
                _bindsourceSelecionado.ResetBindings(false);

            dgSelecionados.Refresh();
        }

        private void dgSelecionados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int count = 0;
                foreach (FuncionarioGrid _f in _selecionados)
                {
                    if (count == e.RowIndex)
                    {
                        _selecionados.Remove(_f);                       
                        lblStatus.Text = "Removido: " + _f.NOME;
                        AtualizaGradeSelecionados();
                        break;
                    }                   

                    count++;
                }
            }
        }

        private void btnImprimirSel_Click(object sender, EventArgs e)
        {
            this.reportsToolStripMenuItem_Click(null, null);
        }

        private void btnStartSel_Click(object sender, EventArgs e)
        {

        }

        
    }
}
