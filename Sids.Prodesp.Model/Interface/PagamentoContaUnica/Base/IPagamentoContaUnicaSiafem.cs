using System;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface IPagamentoContaUnicaSiafem : IPagamentoContaUnica
    {
        string NumeroSiafem { get; set; }
        bool TransmitirSiafem { get; set; }
        bool TransmitidoSiafem { get; set; }
        DateTime DataTransmitidoSiafem { get; set; }
        string MensagemServicoSiafem { get; set; }
        string StatusSiafem { get; set; }
    }
}
