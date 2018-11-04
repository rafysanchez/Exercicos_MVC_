using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao
{
    public class ReclassificacaoRetencaoNota: IPagamentoContaUnicaNota
    {
        [Column("id_reclassificacao_retencao_nota")]
        public virtual int Id { get; set; }

        [Column("id_reclassificacao_retencao")]
        public virtual int IdReclassificacaoRetencao { get; set; }

        [Column("cd_nota")]
        public string CodigoNotaFiscal { get; set; }

        [Column("nr_ordem")]
        public int Ordem { get; set; }
    }
}
