using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using E_Social_Auto_Qualificar.model;

namespace E_Social_Auto_Qualificar.controller.reports
{
    class Reports
    {
        bool _isAlive;
        string _erro;

        //parametros thread
        string _tformato;
        string _tfiltro;
        string _tsrcfile;
        string _tsrcfilepdf;
        bool _islicen = false;

        public bool isAlive { get { return _isAlive; } }
        public string Erro { get { return _erro; } }

        Thread t = null;

        public void Exportar(string _formato, string _filtro, string _srcfile, bool _ieslicenciado)
        {
            //se nulo
            if (String.IsNullOrEmpty(_srcfile))
            {
                _erro = "Usuário não definiu o caminho do arquivo.";
                _isAlive = false;
                return;
            }

            _islicen = _ieslicenciado;


            _tformato = _formato;
            _tfiltro = _filtro;
            _tsrcfile = _srcfile;

            _erro = "";

            //Inicia trhread
            ThreadStart ts = new ThreadStart(Processar);
            t = new Thread(ts);
            t.IsBackground = true;
            t.Start();

            
            _isAlive = true;


        }

        public void Processar()
        {
           //Obtem os dados
            model.dtoFuncionarios dtofun = new model.dtoFuncionarios();
            List<Funcionarios> f = null;
            
            // com erro
            if(_tfiltro == "erro")
                f = dtofun.Get("", null, null, true);

            // processado
            if(_tfiltro == "proc")
                f = dtofun.Get("", null, true, null);
            
            // nao processado
            if(_tfiltro == "nproc")
                f = dtofun.Get("", null, false, null);

            // todos
            if (_tfiltro == "todos")
                f = dtofun.Get("", null, null, null);

            if (f == null || f.Count < 1)
            {
                _erro = "Não foi encontrado dados.";
                _isAlive = false;                
                return;
            }


  

            StringBuilder sb = null;
            int count = 1;
            try
            {
                //Se existir delete
                if (File.Exists(_tsrcfile))
                    File.Delete(_tsrcfile);

                //se pdf
                if (_tformato == "pdf")
                {
                    _tsrcfilepdf = _tsrcfile;
                    _tsrcfile = Path.GetTempFileName().Replace(".tmp",".html");
                }

                using (System.IO.StreamWriter txt = new System.IO.StreamWriter(_tsrcfile, false, Encoding.UTF8))
                {
                    if (!_tformato.Contains("csv"))
                    {
                        txt.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\"><head><title>Relat&oacute;rio</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"></head><body bgcolor=\"#FFFFFF\" text=\"#000000\">");
                    }

                    foreach (Funcionarios ff in f)
                    {


                        //Template
                        if (_tformato.Contains("csv"))
                        {
                            sb = new StringBuilder();

                            
                                //CSV Detalhado
                                sb.AppendFormat("\"{0}\";", ff.CPF);
                                sb.AppendFormat("\"{0}\";", ff.NIS);
                                sb.AppendFormat("\"{0}\";", ff.NOME);
                                sb.AppendFormat("\"{0}\";", ff.DN);

                                if (_tformato == "csv")
                                {
                                    sb.AppendFormat("\"{0}\";", (ff.COD_NIS_INV == null ? "" : (ff.COD_NIS_INV == false ? "OK" : "NIS inválido")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_INV == null ? "" : (ff.COD_CPF_INV == false ? "OK" : "CPF inválido")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_NOME_INV == null ? "" : (ff.COD_NOME_INV == false ? "OK" : "NOME inválido")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_DN_INV == null ? "" : (ff.COD_DN_INV == false ? "OK" : "DN inválida")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CNIS_NIS == null ? "" : (ff.COD_CNIS_NIS == false ? "OK" : "NIS inconsistente")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CNIS_DN == null ? "" : (ff.COD_CNIS_DN == false ? "OK" : "Data de Nascimento informada diverge da existente no CNIS.")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CNIS_OBITO == null ? "" : (ff.COD_CNIS_OBITO == false ? "OK" : "NIS com óbito no CNIS")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CNIS_CPF == null ? "" : (ff.COD_CNIS_CPF == false ? "OK" : "CPF informado diverge do existente no CNIS")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CNIS_CPF_NAO_INF == null ? "" : (ff.COD_CNIS_CPF_NAO_INF == false ? "OK" : "CPF não preenchido no CNIS")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_NAO_CONSTA == null ? "" : (ff.COD_CPF_NAO_CONSTA == false ? "OK" : "CPF informado não consta no Cadastro CPF")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_NULO == null ? "" : (ff.COD_CPF_NULO == false ? "OK" : "CPF informado NULO no Cadastro CPF")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_CANCELADO == null ? "" : (ff.COD_CPF_CANCELADO == false ? "OK" : "CPF informado CANCELADO no Cadastro CPF")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_SUSPENSO == null ? "" : (ff.COD_CPF_SUSPENSO == false ? "OK" : "CPF informado SUSPENSO no Cadastro CPF")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_DN == null ? "" : (ff.COD_CPF_DN == false ? "OK" : "Data de Nascimento informada diverge da existente no Cadastro CPF.")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_CPF_NOME == null ? "" : (ff.COD_CPF_NOME == false ? "OK" : "NOME informado diverge do existente no Cadastro CPF.")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_ORIENTACAO_CPF == null ? "" : (ff.COD_ORIENTACAO_CPF == false ? "OK" : "Procurar Conveniadas da RFB  Atualizar o CPF em uma agência do Banco do Brasil, da CAIXA ou dos CORREIOS")));
                                    sb.AppendFormat("\"{0}\";", (ff.COD_ORIENTACAO_NIS == null ? "" : (ff.COD_ORIENTACAO_NIS == 0 ? "OK" : (ff.COD_ORIENTACAO_NIS == 1 ? "Atualizar NIS no INSS Ligar para 135 e agendar o atendimento em uma Agência da Previdência Social." : (ff.COD_ORIENTACAO_NIS == 2 ? "Atualizar o Cadastro NIS em uma agência da CAIXA" : "Atualizar o Cadastro NIS em uma agência do Banco do Brasil.")))));
                                    sb.AppendFormat("\"{0}\";", ff.WEB_CPF_SIT);
                                    sb.AppendFormat("\"{0}\";", ff.WEB_NIS_SIT);
                                    sb.AppendFormat("\"{0}\";", ff.WEB_MSG_SIT);
                                }
                                else
                                {
                                    //Situacao CPF
                                    if (String.IsNullOrEmpty(ff.WEB_CPF_SIT))
                                    {
                                        ff.WEB_CPF_SIT = "";
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_INV == true ? " CPF inválido. " : "");
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_NAO_CONSTA == true ? " CPF informado não consta no Cadastro CPF. " : "");
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_NULO == true ? " CPF informado NULO no Cadastro CPF. " : "");
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_CANCELADO == true ? " CPF informado CANCELADO no Cadastro CPF. " : "");
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_SUSPENSO == true ? " CPF informado SUSPENSO no Cadastro CPF. " : "");
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_DN == true ? " Data de Nascimento informada diverge da existente no Cadastro CPF. " : "");
                                        ff.WEB_CPF_SIT += (ff.COD_CPF_NOME == true ? " NOME informado diverge do existente no Cadastro CPF. " : "");


