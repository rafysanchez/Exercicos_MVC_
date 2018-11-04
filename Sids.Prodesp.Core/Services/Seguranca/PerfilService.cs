namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Enum;
    using Model.Exceptions;

    public class PerfilService : BaseService
    {
        const string LogTable = "tb_perfil";
        readonly ICrudPerfil perfil;
        readonly PerfilAcaoService perfilAcaoeService;
        //private Infrastructure.Log.LogErrorDal logErrorDal;
        //private Infrastructure.DataBase.Seguranca.PerfilDal perfilDal;
        //private Infrastructure.DataBase.Seguranca.PerfilFuncionalidadeDal perfilRecursoDal;

        public PerfilService(ILogError l, ICrudPerfil r, ICrudPerfilAcao pr)
            : base(l)
        {
            perfil = r;
            perfilAcaoeService = new PerfilAcaoService(l, pr);
        }

        public AcaoEfetuada Salvar(Perfil obj, List<PerfilAcao> recursos, int recursoId, short actionId)
        {
            try
            {
                if (obj.Codigo == 0)
                {
                    if (perfil.Fetch(new Perfil { Descricao = obj.Descricao }).Any())
                        throw new SidsException("Registro já existente!");

                    PreInsertModel(obj);
                    int id = perfil.Add(obj);
                    SalvarAcoes(id, recursos);

                    var arg = String.Format("Nome {0}", obj.Descricao);

                    return LogSucesso(actionId, recursoId, arg.ToString());
                }
                else
                {

                    if (Buscar(new Perfil { Codigo = obj.Codigo }).FirstOrDefault().Administrador == true && obj.Status == false)
                        throw new SidsException("O perfil Admistrador não pode ser Desativado!");

                    var perfilsalvo = perfil.Fetch(new Perfil { Descricao = obj.Descricao }).ToList();

                    if (perfilsalvo.Count(x => x.Descricao == obj.Descricao && x.Codigo != obj.Codigo) > 0)
                        throw new SidsException("Registro já existente!");

                    SalvarAcoes(obj.Codigo, recursos);
                    perfil.Edit(obj);

                    var arg = String.Format("Nome {0}", obj.Descricao);

                    return LogSucesso(actionId, recursoId, arg.ToString());
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        private void SalvarAcoes(int id, List<PerfilAcao> acoes)
        {
            try
            {
                acoes = acoes ?? new List<PerfilAcao>();

                var acoesSalvas = ObterAcoesPorPerfil(id);

                foreach (var acao in acoesSalvas)
                {
                    perfilAcaoeService.Excluir(acao.Codigo);
                }

                foreach (PerfilAcao item in acoes)
                {
                    item.Perfil = id;
                    item.Status = true;
                    perfilAcaoeService.Salvar(item);
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<Perfil> Buscar(Perfil obj)
        {
            try
            {
                return perfil.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<Perfil> ObterPerfilPorUsuario(Usuario obj)
        {
            try
            {
                return perfil.FetchByUser(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        private int ObterNumeroUsuariosPorPerfil(int id)
        {
            try
            {
                return perfil.GetUserCountByProfileId(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        private List<PerfilAcao> ObterAcoesPorPerfil(int id)
        {
            try
            {
                return perfilAcaoeService.ObterPerfilAcaoPorPerfil(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public AcaoEfetuada Excluir(int id, int recursoId, short actionId)
        {
            try
            {
                if (ObterNumeroUsuariosPorPerfil(id) > 0)
                    throw new SidsException("O perfil não pode ser excluído!");

                if(Buscar(new Perfil { Codigo = id }).FirstOrDefault().Administrador == true)
                    throw new SidsException("O perfil Admistrador não pode ser excluído!");

                perfil.Remove(id);

                return LogSucesso(actionId, recursoId, id.ToString());
            }
            catch (Exception ex)
            {
                throw SaveLog(ex,actionId: actionId,functionalityId: recursoId);
            }
        }
    }

}
