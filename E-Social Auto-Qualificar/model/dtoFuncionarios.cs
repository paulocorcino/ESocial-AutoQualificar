using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;


namespace E_Social_Auto_Qualificar.model
{
    class dtoFuncionarios
    {
        /// <summary>
        /// Obtem dados funcionario
        /// </summary>
        /// <param name="_cpf"></param>
        /// <param name="_limit"></param>
        /// <returns></returns>
        public List<Funcionarios> Get(string _cpf = "", bool? _inuse = null, bool? _proc = null, bool? _erro = null, int _limit = 0)
        {
            List<Funcionarios> f = null;            

            try
            {
                f = new List<Funcionarios>();
                dbconnect dbconn = new dbconnect();
                List<SQLiteParameter> l = null;

                string sql = "Select";
                        sql += "  CPF,";
                        sql += "  NIS,";
                        sql += "  NOME,";
                        sql += "  DN,";
                        sql += "  COD_NIS_INV,";
                        sql += "  COD_CPF_INV,";
                        sql += "  COD_NOME_INV,";
                        sql += "  COD_DN_INV,";
                        sql += "  COD_CNIS_NIS,";
                        sql += "  COD_CNIS_DN,";
                        sql += "  COD_CNIS_OBITO,";
                        sql += "  COD_CNIS_CPF,";
                        sql += "  COD_CNIS_CPF_NAO_INF,";
                        sql += "  COD_CPF_NAO_CONSTA,";
                        sql += "  COD_CPF_NULO,";
                        sql += "  COD_CPF_CANCELADO,";
                        sql += "  COD_CPF_SUSPENSO,";
                        sql += "  COD_CPF_DN,";
                        sql += "  COD_CPF_NOME,";
                        sql += "  COD_ORIENTACAO_CPF,";
                        sql += "  COD_ORIENTACAO_NIS,";
                        sql += "  WEB_CPF_SIT,";
                        sql += "  WEB_NIS_SIT,";
                        sql += "  WEB_MSG_SIT,";
                        sql += "  SYS_PROCESSADO,";
                        sql += "  SYS_IN_USE,";
                        sql += "  SYS_PROBLEMS,";
                        sql += "  SYS_FILTRO ";
                        sql += " From ";
                        sql += "  FUNCIONARIOS";

                if (!String.IsNullOrEmpty(_cpf)) {

                    sql += " where CPF = @CPF";
                        
                    l = new List<SQLiteParameter>();
                    l.Add(new SQLiteParameter("CPF", _cpf));
                }

                if (_inuse != null)
                {
                    sql += (sql.Contains("where") ? " and " : " where ") + " SYS_IN_USE = @SYS_IN_USE";

                    if (l == null)
                        l = new List<SQLiteParameter>();

                    l.Add(new SQLiteParameter("SYS_IN_USE", _inuse));

                }

                if (_proc != null)
                {
                    sql += (sql.Contains("where") ? " and " : " where ") + " SYS_PROCESSADO = @SYS_PROCESSADO";

                    if (l == null)
                        l = new List<SQLiteParameter>();

                    l.Add(new SQLiteParameter("SYS_PROCESSADO", _proc));

                }

                if (_erro != null)
                {
                    sql += (sql.Contains("where") ? " and " : " where ") + " SYS_PROBLEMS = @SYS_PROBLEMS";

                    if (l == null)
                        l = new List<SQLiteParameter>();

                    l.Add(new SQLiteParameter("SYS_PROBLEMS", _erro));

                }


                if (_limit > 0)
                    sql += " Limit " + _limit.ToString();

                //Order by Nome
                sql = "SELECT * FROM (" + sql + ") tmp Order By tmp.NOME";

                using (var r = dbconn.QueryReader(sql, out dbconn._conn, l))
                {
                    while (r.Read())
                    {
                        var ff = new Funcionarios();
                            ff.CPF = r["CPF"].ToString();
                            ff.NIS = r["NIS"].ToString();
                            ff.NOME = r["NOME"].ToString();
                            ff.DN = r["DN"].ToString();
                            ff.COD_NIS_INV = SafeCast.castbool(r["COD_NIS_INV"]);
                            ff.COD_CPF_INV =  SafeCast.castbool(r["COD_CPF_INV"]);
                            ff.COD_NOME_INV =  SafeCast.castbool(r["COD_NOME_INV"]);
                            ff.COD_DN_INV =  SafeCast.castbool(r["COD_DN_INV"]);
                            ff.COD_CNIS_NIS =  SafeCast.castbool(r["COD_CNIS_NIS"]);
                            ff.COD_CNIS_DN =  SafeCast.castbool(r["COD_CNIS_DN"]);
                            ff.COD_CNIS_OBITO =  SafeCast.castbool(r["COD_CNIS_OBITO"]);
                            ff.COD_CNIS_CPF =  SafeCast.castbool(r["COD_CNIS_CPF"]);
                            ff.COD_CNIS_CPF_NAO_INF =  SafeCast.castbool(r["COD_CNIS_CPF_NAO_INF"]);
                            ff.COD_CPF_NAO_CONSTA =  SafeCast.castbool(r["COD_CPF_NAO_CONSTA"]);
                            ff.COD_CPF_NULO =  SafeCast.castbool(r["COD_CPF_NULO"]);
                            ff.COD_CPF_CANCELADO =  SafeCast.castbool(r["COD_CPF_CANCELADO"]);
                            ff.COD_CPF_SUSPENSO =  SafeCast.castbool(r["COD_CPF_SUSPENSO"]);
                            ff.COD_CPF_DN =  SafeCast.castbool(r["COD_CPF_DN"]);
                            ff.COD_CPF_NOME =  SafeCast.castbool(r["COD_CPF_NOME"]);
                            ff.COD_ORIENTACAO_CPF =  SafeCast.castbool(r["COD_ORIENTACAO_CPF"]);
                            ff.COD_ORIENTACAO_NIS =  SafeCast.castint(r["COD_ORIENTACAO_NIS"]);
                            ff.WEB_CPF_SIT = r["WEB_CPF_SIT"].ToString();
                            ff.WEB_NIS_SIT = r["WEB_NIS_SIT"].ToString();
                            ff.WEB_MSG_SIT = r["WEB_MSG_SIT"].ToString();
                            ff.SYS_PROCESSADO = (bool)r["SYS_PROCESSADO"];
                            ff.SYS_IN_USE = (bool)r["SYS_IN_USE"];
                            ff.SYS_PROBLEMS = (bool)r["SYS_PROBLEMS"];
                            ff.SYS_FILTRO = r["SYS_FILTRO"].ToString();
                            f.Add(ff);
                            ff = null;
                    }

                    dbconn._conn.Close();
                }

                dbconn = null; 
                        
            }
            catch (Exception ex)
            {
                //nada
                f = null;
            }

            

            return f;
        }

