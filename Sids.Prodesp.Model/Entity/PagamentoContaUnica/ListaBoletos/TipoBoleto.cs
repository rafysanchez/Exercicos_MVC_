using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos
{
    public class TipoBoleto: ITipoPagamentoContaUnica
    {
        [Column("id_tipo_de_boleto")]
        public int Id { get; set; }
        [Column("ds_tipo_de_boleto")]
        public string Descricao { get; set; }
    }
}
