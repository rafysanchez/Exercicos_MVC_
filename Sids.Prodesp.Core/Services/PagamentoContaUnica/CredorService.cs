using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.Configuracao;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class CredorService : BaseService
    {
        private readonly ICrudCredor _credor;
        private readonly ProdespPagamentoContaUnicaService _prodesp;
        private readonly ChaveCicsmoService _chave;

        public CredorService(ILogError log, ICrudCredor credor) : base(log)
        {
            _chave = new ChaveCicsmoService(log, new ChaveCicsmoDal());
            _credor = credor;
            _prodesp = new ProdespPagamentoContaUnicaService(log, new ProdespPagamentoContaUnicaWs());
        }

        public void Excluir(int recursoId)
        {
            try
            {
                _credor.Delete();
            }
            catch
            {
                throw;
            }
        }

        public void Salvar(Credor credor)
        {
            try
            {
                _credor.Save(credor);
            }
            catch
            {
                throw;
            }
        }

        public void AtualizarcCredor(int recursoId)
        {
            try
            {
                Excluir(recursoId);

                var credores = ConsultaCredor().ToList();

                Parallel.ForEach(credores, Salvar);

                var lista = (IEnumerator<Credor>)_credor.Fetch(new Credor());
                SetCurrentCache(lista,"Credor");

                if (recursoId > 0) LogSucesso(1, recursoId, @"Atualizado table de Credores");

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, 1, recursoId);
            }
        }

        public IEnumerable<Credor> Listar(Credor credor)
        {
            var lista = (IEnumerable<Credor>)GetCurrentCache<Credor>("Credor");

            if (lista != null)
            {
                return lista;
            }

            var credores = (IEnumerator<Credor>)_credor.Fetch(new Credor());
            SetCurrentCache(credores, "Credor");

            return (IEnumerable<Credor>) credores;
        }
        
        public IEnumerable<Credor> ListarPesquisa(string credorNome)
        {
            List<Credor> credorList = new List<Credor>();

            var credores = _credor.Fetch(new Credor()).ToList();

            credorList.AddRange(credores.Where(x => x.Prefeitura.Contains(credorNome)));
            credorList.AddRange(credores.Where(x => !x.Prefeitura.Contains(credorNome)));

            return credorList;
        }
        private IEnumerable<Credor> ConsultaCredor()
        {

#if DEBUG
            int? userId = GetUserIdLogado();
            userId = userId == 0 ? 1 : 0;
            var chave = _chave.ObterChave((int)userId);
#else
           var chave = _chave.ObterChave(GetUserIdLogado());
#endif
            try
            {
                return _prodesp.ConsultaCredorReduzido(chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

    }
}
