using Sids.Prodesp.Model.Interface.LiquidacaoDespesa;

namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventoTipo: IEvento
    {
        [Column("id_evento_tipo")]
        public int Id { get; set; }

        [Column("ds_evento_tipo")]
        public string Descricao { get; set; }

        [Column("cd_tp_subempenho")]
        public int TpEventoSubempenho { get; set; }
    }
}
