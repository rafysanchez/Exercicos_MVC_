namespace Sids.Prodesp.Infrastructure.Services.Movimentacao
{
    using Helpers;
    using Model.Interface.Service.LiquidacaoDespesa;
    using Model.Interface.Service.Movimentacao;
    //using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using System;
    using Model.ValueObject.Service.Siafem.Movimentacao;
    using Model.Exceptions;

    public class SiafemMovimentacaoWs : ISiafemMovimentacao
    {
        const string ERROR_MESSAGE = "Erro na comunicação com WebService Siafem.";

        public string Consultar(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
            catch { throw new SidsException(ERROR_MESSAGE); }
        }

        public string InserirInserirMovimentacaoOrcamentaria(string login, string password, string unidadeGestora, Model.ValueObject.Service.Siafem.Movimentacao.SIAFDOC document)
        {
            try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
            catch { throw new SidsException(ERROR_MESSAGE); }
        }

        //public string InserirCancelamentoTesouroSiafem(string login, string password, string unidadeGestora, Model.ValueObject.Service.Siafem.Movimentacao.SIAFDOC document)
        //{
        //    try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
        //    catch { throw new SidsException(ERROR_MESSAGE); }
        //}

        //public string InserirDistribuicaoNaoTesouroSiafem(string login, string password, string unidadeGestora, Model.ValueObject.Service.Siafem.Movimentacao.SIAFDOC document)
        //{
        //    try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
        //    catch { throw new SidsException(ERROR_MESSAGE); }
        //}

        //public string InserirDistribuicaoTesouroSiafem(string login, string password, string unidadeGestora, Model.ValueObject.Service.Siafem.Movimentacao.SIAFDOC document)
        //{
        //    try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
        //    catch { throw new SidsException(ERROR_MESSAGE); }
        //}

        //public string InserirNotaCreditoSiafem(string login, string password, string unidadeGestora, Model.ValueObject.Service.Siafem.Movimentacao.SIAFDOC document)
        //{
        //    try { return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document); }
        //    catch { throw new SidsException(ERROR_MESSAGE); }
        //}
    }
}
