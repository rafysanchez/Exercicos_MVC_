namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ObraTipo
    {
        [Column("id_obra_tipo")]
        public int Id { get; set; }

        [Column("ds_obra_tipo")]
        public string Descricao { get; set; }
    }
}
