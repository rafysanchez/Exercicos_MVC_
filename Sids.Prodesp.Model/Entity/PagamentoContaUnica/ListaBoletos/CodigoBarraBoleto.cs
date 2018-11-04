using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos
{
    public class CodigoBarraBoleto
    {

        [Column("id_codigo_de_barras_boleto")]
        public int Id { get; set; }

        [Column("nr_conta_cob1")]
        public string NumeroConta1 { get; set; }

        [Column("nr_conta_cob2")]
        public string NumeroConta2 { get; set; }

        [Column("nr_conta_cob3")]
        public string NumeroConta3 { get; set; }

        [Column("nr_conta_cob4")]
        public string NumeroConta4 { get; set; }

        [Column("nr_conta_cob5")]
        public string NumeroConta5 { get; set; }

        [Column("nr_conta_cob6")]
        public string NumeroConta6 { get; set; }

        [Column("nr_conta_cob7")]
        public string NumeroConta7 { get; set; }

        [Column("nr_digito")]
        public string NumeroDigito { get; set; }

        [Column("id_codigo_de_barras")]
        public int CodigoBarraId { get; set; }
        
    }
}
