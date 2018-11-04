namespace Sids.Prodesp.Infrastructure.Services.LiquidacaoDespesa
{
    using Helpers;
    using Model.Interface.Service.LiquidacaoDespesa;
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using System;

    public class SiafemLiquidacaoDespesaWs : ISiafemLiquidacaoDespesa
    {
        const string ERROR_MESSAGE = "Erro na comunicação com WebService Siafem.";


        public string InserirSubempenhoSiafem(string login, string password, string unidadeGestora, SIAFDOC document, bool tagId = false)
        {
            try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document, tagId); }
            catch { throw new Exception(ERROR_MESSAGE); }
        }

        public string InserirSubempenhoSiafisico(string login, string password, string unidadeGestora, SFCODOC document, bool tagId = false)
        {
            try { return DataHelperSiafem<SFCODOC>.Send(login, password, unidadeGestora, document, tagId);  }
            catch { throw new Exception(ERROR_MESSAGE); }
        }

        public string Consultar(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
            catch { throw new Exception(ERROR_MESSAGE); }
        }

        public string InserirRapInscricaoSiafem(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new Exception(ERROR_MESSAGE);
            }
        }

        public string InserirRapRequisicaoApoioSiafem(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
            catch { throw new Exception(ERROR_MESSAGE); }
        }

        public string InserirRapAnulacaoApoioSiafem(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
            catch { throw new Exception(ERROR_MESSAGE); }
        }
    }
}
