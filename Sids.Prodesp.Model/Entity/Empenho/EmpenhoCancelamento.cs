namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class EmpenhoCancelamento : BaseEmpenho
    {
        [Column("id_empenho_cancelamento")]
        public override int Id { get; set; }

        [Column("tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo")]
        public int EmpenhoCancelamentoTipoId { get; set; }

        [Column("cd_empenho")]
        public string CodigoEmpenho { get; set; }

        [Column("cd_empenho_original")]
        public string CodigoEmpenhoOriginal { get; set; }
        
        [Column("cd_fonte_siafisico")]
        public string CodigoFonteSiafisico { get; set; }

        [Column("nr_natureza_ne")]
        public string CodigoNaturezaNe { get; set; }

    }
}
