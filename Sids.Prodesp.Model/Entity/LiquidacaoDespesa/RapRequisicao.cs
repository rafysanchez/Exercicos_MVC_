namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RapRequisicao : Rap
    {
        [Column("id_rap_requisicao")]
        public override int Id { get; set; }


        [Column("nr_prodesp_original")]
        public override string NumeroOriginalProdesp { get; set; }
        
        public override string TipoRap => "RR";
        public override string Normal => "X"; //field Inclui
        public override string Estorno => default(string);

        
    }
}
