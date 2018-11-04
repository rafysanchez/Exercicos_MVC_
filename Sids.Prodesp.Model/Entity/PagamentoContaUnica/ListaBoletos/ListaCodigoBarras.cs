using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos
{
    public class ListaCodigoBarras
    {
        public ListaCodigoBarras()
        {
            TipoBoleto = new TipoBoleto();
        }

        [Column("id_codigo_de_barras")]
        public int Id { get; set; }

        [Column("id_lista_de_boletos")]
        public int ListaBoletosId { get; set; }

        [Column("id_tipo_de_boleto")]
        public int TipoBoletoId { get; set; }

        public TipoBoleto TipoBoleto { get; set; }
        
        [Column("vr_boleto")]
        public decimal Valor { get; set; }

        [Column("bl_transmitido_siafem")]
        public bool TransmitidoSiafem { get; set; }

        public CodigoBarraTaxa CodigoBarraTaxa { get; set; }
        public CodigoBarraBoleto CodigoBarraBoleto { get; set; }
    }
}
