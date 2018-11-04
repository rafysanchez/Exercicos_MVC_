using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos
{
    public class CodigoBarraTaxa
    {

        [Column("id_codigo_de_barras_taxas")]
        public int Id { get; set; }

        [Column("nr_conta1")]
        public string NumeroConta1 { get; set; }

        [Column("nr_conta2")]
        public string NumeroConta2 { get; set; }

        [Column("nr_conta3")]
        public string NumeroConta3 { get; set; }

        [Column("nr_conta4")]
        public string NumeroConta4 { get; set; }

        [Column("id_codigo_de_barras")]
        public int CodigoBarraId { get; set; }
        
    }
}
