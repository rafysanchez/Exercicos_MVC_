namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpenhoReforcoItem : BaseEmpenhoItem
    {
        [Column("id_item_reforco")]
        public override int Id { get; set; }
        
        [Column("tb_empenho_reforco_id_empenho_reforco")]
        public override int EmpenhoId { get; set; }
               
    }
}
