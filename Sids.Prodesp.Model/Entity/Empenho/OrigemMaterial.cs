namespace Sids.Prodesp.Model.Entity.Empenho
{
    using System.ComponentModel.DataAnnotations.Schema;


    public class OrigemMaterial
    {
        [Column("id_origem_material")]
        public int Id { get; set; }

        [Column("ds_origem_material")]
        public string Descricao { get; set; }
    }
}
