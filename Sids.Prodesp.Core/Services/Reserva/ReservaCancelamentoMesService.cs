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

    public class ReservaCancelamentoMesService : BaseService
    {
        private ICrudReservaCancelamentoMes _cancelamentoMes;
        public ReservaCancelamentoMesService(ILogError l, ICrudReservaCancelamentoMes p) : base(l)
        {
            _cancelamentoMes = p;
        }

        public AcaoEfetuada Excluir(ReservaCancelamentoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _cancelamentoMes.Remove(objModel.Codigo);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Alterar(ReservaCancelamentoMes objModel, int recursoId, short actionId)
        {
            try
            {
                _cancelamentoMes.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(ReservaCancelamentoMes objModel, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                    objModel.Codigo = _cancelamentoMes.Add(objModel);
                else
                    _cancelamentoMes.Edit(objModel);

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

        public AcaoEfetuada Salvar(List<ReservaCancelamentoMes> objModel, int recursoId, short actionId)
        {
            try
            {
                var meses = objModel.Any()? Buscar(new ReservaCancelamentoMes { Id = objModel.FirstOrDefault().Id }): new List<ReservaCancelamentoMes>();
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

        public IEnumerable<ReservaCancelamentoMes> Buscar(ReservaCancelamentoMes objModel)
        {
            return _cancelamentoMes.Fetch(objModel);
        }
    }
}
