using System;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface IDesdobramento: IPagamentoContaUnica
    {
        bool TransmitirProdesp { get; set; }
        bool TransmitidoProdesp { get; set; }
        DateTime DataTransmitidoProdesp { get; set; }
        string MensagemServicoProdesp { get; set; }
        string StatusProdesp { get; set; }
        bool StatusDocumento { get; set; }
    }
}
