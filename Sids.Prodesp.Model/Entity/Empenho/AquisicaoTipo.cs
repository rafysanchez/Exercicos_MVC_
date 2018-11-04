namespace Sids.Prodesp.Model.Entity.Empenho
{
    using System.ComponentModel.DataAnnotations.Schema;


    public class AquisicaoTipo
    {
        [Column("id_aquisicao_tipo")]
        public int Id { get; set; }

        [Column("ds_aquisicao_tipo")]
        public string Descricao { get; set; }
    }
}
