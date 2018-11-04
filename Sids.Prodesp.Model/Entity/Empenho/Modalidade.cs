namespace Sids.Prodesp.Model.Entity.Empenho
{
    using System.ComponentModel.DataAnnotations.Schema;


    public class Modalidade
    {
        [Column("id_modalidade")]
        public int Id { get; set; }

        [Column("ds_modalidade")]
        public string Descricao { get; set; }
    }
}
