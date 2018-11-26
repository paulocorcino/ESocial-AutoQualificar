using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_Social_Auto_Qualificar.model;

namespace E_Social_Auto_Qualificar.controller
{
    class Inicial
    {
        public void GetTotal(out int total, out int processados, out int abertos, out int erros)
        {
            total = 0;
            processados = 0;
            abertos = 0;
            erros = 0;

            dbconnect dbconn = new model.dbconnect();
            string sql = "Select IFNULL((Select Count(CPF) From  FUNCIONARIOS Where  SYS_PROCESSADO = 1),0) as totalproc, " +
                                "IFNULL((Select Count(CPF) From  FUNCIONARIOS Where  SYS_PROCESSADO = 0),0) as totalnaoproc, " +
                                "IFNULL((Select Count(CPF) From  FUNCIONARIOS Where  SYS_PROBLEMS = 1),0) as totalerro";


            var rs = dbconn.QueryReader(sql);
            foreach (Dictionary<string, object> d in rs)
            {
                processados = int.Parse(d["totalproc"].ToString());
                abertos = int.Parse(d["totalnaoproc"].ToString());
                erros = int.Parse(d["totalerro"].ToString());
                total = processados + abertos;
            }           

            
        }
    }
}
