using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento
{
    public class Reter: ITipoPagamentoContaUnica
    {
        [Column("id_Reter")]
        public int Id { get; set; }

        [Column("ds_Reter")]
        public string Descricao { get; set; }
    }
}
