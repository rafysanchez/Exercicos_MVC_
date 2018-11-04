namespace Sids.Prodesp.Core.Services.Empenho
{
    using Base;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Enum;

    public class EmpenhoMesService : BaseService
    {
        private ICrudEmpenhoMes _mes;
        public EmpenhoMesService(ILogError l, ICrudEmpenhoMes p) : base(l)
        {
            _mes = p;
        }

        public AcaoEfetuada Excluir(EmpenhoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _mes.Remove(objModel.Codigo);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Alterar(EmpenhoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _mes.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(EmpenhoMes objModel, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                    objModel.Codigo = _mes.Add(objModel);
                else
                    _mes.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(IEnumerable<EmpenhoMes> objModel, int recursoId, short actionId)
        {
            try
            {

                var meses = objModel.Any() ? Buscar(new EmpenhoMes { Id = objModel.FirstOrDefault().Id }) : new List<EmpenhoMes>();
                ((List<EmpenhoMes>)objModel).ForEach(e => e.Codigo = meses.FirstOrDefault(f => f.Descricao.Equals(e.Descricao))?.Codigo ?? 0);

                foreach (var mes in meses.Where(w => !objModel.Select(a => a.Descricao).Contains(w.Descricao) || (w.ValorMes == 0M)).ToList())
                    Excluir(mes, recursoId, 3);
                
                foreach (var mes in objModel.Where(w => w.ValorMes > 0M))
                    Salvar(mes, recursoId, actionId);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public IEnumerable<EmpenhoMes> Buscar(EmpenhoMes objModel)
        {
            return _mes.Fetch(objModel);
        }
    }
}