        /// <summary>
        /// Inserir dados na tabela funconario
        /// </summary>
        /// <param name="_f"></param>
        /// <returns></returns>
        public int Insert(Funcionarios _f)
        {
            int ret = -1;

            if (_f == null)
                return ret;

            
                dbconnect dbconn = new dbconnect();

                string sql = "INSERT INTO FUNCIONARIOS (";
                    sql += " CPF,";
                    sql += " NIS,";
                    sql += " NOME,";
                    sql += " DN,";
                    sql += " COD_NIS_INV,";
                    sql += " COD_CPF_INV,";
                    sql += " COD_NOME_INV,";
                    sql += " COD_DN_INV,";
                    sql += " COD_CNIS_NIS,";
                    sql += " COD_CNIS_DN,";
                    sql += " COD_CNIS_OBITO,";
                    sql += " COD_CNIS_CPF,";
                    sql += " COD_CNIS_CPF_NAO_INF,";
                    sql += " COD_CPF_NAO_CONSTA,";
                    sql += " COD_CPF_NULO,";
                    sql += " COD_CPF_CANCELADO,";
                    sql += " COD_CPF_SUSPENSO,";
                    sql += " COD_CPF_DN,";
                    sql += " COD_CPF_NOME,";
                    sql += " COD_ORIENTACAO_CPF,";
                    sql += " COD_ORIENTACAO_NIS,";
                    sql += " WEB_CPF_SIT,";
                    sql += " WEB_NIS_SIT,";
                    sql += " WEB_MSG_SIT,";
                    sql += " SYS_PROCESSADO,";
                    sql += " SYS_IN_USE,";
                    sql += " SYS_PROBLEMS,";
                    sql += " SYS_FILTRO";                
                    sql += ") VALUES (";
                    sql += " @CPF,";
                    sql += " @NIS,";
                    sql += " @NOME,";
                    sql += " @DN,";
                    sql += " @COD_NIS_INV,";
                    sql += " @COD_CPF_INV,";
                    sql += " @COD_NOME_INV,";
                    sql += " @COD_DN_INV,";
                    sql += " @COD_CNIS_NIS,";
                    sql += " @COD_CNIS_DN,";
                    sql += " @COD_CNIS_OBITO,";
                    sql += " @COD_CNIS_CPF,";
                    sql += " @COD_CNIS_CPF_NAO_INF,";
                    sql += " @COD_CPF_NAO_CONSTA,";
                    sql += " @COD_CPF_NULO,";
                    sql += " @COD_CPF_CANCELADO,";
                    sql += " @COD_CPF_SUSPENSO,";
                    sql += " @COD_CPF_DN,";
                    sql += " @COD_CPF_NOME,";
                    sql += " @COD_ORIENTACAO_CPF,";
                    sql += " @COD_ORIENTACAO_NIS,";
                    sql += " @WEB_CPF_SIT,";
                    sql += " @WEB_NIS_SIT,";
                    sql += " @WEB_MSG_SIT,";
                    sql += " @SYS_PROCESSADO,";
                    sql += " @SYS_IN_USE, ";
                    sql += " @SYS_PROBLEMS, ";
                    sql += " @SYS_FILTRO";
                    sql += ")";

                    var p = new List<System.Data.SQLite.SQLiteParameter>();
                    p.Add(new SQLiteParameter("CPF", _f.CPF));
                    p.Add(new SQLiteParameter("NIS", _f.NIS));
                    p.Add(new SQLiteParameter("NOME", _f.NOME));
                    p.Add(new SQLiteParameter("DN", _f.DN));
                    p.Add(new SQLiteParameter("COD_NIS_INV", _f.COD_NIS_INV));
                    p.Add(new SQLiteParameter("COD_CPF_INV", _f.COD_CPF_INV));
                    p.Add(new SQLiteParameter("COD_NOME_INV", _f.COD_NOME_INV));
                    p.Add(new SQLiteParameter("COD_DN_INV", _f.COD_DN_INV));
                    p.Add(new SQLiteParameter("COD_CNIS_NIS", _f.COD_CNIS_NIS));
                    p.Add(new SQLiteParameter("COD_CNIS_DN", _f.COD_CNIS_DN));
                    p.Add(new SQLiteParameter("COD_CNIS_OBITO", _f.COD_CNIS_OBITO));
                    p.Add(new SQLiteParameter("COD_CNIS_CPF", _f.COD_CNIS_CPF));
                    p.Add(new SQLiteParameter("COD_CNIS_CPF_NAO_INF", _f.COD_CNIS_CPF_NAO_INF));
                    p.Add(new SQLiteParameter("COD_CPF_NAO_CONSTA", _f.COD_CPF_NAO_CONSTA));
                    p.Add(new SQLiteParameter("COD_CPF_NULO", _f.COD_CPF_NULO));
                    p.Add(new SQLiteParameter("COD_CPF_CANCELADO", _f.COD_CPF_CANCELADO));
                    p.Add(new SQLiteParameter("COD_CPF_SUSPENSO", _f.COD_CPF_SUSPENSO));
                    p.Add(new SQLiteParameter("COD_CPF_DN", _f.COD_CPF_DN));
                    p.Add(new SQLiteParameter("COD_CPF_NOME", _f.COD_CPF_NOME));
                    p.Add(new SQLiteParameter("COD_ORIENTACAO_CPF", _f.COD_ORIENTACAO_CPF));
                    p.Add(new SQLiteParameter("COD_ORIENTACAO_NIS", _f.COD_ORIENTACAO_NIS));
                    p.Add(new SQLiteParameter("WEB_CPF_SIT", _f.WEB_CPF_SIT));
                    p.Add(new SQLiteParameter("WEB_NIS_SIT", _f.WEB_NIS_SIT));
                    p.Add(new SQLiteParameter("WEB_MSG_SIT", _f.WEB_MSG_SIT));
                    p.Add(new SQLiteParameter("SYS_PROCESSADO", _f.SYS_PROCESSADO));
                    p.Add(new SQLiteParameter("SYS_IN_USE", _f.SYS_IN_USE));
                    p.Add(new SQLiteParameter("SYS_PROBLEMS", _f.SYS_PROBLEMS));
                    p.Add(new SQLiteParameter("SYS_FILTRO", _f.SYS_FILTRO));

                    ret = dbconn.QueryExecute(sql, p);

                    dbconn = null;

            return ret;
        }

