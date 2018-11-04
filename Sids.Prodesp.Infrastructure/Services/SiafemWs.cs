using Sids.Prodesp.Infrastructure.Helpers;
using System;
using Sids.Prodesp.Interface.Interface.Service;
using login = Sids.Prodesp.Model.ValueObject.Service.Login;
using Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem;

namespace Sids.Prodesp.Infrastructure.Services
{
    public class SiafemWs : ISiafem
    {

        public string Login(string login, string senha, string unidadeGestora, login.SIAFDOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<login.SIAFDOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string AlterarSenha(string login, string senha, string unidadeGestora, login.SIAFDOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<login.SIAFDOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirReservaSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirReservaSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }


        public string InserirReforcoSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirReforcoSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string InserirCancelamentoReserva(string login, string senha, string unidadeGestora, SIAFDOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }


        public string ConsultaOC(string login, string senha, string unidadeGestora, SFCODOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SFCODOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string ConsultaNr(string login, string senha, string unidadeGestora, SIAFDOC siafdoc)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.EmviarMensagem(login, senha, unidadeGestora, siafdoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }
    }

}