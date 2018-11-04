namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpenhoReforco : BaseEmpenho
    {
        [Column("id_empenho_reforco")]
        public override int Id { get; set; }

        [Column("cd_empenho")]
        public string CodigoEmpenho { get; set; }

        [Column("cd_empenho_original")]
        public string CodigoEmpenhoOriginal { get; set; }
               
        [Column("cd_fonte_siafisico")]
        public string CodigoFonteSiafisico { get; set; }

        [Column("nr_natureza_ne")]
        public string  CodigoNaturezaNe{ get; set; }

    }
}