                                        if (ff.WEB_CPF_SIT == "" && ff.SYS_PROCESSADO)
                                            ff.WEB_CPF_SIT = "Válido.";

                                    }

                                    sb.AppendFormat("\"{0}\";", ff.WEB_CPF_SIT);

                                    ////////////////////////////////////////////////////////////

                                    // NIS //////////////////////////

                                    if (String.IsNullOrEmpty(ff.WEB_NIS_SIT))
                                    {
                                        ff.WEB_NIS_SIT = "";
                                        ff.WEB_NIS_SIT += (ff.COD_NIS_INV == true ? " NIS inválido. " : "");
                                        ff.WEB_NIS_SIT += (ff.COD_CNIS_NIS == true ? " NIS inconsistente. " : "");
                                        ff.WEB_NIS_SIT += (ff.COD_CNIS_DN == true ? " Data de Nascimento informada diverge da existente no CNIS. " : "");
                                        ff.WEB_NIS_SIT += (ff.COD_CNIS_OBITO == true ? " NIS com óbito no CNIS. " : "");
                                        ff.WEB_NIS_SIT += (ff.COD_CNIS_CPF == true ? " CPF informado diverge do existente no CNIS. " : "");
                                        ff.WEB_NIS_SIT += (ff.COD_CNIS_CPF_NAO_INF == true ? " CPF não preenchido no CNIS. " : "");

                                        if (ff.WEB_NIS_SIT == "" && ff.SYS_PROCESSADO)
                                            ff.WEB_NIS_SIT = "Válido.";

                                    }

                                    sb.AppendFormat("\"{0}\";", ff.WEB_NIS_SIT);

                                    /////////////////////////////////////////////////////////////////////

