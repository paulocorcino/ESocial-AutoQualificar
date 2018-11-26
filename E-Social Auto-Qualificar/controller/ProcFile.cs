using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Data.SQLite;

namespace E_Social_Auto_Qualificar.controller
{
    class ProcFile
    {
        //Funcionarios
        List<model.Funcionarios> LFuncionarios = new List<model.Funcionarios>();
        model.dbconnect dbconn;

        //variaveis do modulo
        string srcfile;
        int countproc;
        int countignor;
        bool isIncrement;
        bool isSobrescrever;
        bool retproc = false;

        bool _isAlive;
        string _erro;
        string _linesproc;

        public bool isAlive { get { return _isAlive; } }
        public string Erro { get { return _erro; } }
        public string NumLinesproc { get { return "Processado...: " + countproc.ToString() + " / Ignorados...: " + countignor.ToString(); } }
        public int NumCountProc { get { return countproc; } }
        public int NumCountIg { get { return countignor; } }

        Thread t = null;


        public void ProcFileCSV(string _txt, bool _isIncrement, bool _issobrescrever)
        {

            if (String.IsNullOrEmpty(_txt))
            {
                _erro = "Informe um arquivo para importar";
                _isAlive = false;
                return;
            }

            srcfile = _txt;
            isIncrement = _isIncrement;
            isSobrescrever = _issobrescrever;

            _erro = "";

            ThreadStart ts = new ThreadStart(Processando);
            t = new Thread(ts);
            t.IsBackground = true;
            t.Start();

            _isAlive = true;

            return;

        }

        private void Processando()
        {

            int count  = 0;
            int countf = 0;
            dbconn = new model.dbconnect();
            int ret = -1;
            //var c = dbconn.GetConn();
            dbconn._conn = dbconn.GetConn();

            //deve deletar tudo
            if (!isIncrement)
            {
                
                //backup antes de excluir
                dbconn.BackupDatabase();

                

                using (System.Data.SQLite.SQLiteTransaction transac = dbconn._conn.BeginTransaction())
                {

                    ret = dbconn.QueryExecute("delete from FUNCIONARIOS",null,"",dbconn._conn);
                    transac.Commit();                        
                }

                if(ret == -1){                    
                    _erro = "Não foi possível exluir os dados do funcionários. Veja se existe alguem utilizando o sistema na rede.";
                    _isAlive = false;
                    return;
                }

                dbconn._conn.Close();
            }

     
            //const Int32 BufferSize = 1024;
           // using (var fileStream = File.OpenRead(srcfile)){
                //using (var streamReader = new StreamReader(fileStream, Encoding.Default, true, BufferSize)) {

            // Read TXT File;
            List<string> lines = new List<string>();

            using (FileStream fs = File.Open(srcfile, FileMode.Open)){
                using (BufferedStream bs = new BufferedStream(fs)){
					using (StreamReader streamReader = new StreamReader(bs)){
					    String line;
					    while ((line = streamReader.ReadLine()) != null)
					    {
						    //Linha processada
						    line = line.Replace("\"", "").Replace(",", ";").Replace(".","").Replace("/","").Replace("-","");
                            lines.Add(line);
                        }
                    }
                }
            }

            if (lines.Count > 0)
            {
                dbconn._conn.Open();

                using (System.Data.SQLite.SQLiteTransaction transac = dbconn._conn.BeginTransaction())
                {
                    foreach (string line in lines)
                    {
                        string[] dtos = line.Split(';');
                        if (dtos.Length >= 4)
                        {  
                            ret = -1;
                            var p = new List<System.Data.SQLite.SQLiteParameter>();    
                            p.Add(new SQLiteParameter("CPF", (String.IsNullOrEmpty(dtos[0]) || dtos[0].Length < 11 ? "" : dtos[0])));
                            p.Add(new SQLiteParameter("NIS", (String.IsNullOrEmpty(dtos[1]) || dtos[1].Length < 11 ? "" : dtos[1])));
                            p.Add(new SQLiteParameter("NOME", (String.IsNullOrEmpty(dtos[2]) ? "SEM NOME" : dtos[2])));
                            p.Add(new SQLiteParameter("DN", (String.IsNullOrEmpty(dtos[3]) || dtos[3].Length < 8 ? "" : dtos[3])));
                            p.Add(new SQLiteParameter("SYS_FILTRO", (dtos.Length == 4 ? "" : (String.IsNullOrEmpty(dtos[4]) ? "" : dtos[4]))));

                            ret = dbconn.QueryExecute("insert into FUNCIONARIOS(CPF, NIS, NOME, DN, SYS_FILTRO) values (@CPF, @NIS, @NOME, @DN, @SYS_FILTRO)", p, "", dbconn._conn);


                            if (ret == -1)
                            {
                                if (isSobrescrever)
                                {
                                    p.Clear();
                                    p.Add(new SQLiteParameter("CPF", (String.IsNullOrEmpty(dtos[0]) || dtos[0].Length < 11 ? "" : dtos[0])));
                                    ret = dbconn.QueryExecute("delete from funcionarios where CPF = @CPF", p);
                                    if (ret == -1)
                                        countf++;
                                    else
                                        count++;
                                }
                                else
                                {

                                    countf++;
                                }

                            }
                            else
                            {
                                count++;
                            }                              


                        }

                        countproc = count;
                        countignor = countf;
                    }

                    transac.Commit();
                }

                dbconn._conn.Close();
               
            }
            

            _erro = "";
            _isAlive = false;
            return;
        }
    }
}
