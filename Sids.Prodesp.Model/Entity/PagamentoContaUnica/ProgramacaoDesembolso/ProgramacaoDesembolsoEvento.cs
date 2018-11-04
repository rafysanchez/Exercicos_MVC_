using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso
{
    public class ProgramacaoDesembolsoEvento : IPagamentoContaUnicaEvento
    {
        [Column("id_programacao_desembolso_evento")]
        public virtual int Id { get; set; }
        
        [Column("id_programacao_desembolso")]
        public virtual int PagamentoContaUnicaId { get; set; }

        [Column("cd_fonte")]
        public string Fonte { get; set; }

        [Column("cd_evento")]
        public string NumeroEvento { get; set; }

        [Column("cd_classificacao")]
        public string Classificacao { get; set; }

        [Column("ds_inscricao")]
        public string InscricaoEvento { get; set; }

        [Column("vl_evento")]
        public int ValorUnitario { get; set; }
    }
}