        /// <summary>
        /// Insere e retorna o valor
        /// </summary>
        /// <param name="_f"></param>
        /// <returns></returns>
        public Funcionarios InsertGet(Funcionarios _f)
        {
            int ret = this.Insert(_f);
            Funcionarios ff = null;

            if (ret > 0)
                ff = Get(_f.CPF)[0];

            return ff;

        }

        /// <summary>
        /// Atualiza dados do funcionario
        /// </summary>
        /// <param name="_f"></param>
        /// <returns></returns>
        public int Update(Funcionarios _f)
        {
            int ret = -1;

            if (_f == null)
                return ret;

          
                dbconnect dbconn = new dbconnect();

                string sql = "UPDATE FUNCIONARIOS SET ";
                        sql += " NIS = @NIS,";
                        sql += " NOME = @NOME,";
                        sql += " DN = @DN,";
                        sql += " COD_NIS_INV = @COD_NIS_INV,";
                        sql += " COD_CPF_INV = @COD_CPF_INV,";
                        sql += " COD_NOME_INV = @COD_NOME_INV,";
                        sql += " COD_DN_INV = @COD_DN_INV,";
                        sql += " COD_CNIS_NIS = @COD_CNIS_NIS,";
                        sql += " COD_CNIS_DN = @COD_CNIS_DN,";
                        sql += " COD_CNIS_OBITO = @COD_CNIS_OBITO,";
                        sql += " COD_CNIS_CPF = @COD_CNIS_CPF,";
                        sql += " COD_CNIS_CPF_NAO_INF = @COD_CNIS_CPF_NAO_INF,";
                        sql += " COD_CPF_NAO_CONSTA = @COD_CPF_NAO_CONSTA,";
                        sql += " COD_CPF_NULO = @COD_CPF_NULO,";
                        sql += " COD_CPF_CANCELADO = @COD_CPF_CANCELADO,";
                        sql += " COD_CPF_SUSPENSO = @COD_CPF_SUSPENSO,";
                        sql += " COD_CPF_DN = @COD_CPF_DN,";
                        sql += " COD_CPF_NOME = @COD_CPF_NOME,";
                        sql += " COD_ORIENTACAO_CPF = @COD_ORIENTACAO_CPF,";
                        sql += " COD_ORIENTACAO_NIS = @COD_ORIENTACAO_NIS,";
                        sql += " WEB_CPF_SIT = @WEB_CPF_SIT,";
                        sql += " WEB_NIS_SIT = @WEB_NIS_SIT,";
                        sql += " WEB_MSG_SIT = @WEB_MSG_SIT,";
                        sql += " SYS_PROCESSADO = @SYS_PROCESSADO,";
                        sql += " SYS_IN_USE = @SYS_IN_USE,";
                        sql += " SYS_PROBLEMS = @SYS_PROBLEMS,";
                        sql += " SYS_FILTRO = @SYS_FILTRO,";                
                        sql += " WHERE CPF = @CPF";

                        var p = new List<System.Data.SQLite.SQLiteParameter>();
                        p.Add(new SQLiteParameter("CPF", _f.CPF));
                        p.Add(new SQLiteParameter("NIS", _f.NIS));
                        p.Add(new SQLiteParameter("NOME", _f.NOME));
                        p.Add(new SQLiteParameter("DN", _f.DN));
                        p.Add(new SQLiteParameter("COD_NIS_INV", _f.COD_NIS_INV));
                        p.Add(new SQLiteParameter("COD_CPF_INV", _f.COD_CPF_INV));
                        p.Add(new SQLiteParameter("COD_NOME_INV", _f.COD_NOME_INV));
                        p.Add(new SQLiteParameter("COD_DN_INV", _f.COD_DN_INV));
                        p.Add(new SQLiteParameter("COD_CNIS_NIS", _f.COD_CNIS_NIS));
                        p.Add(new SQLiteParameter("COD_CNIS_DN", _f.COD_CNIS_DN));
                        p.Add(new SQLiteParameter("COD_CNIS_OBITO", _f.COD_CNIS_OBITO));
                        p.Add(new SQLiteParameter("COD_CNIS_CPF", _f.COD_CNIS_CPF));
                        p.Add(new SQLiteParameter("COD_CNIS_CPF_NAO_INF", _f.COD_CNIS_CPF_NAO_INF));
                        p.Add(new SQLiteParameter("COD_CPF_NAO_CONSTA", _f.COD_CPF_NAO_CONSTA));
                        p.Add(new SQLiteParameter("COD_CPF_NULO", _f.COD_CPF_NULO));
                        p.Add(new SQLiteParameter("COD_CPF_CANCELADO", _f.COD_CPF_CANCELADO));
                        p.Add(new SQLiteParameter("COD_CPF_SUSPENSO", _f.COD_CPF_SUSPENSO));
                        p.Add(new SQLiteParameter("COD_CPF_DN", _f.COD_CPF_DN));
                        p.Add(new SQLiteParameter("COD_CPF_NOME", _f.COD_CPF_NOME));
                        p.Add(new SQLiteParameter("COD_ORIENTACAO_CPF", _f.COD_ORIENTACAO_CPF));
                        p.Add(new SQLiteParameter("COD_ORIENTACAO_NIS", _f.COD_ORIENTACAO_NIS));
                        p.Add(new SQLiteParameter("WEB_CPF_SIT", _f.WEB_CPF_SIT));
                        p.Add(new SQLiteParameter("WEB_NIS_SIT", _f.WEB_NIS_SIT));
                        p.Add(new SQLiteParameter("WEB_MSG_SIT", _f.WEB_MSG_SIT));
                        p.Add(new SQLiteParameter("SYS_PROCESSADO", _f.SYS_PROCESSADO));
                        p.Add(new SQLiteParameter("SYS_IN_USE", _f.SYS_IN_USE));
                        p.Add(new SQLiteParameter("SYS_PROBLEMS", _f.SYS_PROBLEMS));
                        p.Add(new SQLiteParameter("SYS_FILTRO", _f.SYS_FILTRO));
                    

                    ret = dbconn.QueryExecute(sql, p);

                    dbconn = null;
            return ret;
        }

