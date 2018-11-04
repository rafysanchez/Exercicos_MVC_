using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
     public interface IProgramacaoDesembolso: IPagamentoContaUnicaSiafem
    {
         int ProgramacaoDesembolsoTipoId { get; set; }

         string NumeroProcesso { get; set; }
        
         string CodigoUnidadeGestora { get; set; }

        string CodigoGestao { get; set; }

        decimal Valor { get; set; }

        string NumeroListaAnexo { get; set; }

         string NumeroNLReferencia { get; set; }

         string Finalidade { get; set; }

         string CodigoDespesa { get; set; }
        
         DateTime DataVencimento { get; set; }

         string NumeroCnpjcpfCredor { get; set; }

         string GestaoCredor { get; set; }

         string NumeroBancoCredor { get; set; }

         string NumeroAgenciaCredor { get; set; }

         string NumeroContaCredor { get; set; }

         string NumeroCnpjcpfPagto { get; set; }

         string GestaoPagto { get; set; }

         string NumeroBancoPagto { get; set; }

         string NumeroAgenciaPagto { get; set; }

         string NumeroContaPagto { get; set; }

         string NumeroSiafem { get; set; }
        
         bool Bloqueio { get; set; }

        bool Cancelado { get; set; }

        int NumeroAgrupamento { get; set; }

         string NumeroDocumentoGerador { get; set; }

         string CausaCancelamento { get; set; }

        DateTime DataEmissao { get; set; }

        int TipoBloqueio { get; set; }
        
        bool? Bloquear { get; set; }
    }
}
