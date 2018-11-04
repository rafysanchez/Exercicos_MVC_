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

    public class EmpenhoReforcoMesService : BaseService
    {
        private ICrudEmpenhoReforcoMes _reforcoMes;
        public EmpenhoReforcoMesService(ILogError l, ICrudEmpenhoReforcoMes p) : base(l)
        {
            _reforcoMes = p;
        }

        public AcaoEfetuada Excluir(EmpenhoReforcoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _reforcoMes.Remove(objModel.Codigo);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Alterar(EmpenhoReforcoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _reforcoMes.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(EmpenhoReforcoMes objModel, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                    objModel.Codigo = _reforcoMes.Add(objModel);
                else
                    _reforcoMes.Edit(objModel);

                var arg = string.Format("Reforco Mes {0}, Codigo {1}", objModel.Descricao, objModel.ValorMes);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(IEnumerable<EmpenhoReforcoMes> objModel, int recursoId, short actionId)
        {
            try
            {

                var meses = objModel.Any() ? Buscar(new EmpenhoReforcoMes { Id = objModel.FirstOrDefault().Id }) : new List<EmpenhoReforcoMes>();
                ((List<EmpenhoReforcoMes>)objModel).ForEach(e => e.Codigo = meses.FirstOrDefault(f => f.Descricao.Equals(e.Descricao))?.Codigo ?? 0);

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

        public IEnumerable<EmpenhoReforcoMes> Buscar(EmpenhoReforcoMes objModel)
        {
            return _reforcoMes.Fetch(objModel);
        }
    }
}
