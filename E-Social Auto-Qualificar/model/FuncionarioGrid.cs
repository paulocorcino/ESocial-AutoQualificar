using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Social_Auto_Qualificar.model
{
    class FuncionarioGrid
    {
        public string CPF { get; set; }  //11 Numérico
        public string NIS { get; set; } //11 Numérico
        public string NOME { get; set; } //60 Alfanumérico Nome que consta do cadastro do CPF
        public string DN { get; set; } //08 Data (DDMMAAAA)
        public string SITUACAO { get; set; } //08 Data (DDMMAAAA)

    }
}
