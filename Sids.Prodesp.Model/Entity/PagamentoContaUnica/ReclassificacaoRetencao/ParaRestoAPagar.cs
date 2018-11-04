using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao
{
    public class ParaRestoAPagar
    {
        [Column("id_resto_pagar")]
        public string Id { get; set; }
        [Column("ds_resto_pagar")]
        public string Descricao { get; set; }
    }
}
