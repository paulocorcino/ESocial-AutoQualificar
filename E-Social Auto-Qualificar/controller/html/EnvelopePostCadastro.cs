using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Social_Auto_Qualificar.controller.html
{
    class EnvelopePostCadastro
    {
        //name="formQualificacaoCadastral" value="formQualificacaoCadastral" 
        public string formQualificacaoCadastral { get { return "formQualificacaoCadastral"; } }
        //formQualificacaoCadastral:cpf
        public string cpf { get; set; }
        //formQualificacaoCadastral:nis
        public string nis { get; set; }
        //formQualificacaoCadastral:nome
        public string nome { get; set; }
        //formQualificacaoCadastral:dataNascimento
        public string dataNascimento { get; set; }
        //formQualificacaoCadastral:btAdicionar = Adicionar
        public string btAdicionar { get { return "Adicionar"; } }

        public EnvelopePostCadastro(string CPF, string nis, string nome, string dataNascimento)
        {
            this.cpf = CPF;
            this.nis = nis;
            this.nome = nome;
            this.dataNascimento = dataNascimento;
        }
    }
}
