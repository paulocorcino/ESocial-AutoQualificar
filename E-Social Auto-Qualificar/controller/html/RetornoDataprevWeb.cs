using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Social_Auto_Qualificar.controller.html
{
    class RetornoDataprevWeb
    {
        /*
        cell: CPF
        cell: NIS
        cell: Nome
        cell: Data Nascimento
        cell: Situação no CNIS
        cell: Situação no CPF
        cell: Mensagem */

        public string CPF { get; set; }
        public string NIS { get; set; }
        public string Nome { get; set; }
        public string DN { get; set; }
        public string SIT_NIS { get; set; }
        public string SIT_CPF { get; set; }
        public string SIT_MSG { get; set; }
    }
}
