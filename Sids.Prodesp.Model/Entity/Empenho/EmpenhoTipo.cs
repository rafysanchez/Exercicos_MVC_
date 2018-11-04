namespace Sids.Prodesp.Model.Entity.Empenho
{
    using System.ComponentModel.DataAnnotations.Schema;


    public class EmpenhoTipo
    {
        [Column("id_empenho_tipo")]
        public int Id { get; set; }

        [Column("ds_empenho_tipo")]
        public string Descricao { get; set; }
    }
}
