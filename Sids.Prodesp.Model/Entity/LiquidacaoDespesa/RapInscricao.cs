
namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tb_rap_inscricao", Schema = "pagamento")]
    public  class RapInscricao : Rap
    {
        [Column("id_rap_inscricao")]
        public override int Id { get; set; }

        public override string Normal => "X";

        public override string TipoRap => "RI";
    }
}
