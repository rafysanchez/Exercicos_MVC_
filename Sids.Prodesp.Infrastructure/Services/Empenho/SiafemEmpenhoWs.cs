namespace Sids.Prodesp.Infrastructure.Services.Empenho
{
    using Helpers;
    using Model.Exceptions;
    using Model.Interface.Service.Empenho;
    using System;
    using SFCODOC = Model.ValueObject.Service.Siafem.Empenho.SFCODOC;
    using SIAFDOC = Model.ValueObject.Service.Siafem.Empenho.SIAFDOC;

    public class SiafemEmpenhoWs : ISiafemEmpenho
    {
        public string InserirEmpenhoSiafem(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);

            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
        public string InserirEmpenhoSiafisico(string login, string password, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, password, unidadeGestora, document);

            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }


        public string InserirEmpenhoReforcoSiafem(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
        public string InserirEmpenhoReforcoSiafisico(string login, string password, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }


        public string InserirEmpenhoCancelamentoSiafem(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
        public string InserirEmpenhoCancelamentoSiafisico(string login, string password, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, password, unidadeGestora, document);

            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
        public string ConsultaOC(string login, string password, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
        public string Consultar(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
        public string Consultar(string login, string password, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, password, unidadeGestora, document);
            }
            catch(Exception)
            {
                throw new SidsException("Erro na comunicação com WebService Siafem.");
            }
        }
    }
}
