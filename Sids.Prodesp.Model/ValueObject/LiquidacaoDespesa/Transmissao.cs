namespace Sids.Prodesp.Model.ValueObject.LiquidacaoDespesa
{
    using System;

    public class Transmissao
    {
        public string Status { get; set; }
        public bool Transmitido { get; set; }
        public DateTime DataTransmitido { get; set; }
        public string Mensagem { get; set; }
    }
}
