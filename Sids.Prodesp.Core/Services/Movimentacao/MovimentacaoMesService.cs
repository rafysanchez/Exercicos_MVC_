namespace Sids.Prodesp.Core.Services.Movimentacao
{
    using Base;
    using Model.Entity.Movimentacao;
    using Model.Interface.Movimentacao;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Enum;

    public class MovimentacaoMesService : BaseService
    {
        private ICrudMovimentacaoMes _serviceMes;
        public IEnumerable<MovimentacaoMes> meses;
        public MovimentacaoMesService(ILogError l, ICrudMovimentacaoMes p) : base(l)
        {
            _serviceMes = p;
        }

        public AcaoEfetuada Excluir(MovimentacaoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _serviceMes.Remove(objModel.Id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Alterar(MovimentacaoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _serviceMes.Add(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(MovimentacaoMes objModel, int recursoId, short actionId)
        {
            try
            {
                //if (objModel.Id == 0)
                    objModel.Id = _serviceMes.Add(objModel);
                //else
                //    _mes.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(IEnumerable<MovimentacaoMes> objModel, int recursoId, short actionId)
        {
            try
            {
                var salvos = _serviceMes.Fetch(new MovimentacaoMes { IdMovimentacao = objModel.FirstOrDefault().IdMovimentacao, NrAgrupamento = objModel.FirstOrDefault().NrAgrupamento, tab = objModel.FirstOrDefault().tab });

                var deleta = salvos.Where(w => objModel.All(a => a.Id != w.Id));

                foreach (var item in deleta)
                {
                    Excluir(item, recursoId, 3);
                }

                foreach (var item in objModel.Where(w => w.ValorMes > 0M))
                {
                    Salvar(item, recursoId, actionId);
                }

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public IEnumerable<MovimentacaoMes> Buscar(MovimentacaoMes objModel)
        {
            var dbResult = _serviceMes.Fetch(objModel);

            return dbResult;
        }

        public IEnumerable<MovimentacaoMes> BuscarCancelamento(MovimentacaoMes objModel)
        {
            var dbResult = _serviceMes.FetchCancelamento(objModel);

            return dbResult;
        }

        public IEnumerable<MovimentacaoMes> BuscarDistribuicao(MovimentacaoMes objModel)
        {
            var dbResult =  _serviceMes.FetchDistribuicao(objModel);

            return dbResult;
        }

        public IEnumerable<MovimentacaoMes> BuscarReducaoSuplementacao(MovimentacaoMes objModel)
        {
            var dbResult = _serviceMes.FetchReducaoSuplementacao(objModel).ToList();

            return dbResult;
        }
    }
}
