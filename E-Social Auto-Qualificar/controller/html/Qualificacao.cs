using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace E_Social_Auto_Qualificar.controller.html
{
    class Qualificacao
    {
        string _urlinicial = "http://www9.dataprev.gov.br/Esocial/pages/qualificacao/qualificar.xhtml";
        string _urlroot = "http://www9.dataprev.gov.br";
        
        //paths sistema
        string CurrPath = AppDomain.CurrentDomain.BaseDirectory;

        //Erro out;
        public string ErroMsg { get; set; }
        
        //Parar
        public bool Interromper { get; set; }

        //Erro msg
        string MsgErroDefault = "Sistema não obteve exito ao obter dados do site Dataprev. Tente mais tarde!";


        //DataPrevWeb
        List<RetornoDataprevWeb> _listadpw;
        

        /// <summary>
        /// Obtem dados iniciais para validação cadastral
        /// </summary>
        /// <param name="_envelopform"></param>
        /// <param name="_envelopformsession"></param>
        /// <returns></returns>
        public bool Inicializa(out EnvelopeForm _envelopformout, out EnvelopeFormSession _envelopformsessionout)
        {
                       
            string a = "";
            ErroMsg = String.Empty;
            _envelopformout = null;
            _envelopformsessionout = null;
            Interromper = false;

            try
            {                
                _envelopformout = new EnvelopeForm();
                _envelopformsessionout = new EnvelopeFormSession();

                CookieCollection _cookie;
                var h = HttpRequest(_urlinicial, out a, out _cookie);
                if (h == HttpStatusCode.OK)
                {
                    if (IsDataprevExpired(a) || IsDataprevForadoAr(a))
                    {
                        ErroMsg = MsgErroDefault;
                        return false;
                    }

                    _envelopformout.Cookie = _cookie;
                }
                else
                {
                    a = String.Empty;
                    ErroMsg = MsgErroDefault;
                }

                if (String.IsNullOrEmpty(a))
                {
                    ErroMsg = MsgErroDefault;
                    return false;
                }

                string _actions = "";
                var r = GetInfoForms(a, out _actions, out _envelopformsessionout);
                _envelopformout.FormAction = _actions;

                if (!r)
                    return false;
                
                return true;
                
            }
            catch (WebException ex)
            {
                return false;
            }

        }

        /// <summary>
        /// Envia os dados cadastrais para validação no dataprev
        /// </summary>
        /// <param name="_cadastro"></param>
        /// <param name="_envform"></param>
        /// <param name="_envformsession"></param>
        /// <param name="_envformsessionout"></param>
        /// <param name="_rdataprev"></param>
        /// <returns></returns>
        public bool ValidaCadastro(EnvelopePostCadastro _cadastro, EnvelopeForm _envform, EnvelopeFormSession _envformsession, out EnvelopeFormSession _envformsessionout, out RetornoDataprev _rdataprev)
        {
            _envformsessionout = null;
            _rdataprev = null;
            ErroMsg = String.Empty;

            if (_cadastro == null || _envform == null || _envformsession == null || _envform.Cookie == null || String.IsNullOrEmpty(_envform.FormAction))
                return false;

            string _post = "";
            var a = "";

            //Validações iniciais sem o envio
            _rdataprev = new RetornoDataprev();
            
            //Laiout Base
            _rdataprev.CPF = _cadastro.cpf;
            _rdataprev.NIS = _cadastro.nis;
            _rdataprev.NOME = _cadastro.nome.ToUpper();
            _rdataprev.DN = _cadastro.dataNascimento;

            //validacoes locais
            _rdataprev.COD_NIS_INV = !this.IsPis(_cadastro.nis);
            _rdataprev.COD_CPF_INV = !this.IsCpf(_cadastro.cpf);
            _rdataprev.COD_NOME_INV = (String.IsNullOrEmpty(_cadastro.nome) || _cadastro.nome.Length > 60);
            _rdataprev.COD_DN_INV = !this.IsDataValid(_cadastro.dataNascimento);

            //Validacoes online
            _rdataprev.COD_CNIS_NIS = null;
            _rdataprev.COD_CNIS_DN = null;
            _rdataprev.COD_CNIS_OBITO = null;
            _rdataprev.COD_CNIS_CPF = null;
            _rdataprev.COD_CNIS_CPF_NAO_INF = false;
            _rdataprev.COD_CPF_NAO_CONSTA = null;
            _rdataprev.COD_CPF_NULO = null;
            _rdataprev.COD_CPF_CANCELADO = null;
            _rdataprev.COD_CPF_SUSPENSO = null;
            _rdataprev.COD_CPF_DN = null;
            _rdataprev.COD_CPF_NOME = null;
            _rdataprev.COD_ORIENTACAO_CPF = null;
            _rdataprev.COD_ORIENTACAO_NIS = null;
            _rdataprev.SYS_PROBLEMS = false;

            //retorna com os erros encontrados
            if (_rdataprev.COD_NIS_INV == true || _rdataprev.COD_CPF_INV == true || _rdataprev.COD_NOME_INV == true || _rdataprev.COD_DN_INV == true)
            {
                _rdataprev.WEB_NIS_SIT = (_rdataprev.COD_NIS_INV == true ? "NIS Inválido" : "");
                _rdataprev.WEB_CPF_SIT = (_rdataprev.COD_CPF_INV == true ? "CPF Inválido" : "");
                _rdataprev.WEB_MSG_SIT = (_rdataprev.COD_NOME_INV == true ? "Nome com mais 60 caracteres ou inválido" : "");
                _rdataprev.WEB_MSG_SIT += (_rdataprev.COD_DN_INV == true ? " Data de Nascimento inválida." : "");

                if (_rdataprev.COD_NIS_INV == true)
                    _rdataprev.COD_ORIENTACAO_NIS = 1;

                if (_rdataprev.COD_CPF_INV == true)
                    _rdataprev.COD_ORIENTACAO_CPF = true;

                if ((_rdataprev.COD_NOME_INV == true || _rdataprev.COD_DN_INV == true) && (_rdataprev.COD_NIS_INV == null || _rdataprev.COD_CPF_INV == null))
                {
                    _rdataprev.COD_ORIENTACAO_CPF = null;
                    _rdataprev.COD_ORIENTACAO_NIS = null;
                }

                _rdataprev.SYS_PROBLEMS = true;
                _rdataprev.SYS_PROCESSADO = true;
                _envformsessionout = _envformsession;
                return true;
            }
            
            //dados do form
            _post =  "formQualificacaoCadastral=" + _cadastro.formQualificacaoCadastral;
            _post += "&DTPINFRA_TOKEN=" + _envformsession.DTPINFRA_TOKEN;
            var s = _cadastro.cpf;
            _post += "&formQualificacaoCadastral:cpf=" + string.Format("{0}.{1}.{2}-{3}", s.Substring(0, 3), s.Substring(3, 3), s.Substring(6,3), s.Substring(9,2));  //990.805.165-20
            s = _cadastro.nis;
            _post += "&formQualificacaoCadastral:nis=" + string.Format("{0}.{1}.{2}-{3}", s.Substring(0,3), s.Substring(3, 5), s.Substring(8,2), s.Substring(10,1) ); //127.47791.10-8
            _post += "&formQualificacaoCadastral:nome=" + _cadastro.nome;
            s = _cadastro.dataNascimento;
            _post += "&formQualificacaoCadastral:dataNascimento=" + string.Format("{0}/{1}/{2}", s.Substring(0,2), s.Substring(2, 2), s.Substring(4,4)); //30%2F10%2F1979
            _post += "&formQualificacaoCadastral:btAdicionar=" + _cadastro.btAdicionar;
            _post += "&javax.faces.ViewState=" + _envformsession.ViewState; //132293997965398514%3A2875781845580533438

            CookieCollection _cookie;
            var h = HttpRequest(_urlroot + "/Esocial/pages/qualificacao/qualificar.xhtml?cid=1", out a, out _cookie, "POST", _post, _envform.Cookie);
            if (h == HttpStatusCode.OK)
            {
                //Se site fora do AR ou Expirado
                if (IsDataprevExpired(a) || IsDataprevForadoAr(a))
                {
                    ErroMsg = MsgErroDefault;
                    return false;
                }


                string _actions = "";
                var r = GetInfoForms(a, out _actions, out _envformsessionout);

                //Validações Online  
                
                _rdataprev.COD_NIS_INV = (a.Contains("NIS inv&#225;lido."));
                _rdataprev.COD_CPF_INV = (a.Contains("CPF inv&#225;lido."));
                _rdataprev.COD_NOME_INV = (a.Contains("Nome inv&#225;lido."));
                _rdataprev.COD_DN_INV = (a.Contains("Data de Nascimento inv&#225;lida."));
                           


                if (_rdataprev.COD_NIS_INV == true || _rdataprev.COD_CPF_INV == true || _rdataprev.COD_NOME_INV == true || _rdataprev.COD_DN_INV == true)
                {
                    
                    if (_rdataprev.COD_NIS_INV == true)
                        _rdataprev.COD_ORIENTACAO_NIS = 1;

                    if (_rdataprev.COD_CPF_INV == true)
                        _rdataprev.COD_ORIENTACAO_CPF = true;

                    if ((_rdataprev.COD_NOME_INV == true || _rdataprev.COD_DN_INV == true) && (_rdataprev.COD_NIS_INV == null || _rdataprev.COD_CPF_INV == null))
                    {
                        _rdataprev.COD_ORIENTACAO_CPF = null;
                        _rdataprev.COD_ORIENTACAO_NIS = null;
                    }

                    _rdataprev.SYS_PROBLEMS = true;
                    _rdataprev.SYS_PROCESSADO = true;
                }

                //finaliza o processamento
                if (r)
                    return true;
            }

            ErroMsg = MsgErroDefault;
            return false;
        }

        public bool EnviaCadastro(List<controller.html.RetornoDataprev> _listcad, EnvelopeForm _envform, EnvelopeFormSession _envformsession, out List<controller.html.RetornoDataprev> _listcadout, bool _licenca = true)
        {
            ErroMsg = String.Empty;
            _listcadout = _listcad;
            var _post = "";
            var _captcha = "";
            var a = "";
            bool repetir = true;

            if (_envform == null || _envformsession == null || _listcad == null || String.IsNullOrEmpty(_envformsession.Captcha))              
                return false;
            
            
            //POST /Esocial/pages/qualificacao/qualificar.xhtml?cid=2 
            //formValidacao=formValidacao&DTPINFRA_TOKEN=1395510457344&formValidacao%3AcaptchaId=XY8D&formValidacao%3AbotaoValidar=Qualificar&javax.faces.ViewState=-5060252129614820439%3A2835102570655533055

            do
            {
                _post = "";

                // Abre form para validacao
                using (view.Captcha c = new view.Captcha())
                {
                    c.ImagemUrl = _urlroot + _envformsession.Captcha;
                    c.Cookie = _envform.Cookie;

                    c.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    c.TopMost = true;
                    c.ShowDialog();

                    Interromper = (c.DialogResult == System.Windows.Forms.DialogResult.Abort);

                    _captcha = c.CaptchaTxt;
                }

                if (_captcha == "NULO")
                {
                    Interromper = true;
                    ErroMsg = "Processo interrompido pelo usuário.";
                    return false;
                }

                //Dados do formuario
                _post += "formValidacao=formValidacao";
                _post += "&DTPINFRA_TOKEN=" + _envformsession.DTPINFRA_TOKEN;
                _post += "&formValidacao:captchaId=" + _captcha;
                _post += "&formValidacao:botaoValidar=Qualificar";
                _post += "&javax.faces.ViewState=" + _envformsession.ViewState;

                CookieCollection _cookie;
                var h = HttpRequest(_urlroot + "/Esocial/pages/qualificacao/qualificar.xhtml?cid=1", out a, out _cookie, "POST", _post, _envform.Cookie);
                if (h == HttpStatusCode.OK)
                {
                    //Se site fora do AR ou Expirado
                    if (IsDataprevExpired(a))
                    {
                        ErroMsg = MsgErroDefault;
                        return false;
                    }

                    repetir = a.Contains("Captcha: O texto");

                    //Pagina fora do ar
                    if (!repetir)
                        repetir = IsDataprevForadoAr(a);

                    if (repetir)
                        System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    repetir = false;
                }

            } while (repetir);

            //confirma que existe resultado
            if (a.Contains("<legend>Resultado da Qualifica"))
            {
                //Obtem os resultados 
                this.GetResultados(a, out _listadpw);

                if (_listadpw == null)
                {
                    ErroMsg = MsgErroDefault;
                    return false;
                }

                //Grava Log
                if(_licenca)
                    LogProcessado(a);

                //Invlui todo o conteudo dentro da lista
                foreach (RetornoDataprevWeb dpw in _listadpw)
                {
                    var f = _listcad.FirstOrDefault(c => c.CPF == dpw.CPF);
                    if (f != null)
                    {
                        f.WEB_CPF_SIT = dpw.SIT_CPF;
                        f.WEB_NIS_SIT = dpw.SIT_NIS;
                        f.WEB_MSG_SIT = dpw.SIT_MSG;

                        f.SYS_PROBLEMS = (dpw.SIT_CPF != "Válido." || dpw.SIT_NIS != "Válido." || dpw.SIT_MSG != "Os dados estão corretos.");

                        f.SYS_PROCESSADO = true;
                    }
                }

                //atualiza Lista;
                _listcadout = _listcad;

                return true;
            }

            ErroMsg = MsgErroDefault;

            return false;
        }

        /// <summary>
        /// Obtem informações do HTML do resultado
        /// </summary>
        /// <param name="_html"></param>
        /// <param name="_actions"></param>
        /// <param name="_envelopformsession"></param>
        /// <returns></returns>
        private bool GetInfoForms(string _html, out string _actions, out EnvelopeFormSession _envelopformsession)
        {
            _actions = "";
            _envelopformsession = null;

            /// Tratando Resultado HTML
            /// 
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlNode.ElementsFlags.Remove("form");

            doc.LoadHtml(_html);

            var _forms = doc.DocumentNode.Descendants("form");
            string[] _action = new string[30];
            var _count = 0;
            foreach (var _form in _forms)
            {
                _action[_count] = _form.Attributes["action"].Value;
                _count++;
            }

            //não encontrameos action
            if (_action.Length <= 1)
                return false;

            _actions = _action[1];

            _envelopformsession = new EnvelopeFormSession();

            //Obter Form Sessions
            _count = 0;
            foreach (var _form in _forms)
            {
                //get inputs
                foreach (HtmlNode _element in _form.Descendants("input"))
                {
                    if (!_element.Attributes.Contains("value") && _count != 1) continue;

                    if (_element.Attributes["name"].Value == "DTPINFRA_TOKEN")
                        _envelopformsession.DTPINFRA_TOKEN = _element.Attributes["value"].Value;

                    if (_element.Attributes["name"].Value == "javax.faces.ViewState")
                        _envelopformsession.ViewState = _element.Attributes["value"].Value;

                }

                foreach (HtmlNode _element in _form.Descendants("img"))
                {
                    if (_element.Attributes["src"].Value.Contains("CAPTCHA.xhtml"))
                        _envelopformsession.Captcha = _element.Attributes["src"].Value;
                }


                _count++;
            }

           //Get todas as imagens
           // var img = doc.DocumentNode.Descendants("img")
           //             .Select(x => x.Attributes["src"].Value)
           //             .ToArray();

            doc = null;

            if (_count > 0)
                return true;

            return false;
        }

        private void GetResultados(string _html, out List<RetornoDataprevWeb> _listadpwout)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(_html);

            // Renove os comentarios
            var comments = doc.DocumentNode.Descendants()
                                    .OfType<HtmlCommentNode>()
                                    .Where(c =>
                                    !c.Comment.StartsWith("<!DOCTYPE", StringComparison.OrdinalIgnoreCase)
                                     ).ToList();

            foreach (var comment in comments)
                comment.Remove();

            _html = doc.DocumentNode.InnerHtml;

            //Processa tabela
            
            doc.LoadHtml(_html);
            /*var query = from table in doc.DocumentNode.SelectNodes("//table").Cast<HtmlNode>()
                        from row in table.SelectNodes("tr").Cast<HtmlNode>()
                        from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                        select new { Table = table.Id, CellText = cell.InnerText };

            foreach (var cell in query)
            {
               // Console.WriteLine("{0}: {1}", cell.Table, cell.CellText);
                result.Text += cell.Table + " : " + cell.CellText + Environment.NewLine;
                
            } */

            int count = 1;
            string tableid = "";
            string celula = "";

            _listadpwout = new List<RetornoDataprevWeb>();

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                //Verifica o id
                try {
                    tableid = table.Id;
                }
                catch(Exception ex)
                {
                    tableid = "";
                }
                

                //se encontrei a tabela colete os dados
                if (tableid.Contains("gridDadosTrabalhado"))
                {
                    foreach (HtmlNode row in table.SelectNodes("//tr"))
                    {
                        RetornoDataprevWeb dpw = new RetornoDataprevWeb();
                        foreach (HtmlNode cell in row.SelectNodes("th|td"))
                        {
                            celula = System.Net.WebUtility.HtmlDecode(cell.InnerText.ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Trim());

                            if (count > 7)
                            {
                                if ((count % 7) == 1)
                                    dpw.CPF = celula.Replace(".","").Replace("-","");

                                if ((count % 7) == 2)
                                    dpw.NIS = celula.Replace(".", "").Replace("-", "");

                                if ((count % 7) == 3)
                                    dpw.Nome = celula;

                                if ((count % 7) == 4)
                                    dpw.DN = celula.Replace("/", "");

                                if ((count % 7) == 5)
                                    dpw.SIT_NIS = celula;

                                if ((count % 7) == 6)
                                    dpw.SIT_CPF = celula;

                                if ((count % 7) == 0)
                                    dpw.SIT_MSG = celula;
                                
                            }

                            count++;
                        }
                        
                        if (!String.IsNullOrEmpty(dpw.Nome))
                            _listadpw.Add(dpw);
                        
                    }
                }
            }
        }

       

        /// <summary>
        /// Verifica se o serviço da DataPrev está online
        /// </summary>
        /// <returns></returns>
        public bool isOnline()
        {

            bool r = false;
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_urlinicial);
                request.AllowAutoRedirect = true;                            
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                    r = true;

                //fecjamento
                response.Close();
                response = null;
                request = null;

                return r;
            }
            catch (WebException ex)
            {                                
                return false;
            }

           
        }

        /// <summary>
        /// Valida CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Valida PIS
        /// </summary>
        /// <param name="pis"></param>
        /// <returns></returns>
        public bool IsPis(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }

        /// <summary>
        /// Testa se a data é válida.
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public bool IsDataValid(string _data)
        {
            DateTime dt;

            if (DateTime.TryParseExact(_data, "ddMMyyyy",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out dt))
                return true;

            return false;
           
        }

        /// <summary>
        /// Efetua requisições HTTP
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_html"></param>
        /// <param name="_cookiesout"></param>
        /// <param name="metodo"></param>
        /// <param name="_querypost"></param>
        /// <param name="_cookies"></param>
        /// <returns>Retorna o código status HTML, retorna HttpStatusCode.Unused em caso de falha</returns>
        private HttpStatusCode HttpRequest(string _url, out string _html, out CookieCollection _cookiesout, string metodo = "GET", string _querypost = "", CookieCollection _cookies = null)
        {
            _html = String.Empty;
            _cookiesout = null;
            HttpStatusCode r = HttpStatusCode.Unused;
            HttpWebResponse response;
                        
            CookieContainer _cookie = new CookieContainer();
            if(_cookies != null)
                _cookie.Add(_cookies);

            System.Net.ServicePointManager.Expect100Continue = false;

            //Envia o cadastro e retorna 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.AllowAutoRedirect = true;
            request.CookieContainer = _cookie;
            request.Method = metodo;
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            request.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            //em caso de post
            if (metodo == "POST")
            {
                request.ContentType = "application/x-www-form-urlencoded";

                byte[] byteArray = Encoding.UTF8.GetBytes(_querypost);
                request.ContentLength = byteArray.Length;

                Stream _str = request.GetRequestStream();
                _str.Write(byteArray, 0, byteArray.Length); //update form
                _str.Close();
                _str = null;
            }
            
            try
            {

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // HTTP = 200 - Internet connection available, server online
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        _html = sr.ReadToEnd();
                    }

                    _cookiesout = response.Cookies;
                }
            }
            catch (Exception ex)
            {
                //Erro 404
                return r;
            }

            _cookie = null;

            r = response.StatusCode;
           
            //liebra recursos
            response.Close();
            response = null;
            request = null;

            return r;
        }

        /// <summary>
        /// Sessão Expirou
        /// </summary>
        /// <param name="_html"></param>
        /// <returns></returns>
        private bool IsDataprevExpired(string _html)
        {
            return _html.Contains("Sess&#227;o expirada!");
        }

        /// <summary>
        /// Sistema fora do AR
        /// </summary>
        /// <param name="_html"></param>
        /// <returns></returns>
        private bool IsDataprevForadoAr(string _html)
        {
            return _html.Contains("Temporariamente esta p&aacute;gina n&atilde;o pode ser exibida");
        }

        /// <summary>
        /// Cria um log da validação
        /// </summary>
        /// <param name="_html"></param>
        private void LogProcessado(string _html)
        {
            try
            {
                string PathLog = CurrPath + "proglog\\";

                //cria diretorio se não existir
                if (!Directory.Exists(PathLog))
                    Directory.CreateDirectory(PathLog);

                _html = _html.Replace("href=\"/Esocial", "href=\"" + _urlroot + "/Esocial").Replace("src=\"/Esocial", "src=\"" + _urlroot + "/Esocial");

                File.WriteAllText(PathLog + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html", _html);
            }
            catch (Exception ex)
            {
                //nda
            }

        }
    }
}
