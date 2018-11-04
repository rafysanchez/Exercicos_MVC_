namespace Sids.Prodesp.Core.Services.Reserva
{
    using Base;
    using Model.Entity.Reserva;
    using Model.Enum;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReservaReforcoMesService : BaseService
    {
        private ICrudReservaReforcoMes _reforcoMes;
        public ReservaReforcoMesService(ILogError l, ICrudReservaReforcoMes p) : base(l)
        {
            _reforcoMes = p;
        }

        public AcaoEfetuada Excluir(ReservaReforcoMes objModel, int recursoId, short actionId)
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
        public AcaoEfetuada Alterar(ReservaReforcoMes objModel, int recursoId, short actionId)
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

        public AcaoEfetuada Salvar(ReservaReforcoMes objModel, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                    objModel.Codigo = _reforcoMes.Add(objModel);
                else
                    _reforcoMes.Edit(objModel);

                var arg = string.Format("Reforco Mes {0}, Codigo {1}", objModel.Descricao, objModel.ValorMes);

                if (recursoId > 0)
                    return LogSucesso(actionId, recursoId, arg);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(List<ReservaReforcoMes> objModel, int recursoId, short actionId)
        {
            try
            {
                var meses = objModel.Any()? Buscar(new ReservaReforcoMes { Id = objModel.FirstOrDefault().Id }): new List<ReservaReforcoMes>();
                objModel.ForEach(e => e.Codigo = meses.FirstOrDefault(f => f.Descricao.Equals(e.Descricao))?.Codigo ?? 0);

                foreach (var mes in meses.Where(w => !objModel.Any(a => a.Descricao.Equals(w.Descricao))))
                    Excluir(mes, recursoId, 3);

                foreach (var mes in objModel)
                    Salvar(mes, recursoId, actionId);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public IEnumerable<ReservaReforcoMes> Buscar(ReservaReforcoMes objModel)
        {
            return _reforcoMes.Fetch(objModel);
        }
    }
}
