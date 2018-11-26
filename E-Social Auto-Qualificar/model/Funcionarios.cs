using System;
using System.Collections.Generic;
using System.Text;

namespace E_Social_Auto_Qualificar.model
{
    class Funcionarios
    {
        public Funcionarios()
        {
        }

        public string CPF { get; set; }  //11 Numérico
        public string NIS { get; set; } //11 Numérico
        public string NOME { get; set; } //60 Alfanumérico Nome que consta do cadastro do CPF
        public string DN { get; set; } //08 Data (DDMMAAAA)
        public bool? COD_NIS_INV { get; set; } //01 Numérico 0 –> OK; 1 -> NIS inválido
        public bool? COD_CPF_INV { get; set; } //01 Numérico 0 –> OK; 1 -> CPF inválido
        public bool? COD_NOME_INV { get; set; }//01 Numérico 0 –> OK; 1 -> NOME inválido
        public bool? COD_DN_INV { get; set; } //Numérico 0 –> OK; 1 -> DN inválida
        public bool? COD_CNIS_NIS { get; set; } //Numérico 0 –> OK ; 1 -> NIS inconsistente
        public bool? COD_CNIS_DN { get; set; } ///01 Numérico 0 –> OK ; 1 -> Data de Nascimento infor-mada diverge da existente no CNIS.
        public bool? COD_CNIS_OBITO { get; set; }//01 Numérico 0 –> OK ; 1 -> NIS com óbito no CNIS
        public bool? COD_CNIS_CPF { get; set; } //01 Numérico 0 –> OK ; 1 -> CPF informado diverge do existente no CNIS
        public bool? COD_CNIS_CPF_NAO_INF { get; set; } //01 Numérico 0 –> OK ; 1 -> CPF não preenchido no CNIS
        public bool? COD_CPF_NAO_CONSTA { get; set; } //01 Numérico 0 - > OK ; 1 - > CPF informado não consta no Cadastro CPF
        public bool? COD_CPF_NULO { get; set; } //01 Numérico 0 –> OK ; 1 -> CPF informado NULO no Cadastro CPF
        public bool? COD_CPF_CANCELADO { get; set; } //01 Numérico 0 –> OK ; 1 -> CPF informado CANCELA-DO no Cadastro CPF
        public bool? COD_CPF_SUSPENSO { get; set; } //01 Numérico 0 –> OK ; 1 -> CPF informado SUSPENSO no Cadastro CPF
        public bool? COD_CPF_DN { get; set; } //01 Numérico 0 –> OK ; 1 -> Data de Nascimento informada diverge da existente no Cadastro CPF.
        public bool? COD_CPF_NOME { get; set; } //01 Numérico 0 –> OK ; 1 -> NOME informado diverge do existente no Cadastro CPF.
        public bool? COD_ORIENTACAO_CPF { get; set; } //01 Numérico 0 -> OK ; 1 -> Procurar Conveniadas da RFB(1) .
        public int? COD_ORIENTACAO_NIS { get; set; } //01 Numérico 0 -> OK ; 1 -> Atualizar NIS no
        public string WEB_CPF_SIT { get; set; } //Situcao CPF SIte 
        public string WEB_NIS_SIT { get; set; } //Situcao NIS SIte
        public string WEB_MSG_SIT { get; set; } //situacao mensagem
        public bool SYS_PROCESSADO { get; set; } //codigo sistema processado ou nao
        public bool SYS_IN_USE { get; set; } //codigo esta em uso    
        public bool SYS_PROBLEMS { get; set; } //funcionario tem algum problema   
        public string SYS_FILTRO { get; set; } //filtros
        


        //Adicionar dados rapido ao objeto
        public Funcionarios(string CPF, string PIS, string Nome, string DataNasc, string Filtro = "")
        {
            this.CPF = CPF;
            this.NIS = PIS;
            this.NOME = Nome;
            this.DN = DataNasc;
            this.SYS_FILTRO = Filtro;
        }

    }
}
