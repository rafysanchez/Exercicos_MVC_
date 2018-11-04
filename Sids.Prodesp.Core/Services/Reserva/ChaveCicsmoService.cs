using System;
using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Core.Services.Reserva
{
    using Base;
    using Model.Interface.Log;
    using Model.Interface.Reserva;

    public class ChaveCicsmoService : BaseService
    {
        private readonly IChaveCicsmo _chave;

        public ChaveCicsmoService(ILogError l, IChaveCicsmo chave) : base(l)
        {
            _chave = chave;
        }

        public int LiberarChave(int id)
        {
            return _chave.Release(id);
        }

        public ChaveCicsmo ObterChave(int recursoId = 0)
        {
            try
            {
                int userId = GetUserIdLogado();
                userId = userId == 0 ? 1 : userId;
                ChaveCicsmo chave = null;

                for (int i = 0; i <= 10; i++)
                {
                    chave = _chave.Retrieve(userId);
                    if (chave == null || chave.Codigo == 0)
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                    else
                        break;
                }

                if (chave == null)
                {
                    chave = new ChaveCicsmo();
                }

#if DEBUG
                chave.Senha = "DERSIAFEM22716";
#else
            chave.Senha = Decrypt(chave.Senha);
#endif
                return chave;
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Chave de acesso Prodesp indisponível!");
            }
        }
    }
}
