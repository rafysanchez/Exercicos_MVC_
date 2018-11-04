namespace Sids.Prodesp.Infrastructure.Services.Reserva
{
    using Helpers;
    using Model.Interface.Service.Reserva;
    using Model.ValueObject.Service.Siafem.Reserva;
    using System;

    public class SiafemReservaWs : ISiafemReserva
    {
        public string InserirReservaSiafem(string login, string senha, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, senha, unidadeGestora, document);

            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirReservaSiafisico(string login, string senha, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, senha, unidadeGestora, document);

            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }


        public string InserirReforcoSiafem(string login, string senha, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, senha, unidadeGestora, document);

            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirReforcoSiafisico(string login, string senha, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, senha, unidadeGestora, document);

            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirCancelamentoReserva(string login, string senha, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, senha, unidadeGestora, document);

            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }


        public string ConsultaOC(string login, string senha, string unidadeGestora, SFCODOC document)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.Send(login, senha, unidadeGestora, document);
            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string ConsultaNr(string login, string senha, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, senha, unidadeGestora, document);
            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }
    }
}