                                    if (String.IsNullOrEmpty(ff.WEB_MSG_SIT))
                                    {
                                        ff.WEB_MSG_SIT = "";

                                        //reparar bug da falta de analise
                                        if (ff.COD_NIS_INV == true)
                                            ff.COD_ORIENTACAO_NIS = 1;

                                        if (ff.COD_CPF_INV == true)
                                            ff.COD_ORIENTACAO_CPF = true;

                                        if ((ff.COD_NOME_INV == true || ff.COD_DN_INV == true) && (ff.COD_NIS_INV == null || ff.COD_CPF_INV == null))
                                        {
                                            ff.COD_ORIENTACAO_CPF = null;
                                            ff.COD_ORIENTACAO_NIS = null;
                                        }

                                        ff.WEB_MSG_SIT += (ff.COD_ORIENTACAO_CPF == true ? " Procurar Conveniadas da RFB  Atualizar o CPF em uma agência do Banco do Brasil, da CAIXA ou dos CORREIOS" : "");
                                        ff.WEB_MSG_SIT += (ff.COD_ORIENTACAO_NIS == null ? "" : (ff.COD_ORIENTACAO_NIS == 0 ? "" : (ff.COD_ORIENTACAO_NIS == 1 ? "Atualizar NIS no INSS Ligar para 135 e agendar o atendimento em uma Agência da Previdência Social." : (ff.COD_ORIENTACAO_NIS == 2 ? "Atualizar o Cadastro NIS em uma agência da CAIXA" : "Atualizar o Cadastro NIS em uma agência do Banco do Brasil."))));
                                        ff.WEB_MSG_SIT += (ff.COD_NOME_INV == null ? "" : (ff.COD_NOME_INV == false ? "" : " ** NOME inválido **"));
                                        ff.WEB_MSG_SIT += (ff.COD_DN_INV == null ? "" : (ff.COD_DN_INV == false ? "" : " ** DATA NASCIMENTO inválida **"));

                                        if (ff.WEB_MSG_SIT == "" && ff.SYS_PROCESSADO)
                                            ff.WEB_MSG_SIT = "Os dados estão corretos.";

                                    }
                                    else
                                    {
                                        ff.WEB_MSG_SIT += (ff.COD_NOME_INV == null ? "" : (ff.COD_NOME_INV == false ? "" : " ** NOME inválido **"));
                                        ff.WEB_MSG_SIT += (ff.COD_DN_INV == null ? "" : (ff.COD_DN_INV == false ? "" : " ** DATA NASCIMENTO inválida **"));
                                                                                
                                    }

