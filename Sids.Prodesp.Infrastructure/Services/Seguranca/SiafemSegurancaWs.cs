namespace Sids.Prodesp.Infrastructure.Services.Seguranca
{
    using Helpers;
    using Model.Interface.Service.Seguranca;
    using Model.ValueObject.Service.Login;
    using System;

    public class SiafemSegurancaWs : ISiafemSeguranca
    {
        public string Login(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);
            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }

        public string AlterarSenha(string login, string password, string unidadeGestora, SIAFDOC document)
        {
            try
            {
                return DataHelperSiafem<SIAFDOC>.Send(login, password, unidadeGestora, document);

            }
            catch
            {
                throw new Exception("Erro na comunicação com WebService Siafem.");
            }
        }
    }
}