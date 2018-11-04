namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RapAnulacao : Rap
    {
        [Column("id_rap_anulacao")]
        public override int Id { get; set; }

        [Column("nr_requisicao_rap")]
        public string NumeroRequisicaoRap { get; set; }
        
        public override string TipoRap { get { return "RR"; } }
        public override string Normal => default(string);
        public override string Estorno => "X"; //field Estorna
    }
}