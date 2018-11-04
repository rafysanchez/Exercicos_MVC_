using System;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.Model.Exceptions;

namespace Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica
{
    public class SiafemPagamentoContaUnicaWs: ISiafemPagamentoContaUnica
    {
        public string InserirReclassificacaoRetencao(string login, string password, string unidadeGestora, SIAFDOC document)
        {

            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string InserirListaBoletos(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string InserirProgramacaoDesembolso(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string InserirProgramacaoDesembolsoSiafisico(string login, string password, string unidadeGestora, Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa.SFCODOC document)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafisico.";

            try
            {
                return DataHelperSiafem<Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa.SFCODOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string CancelarProgramacaoDesembolso(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string Consultar(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        #region Execucao de PD
        public string ConsultaPD(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string ListaPd(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string ExecutaPD(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        #endregion Execucao de PD

        #region Autorizacao de OB

        public string ConsultaOB(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        public string CancelaOB(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }
        

        public string AutorizaOB(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch
            {
                throw new SidsException(errorMessage);
            }
        }

        #endregion Autorizacao de OB

        #region Impressão Relação RE e RT

        public string CancelarImpressaoRelacaoReRt(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch(Exception ex)
            {
                throw new SidsException(errorMessage);
            }
        }

        public string TransmitirImpressaoRelacaoReRt(string login, string password, string unidadeGestora, SIAFDOC siafdoc)
        {
            const string errorMessage = "Erro na comunicação com WebService Siafem.";

            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, siafdoc);
            }
            catch (Exception ex)
            {
                throw new SidsException(errorMessage);
            }
        }

        #endregion Impressão Relação RE e RT
    }
}
