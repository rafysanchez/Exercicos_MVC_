namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao
{
    public class SiafemDocMov
    {

        public documento documento { get; set; }
        public documento SiafemDocMovimentacao { get; set; }

        public Repeticao Meses { get; set; }
        public Repeticao Observacao2 { get; set; }
        public cronograma cronograma { get; set; }

        public SiafemDocCelula celula { get; set; }

        public Observacao observacao { get; set; }
        

    }
}
