using System;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface IPagamentoContaUnica
    {
        int Id { get; set; }
        bool CadastroCompleto { get; set; }
        string CodigoAplicacaoObra { get; set; }
        DateTime DataEmissao { get; set; }
        int RegionalId { get; set; }
        DateTime DataCadastro { get; set; }
        string NumeroContrato { get; set; }

        int DocumentoTipoId { get; set; }

        string NumeroDocumento { get; set; }

        DocumentoTipo DocumentoTipo { get; set; }
    }

}

