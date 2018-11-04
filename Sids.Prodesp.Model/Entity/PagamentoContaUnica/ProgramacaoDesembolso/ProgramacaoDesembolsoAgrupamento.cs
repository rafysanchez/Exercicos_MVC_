using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso
{
    public class ProgramacaoDesembolsoAgrupamento: ProgramacaoDesembolsoBase
    {
        [Column("id_programacao_desembolso_agrupamento")]
        public override int Id { get; set; }
        
        [Column("id_programacao_desembolso")]
        public int PagamentoContaUnicaId { get; set; }
     
        [Display(Name = "Valor")]
        [Column("vl_valor")]
        public override decimal Valor { get; set; }


        [Column("nr_sequencia")]
        public int Sequencia { get; set; }
        
        [NotMapped]
        public string Regional { get; set; }
        

        [Display(Name = "Nome Reduzido do Credor")]
        [Column("nm_reduzido_credor")]
        public string NomeCredorReduzido { get; set; }
        
        [Column("cd_fonte")]
        public string Fonte { get; set; }

        [Column("cd_evento")]
        public string NumeroEvento { get; set; }

        [Column("cd_classificacao")]
        public string Classificacao { get; set; }

        [Column("ds_inscricao")]
        public string InscricaoEvento { get; set; }

        [Display(Name = "Mensagem de Erro")]
        [Column("ds_msg_retorno")]
        public override string MensagemServicoSiafem { get; set; }
    }
}
