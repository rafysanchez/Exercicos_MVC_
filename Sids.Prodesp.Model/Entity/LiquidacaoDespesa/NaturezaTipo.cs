namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class NaturezaTipo
    {
        [Column("id_natureza_tipo")]
        public string Id { get; set; }

        [Column("ds_natureza_tipo")]
        public string Descricao { get; set; }
    }
}
