using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace E_Social_Auto_Qualificar.model
{
    class dbconnect
    {
        static string CurrPath = AppDomain.CurrentDomain.BaseDirectory;
        //static string nmdatabase = CurrPath + "data.db";
        static string nmdatabase = "data.db";
        static string conexao = "Data Source=" + nmdatabase + ";Password=cec130310;";
        //static string conexao = "Data Source=" + nmdatabase + ";";
        public static string versao = "1.0.12";
        string versaoatualdb = "";
        public SQLiteConnection _conn;

        string SqlFuncionario
        {
            get
            {
                //Conexao Oficial
                string sql = "CREATE TABLE IF NOT EXISTS FUNCIONARIOS (";
                sql += "CPF  varchar(11) NOT NULL,";
                sql += "NIS  varchar(11) NOT NULL,";
                sql += "NOME  varchar(60) NOT NULL,";
                sql += "DN  varchar(8) NOT NULL,";
                sql += "COD_NIS_INV  bit NULL,";
                sql += "COD_CPF_INV  bit NULL,";
                sql += "COD_NOME_INV  bit NULL,";
                sql += "COD_DN_INV  bit NULL,";
                sql += "COD_CNIS_NIS  bit NULL,";
                sql += "COD_CNIS_DN  bit NULL,";
                sql += "COD_CNIS_OBITO  bit NULL,";
                sql += "COD_CNIS_CPF  bit NULL,";
                sql += "COD_CNIS_CPF_NAO_INF  bit NULL,";
                sql += "COD_CPF_NAO_CONSTA  bit NULL,";
                sql += "COD_CPF_NULO  bit NULL,";
                sql += "COD_CPF_CANCELADO  bit NULL,";
                sql += "COD_CPF_SUSPENSO  bit NULL ,";
                sql += "COD_CPF_DN  bit NULL ,";
                sql += "COD_CPF_NOME  bit NULL ,";
                sql += "COD_ORIENTACAO_CPF  bit NULL ,";
                sql += "COD_ORIENTACAO_NIS  integer NULL,";
                sql += "WEB_CPF_SIT  varchar(255) NULL,";
                sql += "WEB_NIS_SIT  varchar(255) NULL,";
                sql += "WEB_MSG_SIT  varchar(255) NULL,";
                sql += "SYS_PROCESSADO  bit NOT NULL default 0,";
                sql += "SYS_IN_USE bit not null default 0,";
                sql += "SYS_PROBLEMS bit not null default 0,";
                //sql += "SYS_FILTRO varchar(255) not null default 'MATRIZ',"; //Atualizacao versao 1.0.11; 
                sql += "SYS_FILTRO varchar(255) null default '',"; //Atualizacao versao 1.0.12; 
                sql += "PRIMARY KEY (CPF ASC)";
                sql += ");";

                return sql;
            }
        }
        string SqlConfiguracao
        {
            get
            {
                
                    string sql = "CREATE TABLE IF NOT EXISTS CONFIGURACAO (";
                    sql += "VERSAO  varchar(10) NOT NULL,";
                    sql += "MODOCHK  varchar(1) NOT NULL,";
                    sql += "SOMCAPTCHA  bit NOT NULL,";
                    sql += "SHOWNOVIDADES  bit NOT NULL"; //Atualizacao 1.0.12
                    sql += ");";
                    return sql;
            }
        }
       
        public bool CreateDB()
        {
            //banco de dados nao existe
            if (!System.IO.File.Exists(nmdatabase))
            {
                //Conexao Temporaria criacao senha;
                SQLiteConnection.CreateFile(nmdatabase);

                using (SQLiteConnection _conn = new SQLiteConnection(conexao.Split(';')[0] + ";"))
                {
                    _conn.Open();
                    _conn.ChangePassword("cec130310");
                    _conn.Close();                                   
                }

                try
                {
                    //cria tabela funcionario
                    var c = QueryExecute(SqlFuncionario);

                    //cria tabela configuracao
                    c = QueryExecute(SqlConfiguracao);

                    var sql = "INSERT INTO CONFIGURACAO(VERSAO,MODOCHK,SOMCAPTCHA,SHOWNOVIDADES) VALUES ('" + versao + "','W',0,0)";

                    c = QueryExecute(sql);                   

                }
                catch (Exception ex)
                {

                    return false;
                }

                return true;

            }
            else
            {

                try
                {
                    var getversion = QueryReader("Select VERSAO from CONFIGURACAO");                        
                    foreach (Dictionary<string, object> d in getversion)
                    {
                        versaoatualdb = d["VERSAO"].ToString();
                    }
                        
                                        
                    
                }
                catch (Exception exx)
                {                  
                                  
                    if (String.IsNullOrEmpty(versaoatualdb))
                    {

                        //backup db
                        BackupDatabase();

                        using (SQLiteConnection _conn = new SQLiteConnection(conexao.Split(';')[0] + ";"))
                        {
                            _conn.Open();
                            _conn.ChangePassword("cec130310");
                            _conn.Close();
                                                     
                        }

                        var getversion = QueryReader("Select VERSAO from CONFIGURACAO");
                        foreach (Dictionary<string, object> d in getversion)
                        {
                            versaoatualdb = d["VERSAO"].ToString();
                        }
                    }
                }
                              
                
               
                    
                                    
                    //Atualizações versão 1.0.11 /////////////////////////////////////////
                    if (String.Compare(versaoatualdb, "1.0.11") < 0)
                    {
                        //backup da base de dados
                        BackupDatabase();

                        _conn = GetConn();

                        using (System.Data.SQLite.SQLiteTransaction transac = _conn.BeginTransaction())
                        {

                            QueryExecute("ALTER TABLE FUNCIONARIOS ADD COLUMN SYS_FILTRO varchar(255) null default ''", null, "", _conn);

                            //QueryExecute("ALTER TABLE CONFIGURACAO ADD COLUMN SHOWNOVIDADES bit null", null, "", _conn);
                                                       
                            //removidos devido novas soluções implementadas o upgrade pode ser direto
                            //QueryExecute("UPDATE FUNCIONARIOS SET SYS_FILTRO = 'MATRIZ'", null, "", _conn);
                            
                            //QueryExecute("UPDATE CONFIGURACAO SET VERSAO = '1.0.11'", null, "", _conn);

                            //foram feitas as correções para versao 1.0.12
                            QueryExecute("UPDATE CONFIGURACAO SET VERSAO = '1.0.11'", null, "", _conn);
                            
                            transac.Commit();
                        }

                        _conn.Close();
                    }
                  
                    /////////////////////////////////////////////////////////////////////    

                    //Atualizações versão 1.0.12 /////////////////////////////////////////
                    if (String.Compare(versaoatualdb, "1.0.12") < 0)
                    {
                        //backup da base de dados
                        BackupDatabase();

                        _conn = GetConn();

                        using (System.Data.SQLite.SQLiteTransaction transac = _conn.BeginTransaction())
                        {
                          
                            QueryExecute("ALTER TABLE FUNCIONARIOS RENAME TO FUNCIONARIOSCOPY", null, "", _conn);

                            QueryExecute("ALTER TABLE CONFIGURACAO ADD COLUMN SHOWNOVIDADES bit null", null, "", _conn);

                            QueryExecute(SqlFuncionario, null, "", _conn);

                            QueryExecute("insert into FUNCIONARIOS select CPF, NIS, NOME, DN, COD_NIS_INV, COD_CPF_INV, COD_NOME_INV, COD_DN_INV, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, WEB_CPF_SIT, WEB_NIS_SIT, WEB_MSG_SIT, SYS_PROCESSADO, SYS_IN_USE, SYS_PROBLEMS, '' from FUNCIONARIOSCOPY", null, "", _conn);

                            QueryExecute("DROP TABLE FUNCIONARIOSCOPY", null, "", _conn);

                            QueryExecute("UPDATE CONFIGURACAO SET VERSAO = '1.0.12', SHOWNOVIDADES = 0", null, "", _conn);

                            transac.Commit();
                        }

                        _conn.Close();

                        //reduz tamanho arquivo
                        QueryExecute("VACUUM");
                    }

                /////////////////////////////////////////////////////////////////////    
                 
                
                
            }

            return true;
        }

        /// <summary>
        /// Executa comandos de Insert, Update, Delete
        /// </summary>
        /// <param name="_sql"></param>
        /// <returns></returns>
        public int QueryExecute(string _sql, List<SQLiteParameter> _parameter = null, string _conexao = "", SQLiteConnection _conn = null)
        {
            int ret = 0;

            //Se com Transacao
            if (_conn != null)
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                    _conn.Open();

                using (SQLiteCommand _cmd = new SQLiteCommand(_sql, _conn))
                {

                    if (_parameter != null)
                        _cmd.Parameters.AddRange(_parameter.ToArray());

                    ret = _cmd.ExecuteNonQuery();
                }

                return ret;
            }

            //Execução Direta
            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            using (SQLiteConnection tmpconn = new SQLiteConnection(_conexao))
            {

                tmpconn.Open();

                if (String.IsNullOrEmpty(_sql))
                    return 0;

                if (tmpconn == null)
                    return 0;

                using (SQLiteCommand _cmd = new SQLiteCommand(_sql, tmpconn))
                {

                    if (_parameter != null)
                        _cmd.Parameters.AddRange(_parameter.ToArray());

                    ret = _cmd.ExecuteNonQuery();
                }

                tmpconn.Close();
                
            }

            return ret;
        }

        /// <summary>
        /// Executa leitura de dados SELECT
        /// </summary>
        /// <param name="_sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public SQLiteDataReader QueryReader(string _sql, out SQLiteConnection tmpconn,  List<SQLiteParameter> _parameter = null, string _conexao = "")
        {
            tmpconn = null;

            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            tmpconn = new SQLiteConnection(_conexao);           
                tmpconn.Open();

                if (String.IsNullOrEmpty(_sql))
                    return null;

                if (tmpconn == null)
                    return null;

            SQLiteCommand _cmd = new SQLiteCommand(_sql, tmpconn);                

                if (_parameter != null)
                    _cmd.Parameters.AddRange(_parameter.ToArray());                    

            SQLiteDataReader   _r = _cmd.ExecuteReader();

                return _r;
           
        }

        public List<Dictionary<string, object>> QueryReader(string _sql, List<SQLiteParameter> _parameter = null, string _conexao = "")
        {
            List<Dictionary<string, object>> r = new List<Dictionary<string, object>>();
            var dict = new Dictionary<string, object>();
                           

            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            using (SQLiteConnection tmpconn = new SQLiteConnection(_conexao))
            {
                tmpconn.Open();

                if (String.IsNullOrEmpty(_sql))
                    return null;

                if (tmpconn == null)
                    return null;

                using (SQLiteCommand _cmd = new SQLiteCommand(_sql, tmpconn))
                {

                    if (_parameter != null)
                        _cmd.Parameters.AddRange(_parameter.ToArray());

                    SQLiteDataReader _r = _cmd.ExecuteReader();

                    while (_r.Read())
                    {
                        Dictionary<string, object> d = new Dictionary<string, object>();

                        for (int i = 0; i < _r.FieldCount; i++)
                        {
                            d[_r.GetName(i)] = _r[i]; 
                            //nada

                        }

                        r.Add(d);
                        d = null;
                    }

                }
            }           

            return r;
        }

        /// <summary>
        /// Obtem uma conexao atual
        /// </summary>
        /// <param name="_conexao"></param>
        /// <returns></returns>
        public SQLiteConnection GetConn(string _conexao = "")
        {
            if (String.IsNullOrEmpty(_conexao))
                _conexao = conexao;

            SQLiteConnection tmpconn = new SQLiteConnection(_conexao);

            tmpconn.Open();

            return tmpconn;

        }


        public string BackupDatabase()
        {
            string _newfile= "";
            try
            {
                System.IO.Directory.CreateDirectory("backup");
                _newfile = "backup/" + "backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_") + nmdatabase;
                System.IO.File.Copy(nmdatabase, _newfile);

                return _newfile;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
