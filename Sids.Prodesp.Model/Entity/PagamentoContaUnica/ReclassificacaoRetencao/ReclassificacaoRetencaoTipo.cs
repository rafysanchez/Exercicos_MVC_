using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao
{
    public class ReclassificacaoRetencaoTipo : ITipoPagamentoContaUnica
    {
        [Column("id_tipo_reclassificacao_retencao")]
        public int Id { get; set; }
        [Column("ds_tipo_reclassificacao_retencao")]
        public string Descricao { get; set; }
    }
}
