namespace Sids.Prodesp.Model.Entity.Empenho
{
    using System.ComponentModel.DataAnnotations.Schema;


    public class Licitacao
    {
        [Column("id_licitacao")]
        public string Id { get; set; }

        [Column("ds_licitacao")]
        public string Descricao { get; set; }
    }
}