                                    sb.AppendFormat("\"{0}\";", ff.WEB_MSG_SIT);

                                }

                                sb.AppendFormat("\"{0}\";", ff.SYS_FILTRO);
                                sb.AppendFormat("\"{0}\";", (ff.SYS_PROCESSADO ? "PROCESSADO" : "NAO PROCESSADO"));
                                sb.AppendFormat("\"{0}\";", (ff.SYS_PROBLEMS ? "COM ERROS" : "OK"));
                            
                           

                            txt.WriteLine(sb.ToString());
                        }
                        else
                        {
                            //HTML ou PDF
                            sb = new StringBuilder();
                            sb.AppendFormat("<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"3\" bordercolor=\"#000000\">");
                            sb.AppendFormat("<tr> ");
                            
                            if (String.IsNullOrEmpty(ff.SYS_FILTRO))
                            {
                                sb.AppendFormat("<td colspan=\"3\"><font face=\"Arial, Helvetica, sans-serif\"><b><font size=\"2\">{0}</font></b></font></td>", System.Net.WebUtility.HtmlEncode(ff.NOME));
                            }
                            else
                            {
                                sb.AppendFormat("<td colspan=\"3\">");
                                sb.AppendFormat("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                                sb.AppendFormat("<tr><td><font face=\"Arial, Helvetica, sans-serif\"><b><font size=\"2\">{0}</font></b></font></td>", System.Net.WebUtility.HtmlEncode(ff.NOME));
                                sb.AppendFormat("<td width=\"25%\" style=\"border: 1px #000000 solid; padding-top: 3px; padding-right: 3px; padding-bottom: 3px; padding-left: 3px\">");
                                sb.AppendFormat("<font face=\"Arial, Helvetica, sans-serif\" size=\"1\"><b>{0}</b></font></td></tr></table>", System.Net.WebUtility.HtmlEncode(ff.SYS_FILTRO));
                                sb.AppendFormat("</td>");
                            }

                            sb.AppendFormat("</tr>");
                            sb.AppendFormat("<tr> ");
                            sb.AppendFormat("<td width=\"25%\"><font size=\"1\" face=\"Arial, Helvetica, sans-serif\"><b>CPF</b></font><font face=\"Arial, Helvetica, sans-serif\"><br>");
                            sb.AppendFormat("<font size=\"2\">{0} </font></font></td>", (!String.IsNullOrEmpty(ff.CPF) && ff.CPF.Length >= 11?string.Format("{0}.{1}.{2}-{3}", ff.CPF.Substring(0, 3), ff.CPF.Substring(3, 3), ff.CPF.Substring(6, 3), ff.CPF.Substring(9, 2)):""));
                            sb.AppendFormat("<td width=\"25%\"><font size=\"1\" face=\"Arial, Helvetica, sans-serif\"><b>NIS</b></font><font face=\"Arial, Helvetica, sans-serif\"><br>");
                            sb.AppendFormat("<font size=\"2\">{0} </font></font></td>", (!String.IsNullOrEmpty(ff.NIS) && ff.NIS.Length >= 11?string.Format("{0}.{1}.{2}-{3}", ff.NIS.Substring(0, 3), ff.NIS.Substring(3, 5), ff.NIS.Substring(8, 2), ff.NIS.Substring(10, 1)):""));
                            sb.AppendFormat("<td><font size=\"1\" face=\"Arial, Helvetica, sans-serif\"><b>DATA NASCIMENTO</b></font><font face=\"Arial, Helvetica, sans-serif\"><br>");
                            sb.AppendFormat("<font size=\"2\">{0} </font></font></td>", (!String.IsNullOrEmpty(ff.DN) && ff.DN.Length == 8 ? string.Format("{0}/{1}/{2}", ff.DN.Substring(0, 2), ff.DN.Substring(2, 2), ff.DN.Substring(4, 4)) : ""));
                            sb.AppendFormat("</tr>");
                            sb.AppendFormat("<tr> ");
                            sb.AppendFormat("<td><font size=\"1\" face=\"Arial, Helvetica, sans-serif\"><b>SITUA&Ccedil;&Atilde;O ");
                            sb.AppendFormat("CPF</b></font><font face=\"Arial, Helvetica, sans-serif\"><br>");

                            //Situacao CPF
                            var sit = "";

                            if (String.IsNullOrEmpty(ff.WEB_CPF_SIT))
                            {

                                sit += (ff.COD_CPF_INV == true ? " CPF inválido. " : "");
                                sit += (ff.COD_CPF_NAO_CONSTA == true ? " CPF informado não consta no Cadastro CPF. " : "");
                                sit += (ff.COD_CPF_NULO == true ? " CPF informado NULO no Cadastro CPF. " : "");
                                sit += (ff.COD_CPF_CANCELADO == true ? " CPF informado CANCELADO no Cadastro CPF. " : "");
                                sit += (ff.COD_CPF_SUSPENSO == true ? " CPF informado SUSPENSO no Cadastro CPF. " : "");
                                sit += (ff.COD_CPF_DN == true ? " Data de Nascimento informada diverge da existente no Cadastro CPF. " : "");
                                sit += (ff.COD_CPF_NOME == true ? " NOME informado diverge do existente no Cadastro CPF. " : "");


                                if (sit == "" && ff.SYS_PROCESSADO)
                                    sit = "Válido.";

                            }
                            else
                            {
                                sit = ff.WEB_CPF_SIT;
                            }


                            sb.AppendFormat("<font size=\"2\">{0}</font></font></td>", System.Net.WebUtility.HtmlDecode(sit));
                            sb.AppendFormat("<td><font size=\"1\" face=\"Arial, Helvetica, sans-serif\"><b>SITUA&Ccedil;&Atilde;O ");
                            sb.AppendFormat("NIS</b></font><font face=\"Arial, Helvetica, sans-serif\"><br>");

                            //Situação NIS
                            sit = "";

                            if (String.IsNullOrEmpty(ff.WEB_NIS_SIT))
                            {
                                sit += (ff.COD_NIS_INV == true ? " NIS inválido. " : "");
                                sit += (ff.COD_CNIS_NIS == true ? " NIS inconsistente. " : "");
                                sit += (ff.COD_CNIS_DN == true ? " Data de Nascimento informada diverge da existente no CNIS. " : "");
                                sit += (ff.COD_CNIS_OBITO == true ? " NIS com óbito no CNIS. " : "");
                                sit += (ff.COD_CNIS_CPF == true ? " CPF informado diverge do existente no CNIS. " : "");
                                sit += (ff.COD_CNIS_CPF_NAO_INF == true ? " CPF não preenchido no CNIS. " : "");

                                if (sit == "" && ff.SYS_PROCESSADO)
                                    sit = "Válido.";

                            }
                            else
                            {
                                sit = ff.WEB_NIS_SIT;
                            }

                            sb.AppendFormat("<font size=\"2\">{0}</font></font></td>", System.Net.WebUtility.HtmlDecode(sit));
                            sb.AppendFormat("<td><font size=\"1\" face=\"Arial, Helvetica, sans-serif\"><b>MENSAGEM</b></font><font face=\"Arial, Helvetica, sans-serif\"><br>");

                            //Situacao Mensagem
                            sit = "";

                            if (String.IsNullOrEmpty(ff.WEB_MSG_SIT))
                            {
                                //reparar bug da falta de analise
                                if (ff.COD_NIS_INV == true)
                                    ff.COD_ORIENTACAO_NIS = 1;

                                if (ff.COD_CPF_INV == true)
                                    ff.COD_ORIENTACAO_CPF = true;

                                if ((ff.COD_NOME_INV == true || ff.COD_DN_INV == true) && (ff.COD_NIS_INV == null || ff.COD_CPF_INV == null))
                                {
                                    ff.COD_ORIENTACAO_CPF = null;
                                    ff.COD_ORIENTACAO_NIS = null;
                                }

                                sit += (ff.COD_ORIENTACAO_CPF == true ? " Procurar Conveniadas da RFB  Atualizar o CPF em uma agência do Banco do Brasil, da CAIXA ou dos CORREIOS" : "");
                                sit += (ff.COD_ORIENTACAO_NIS == null ? "" : (ff.COD_ORIENTACAO_NIS == 0 ? "" : (ff.COD_ORIENTACAO_NIS == 1 ? "Atualizar NIS no INSS Ligar para 135 e agendar o atendimento em uma Agência da Previdência Social." : (ff.COD_ORIENTACAO_NIS == 2 ? "Atualizar o Cadastro NIS em uma agência da CAIXA" : "Atualizar o Cadastro NIS em uma agência do Banco do Brasil."))));
                                sit += (ff.COD_NOME_INV == null ? "" : (ff.COD_NOME_INV == false ? "" : " ** NOME inválido **"));
                                sit += (ff.COD_DN_INV == null ? "" : (ff.COD_DN_INV == false ? "" : " ** DATA NASCIMENTO inválida **"));

                                if (sit == "" && ff.SYS_PROCESSADO)
                                    sit = "Os dados estão corretos.";

                            }
                            else
                            {
                                sit += (ff.COD_NOME_INV == null ? "" : (ff.COD_NOME_INV == false ? "" : " ** NOME inválido **"));
                                sit += (ff.COD_DN_INV == null ? "" : (ff.COD_DN_INV == false ? "" : " ** DATA NASCIMENTO inválida **"));
                                
                                if (sit == "")
                                    sit = ff.WEB_MSG_SIT;
                            }


                            sb.AppendFormat("<font size=\"2\">{0}</font></font></td>", (sit));
                            sb.AppendFormat("</tr>");
                            sb.AppendFormat("</table>");
                            sb.AppendFormat("<hr size=\"1\" noshade>");

                            if (_tformato == "pdf")
                            {
                                if (count % 7 == 0) sb.AppendFormat("<div style=\"page-break-before:always\">&nbsp;</div>");
                            }
                            else
                            {
                                if (count % 5 == 0) sb.AppendFormat("<div style=\"page-break-before:always\">&nbsp;</div>");
                            }


                            txt.WriteLine(sb.ToString());
                        }

                        count++;

                        

                    }

                    if (!_tformato.Contains("csv"))
                        txt.WriteLine("</body></html>");

                    f = null;
                    txt.Close();
                                      

                }

                sb.Clear();
                sb = null;
                dtofun = null;
               

                //se PDF
                if (_tformato == "pdf")
                {
                    //cria arquivo pdf                    
                    var r = Html2Pdf.ConvertHTML(_tsrcfile, _tsrcfilepdf);
                    
                    if (!r)
                    {
                        _erro = "Falha ao escrever o arquivo";
                        _isAlive = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _erro = "Falha ao escrever o arquivo";
                _isAlive = false;
                return;
            }

            _erro = "";
            _isAlive = false; 
            
        }
    }
}
