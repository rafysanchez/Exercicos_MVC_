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

    public class ReservaMesService : BaseService
    {
        private ICrudReservaMes _reservaMes;
        public ReservaMesService(ILogError l, ICrudReservaMes p) : base(l)
        {
            _reservaMes = p;
        }

        public AcaoEfetuada Excluir(ReservaMes objModel, int recursoId, short actionId)
        {
            try
            {
                _reservaMes.Remove(objModel.Codigo);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Alterar(ReservaMes objModel, int recursoId, short actionId)
        {
            try
            {
                _reservaMes.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(ReservaMes objModel, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                    objModel.Codigo = _reservaMes.Add(objModel);
                else
                    _reservaMes.Edit(objModel);

                var arg = string.Format("Reserva Mes {0}, Codigo {1}", objModel.Descricao, objModel.ValorMes);

                if (recursoId > 0)
                    return LogSucesso(actionId, recursoId, arg);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(List<ReservaMes> objModel, int recursoId, short actionId)
        {
            try
            {

                var meses = objModel.Any() ? Buscar(new ReservaMes { Id = objModel.FirstOrDefault().Id }) : new List<ReservaMes>();
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

        public IEnumerable<ReservaMes> Buscar(ReservaMes objModel)
        {
            return _reservaMes.Fetch(objModel);
        }
    }
}
