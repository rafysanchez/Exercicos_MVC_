using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento
{
    [Table("tb_identificacao_desdobramento")]
    public class IdentificacaoDesdobramento : Base.PagamentoContaUnica
    {
        [Key]
        [Column("id_identificacao_desdobramento")]
        public override int Id { get; set; }

        [Column("id_desdobramento")]
        public int Desdobramento { get; set; }


        [Column("id_tipo_desdobramento")]
        public int DesdobramentoTipoId { get; set; }

        [Column("ds_nome_reduzido_credor")]
        public string NomeReduzidoCredor { get; set; }
        
        [Column("vr_percentual_base_calculo")]
        public decimal ValorPercentual { get; set; }
        
        [Column("vr_desdobrado")]
        public decimal ValorDesdobrado { get; set; }

        [Column("vr_desdobrado_inicial")]
        public decimal ValorDesdobradoInicial { get; set; }

        [Column("vr_distribuicao")]
        public decimal ValorDistribuicao { get; set; }

        [Column("id_reter")]
        public int ReterId { get; set; }

        [ForeignKey("ReterId")]
        public Reter Reter { get; set; }

        [Column("bl_tipo_bloqueio")]
        public string TipoBloqueio { get; set; }
        
        [Column("nr_sequencia")]
        public int Sequencia { get; set; }
    }
}
