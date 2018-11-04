namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AcaoService : BaseService
    {
        const string LogTable = "tb_acao";
        IAcao _acao;
        //private Infrastructure.Log.LogErrorDal logErrorDal;

        public AcaoService(ILogError l, IAcao acao) : base(l)
        {
            _acao = acao;
        }

        public List<Acao> Buscar(Acao obj)
        {
            try
            {
                return _acao.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<Acao> ObterAcaoPorFuncionalidade(Funcionalidade objModel)
        {
            try
            {
                return _acao.FetchByFunctionality(objModel).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<Acao> ObterAcaoPorFuncionalidadeEUsuario(int recursoId)
        {
            try
            {
                var recurso = new Funcionalidade { Codigo = recursoId };
                var usuario = new Usuario { Codigo = GetUserIdLogado() };

                return _acao.FetchByUserAndFunctionality(usuario, recurso).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

    }
}
