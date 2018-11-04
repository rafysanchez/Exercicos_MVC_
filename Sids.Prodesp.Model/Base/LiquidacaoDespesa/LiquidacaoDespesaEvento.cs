namespace Sids.Prodesp.Model.Base.LiquidacaoDespesa
{
    using Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiquidacaoDespesaEvento : ILiquidacaoDespesaEvento
    {
        [Column("id_subempenho_evento")]
        public virtual int Id { get; set; }

        [Column("tb_subempenho_id_subempenho")]
        public virtual int SubempenhoId { get; set; }

        [Column("cd_fonte")]
        public string Fonte { get; set; }

        [Column("cd_evento")]
        public string NumeroEvento { get; set; }

        [Column("cd_classificacao")]
        public string Classificacao { get; set; }

        [Column("ds_inscricao")]
        public string InscricaoEvento { get; set; }

        [Column("vl_evento")]
        public int ValorUnitario { get; set; }
    }
}