        /// <summary>
        /// Retorna uma lista de filtros
        /// </summary>
        /// <returns></returns>
        public string[] GetListFiltros()
        {
            dbconnect dbconn = new dbconnect();
            List<string> ret = null;
            var l = dbconn.QueryReader("Select Distinct SYS_FILTRO From FUNCIONARIOS Order By SYS_FILTRO");
            if (l.Count > 0)
            {
                ret = new List<string>();                
                foreach (Dictionary<string, object> d in l)
                {
                    if (d["SYS_FILTRO"].ToString() != "")
                    {
                        ret.Add(d["SYS_FILTRO"].ToString());
                    }
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Atualiza e retorna o valor
        /// </summary>
        /// <param name="_f"></param>
        /// <returns></returns>
        public Funcionarios UpdateGet(Funcionarios _f)
        {
            int ret = this.Update(_f);
            Funcionarios ff = null;

            if (ret > 0)
                ff = Get(_f.CPF)[0];

            return ff;

        }

        /// <summary>
        /// Deleta um funcionario
        /// </summary>
        /// <param name="_cpf"></param>
        /// <returns></returns>
        public int Delete(string _cpf)
        {
            int ret = -1;
            
            dbconnect dbconn = new dbconnect();
            string sql = "Delete from funcionarios where CPF = @CPF";

            var p = new List<System.Data.SQLite.SQLiteParameter>();     
            p.Add(new SQLiteParameter("CPF", _cpf));
            ret = dbconn.QueryExecute(sql, p);

            dbconn = null;

            return ret;

        }
         
    }
}
