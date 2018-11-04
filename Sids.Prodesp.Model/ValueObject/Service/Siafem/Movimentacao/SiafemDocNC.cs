namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao
{
    public class SiafemDocNC
    {

        public documentoNC documento { get; set; }

        public Repeticao Meses { get; set; }
        public Repeticao Observacao2 { get; set; }
        public cronograma cronograma { get; set; }

        public SiafemDocCelula celula { get; set; }

        public Observacao observacao { get; set; }


    }
}
