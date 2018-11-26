using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Social_Auto_Qualificar.model
{
    class dtoConfiguracao
    {
        

        public Configuracao GetConfiguracao()
        {
            
            Configuracao c = null;

                dbconnect dbconn = new dbconnect();
                string sql = "SELECT VERSAO, MODOCHK, SOMCAPTCHA, SHOWNOVIDADES FROM CONFIGURACAO";
                var rs = dbconn.QueryReader(sql);

                foreach (Dictionary<string, object> d in rs)
                {
                    c = new Configuracao();
                    c.VERSAO = d["VERSAO"].ToString();
                    c.MODOCHK = d["MODOCHK"].ToString()[0];
                    c.SOMCAPTCHA = (bool)d["SOMCAPTCHA"];
                    c.SHOWNOVIDADES = (bool)d["SHOWNOVIDADES"];
                }

                dbconn = null;

            return c;
        }

        public Configuracao SaveConfiguracao(Configuracao _c)
        {
            Configuracao c = null;

            if (_c == null)
                return c;

            
                dbconnect dbconn = new dbconnect();

                string sql = "Delete from CONFIGURACAO";

                //Deleta dados
                int q = dbconn.QueryExecute(sql);

                sql = "insert into CONFIGURACAO(VERSAO,MODOCHK,SOMCAPTCHA, SHOWNOVIDADES) values (@VERSAO, @MODOCHK, @SOMCAPTCHA,@SHOWNOVIDADES)";
                
                var p = new List<System.Data.SQLite.SQLiteParameter>();
                p.Add(new System.Data.SQLite.SQLiteParameter("VERSAO", _c.VERSAO));
                p.Add(new System.Data.SQLite.SQLiteParameter("MODOCHK", _c.MODOCHK.ToString()));
                p.Add(new System.Data.SQLite.SQLiteParameter("SOMCAPTCHA", (_c.SOMCAPTCHA ? 1 : 0)));
                p.Add(new System.Data.SQLite.SQLiteParameter("SHOWNOVIDADES", (_c.SHOWNOVIDADES ? 1 : 0)));

                q = dbconn.QueryExecute(sql, p);

                dbconn = null;

            //Pega a atualização
            c = GetConfiguracao();

            return c;
    
        }
    }
}
