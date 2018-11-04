namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubempenhoCancelamento : LiquidacaoDespesa
    {
        [Column("id_subempenho_cancelamento")]
        public override int Id { get; set; }
        public override string Normal => default(string);
        public override string Estorno => "X";        
    }
}
