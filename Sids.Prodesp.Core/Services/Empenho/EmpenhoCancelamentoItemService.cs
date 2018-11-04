namespace Sids.Prodesp.Core.Services.Empenho
{
    using Base;
    using Model.Entity.Empenho;
    using Model.Enum;
    using Model.Extension;
    using Model.Interface;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EmpenhoCancelamentoItemService : BaseService, IEmpenhoItemService<EmpenhoCancelamentoItem>
    {
        private ICrudEmpenhoCancelamentoItem _item;
        public EmpenhoCancelamentoItemService(ILogError l, ICrudEmpenhoCancelamentoItem item) : base(l)
        {
            _item = item;
        }

        public AcaoEfetuada Excluir(EmpenhoCancelamentoItem objModel, int recursoId, int actionId)
        {
            try
            {
                _item.Remove(objModel.Id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short)actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Alterar(EmpenhoCancelamentoItem objModel, int recursoId, int actionId)
        {
            try
            {
                _item.Edit(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short)actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(EmpenhoCancelamentoItem objModel, int recursoId, int actionId)
        {
            try
            {
                objModel.QuantidadeMaterialServico = objModel.QuantidadeMaterialServico.ParaInteiro();
                objModel.ValorUnitario = objModel.ValorUnitario.ParaInteiro();
                objModel.ValorTotal = objModel.ValorTotal.ParaInteiro();
                if (!string.IsNullOrWhiteSpace(objModel.CodigoItemServico))
                    objModel.CodigoItemServico = objModel.CodigoItemServico.Replace("-", "");

                if (objModel.Id == 0)
                    objModel.Id = _item.Add(objModel);
                else
                    _item.Edit(objModel);

                objModel.QuantidadeMaterialServico = objModel.QuantidadeMaterialServico.ParaDecimal();
                objModel.ValorUnitario = objModel.ValorUnitario.ParaDecimal();
                objModel.ValorTotal = objModel.ValorTotal.ParaDecimal();

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short)actionId, functionalityId: recursoId);
            }
        }

        public AcaoEfetuada Salvar(int cancelamentoId,IEnumerable<EmpenhoCancelamentoItem> objModel, int recursoId, short actionId)
        {
            try
            {
                if (objModel != null)
                {

                    var itens = Buscar(new EmpenhoCancelamentoItem { EmpenhoId = cancelamentoId }).ToList();

                    ((List<EmpenhoCancelamentoItem>)objModel).ForEach(e => e.Id = itens.FirstOrDefault(f => f.CodigoItemServico == e.CodigoItemServico)?.Id ?? 0);


                    var itensDiferentes = itens.Where(w => !objModel.Any(a => a.CodigoItemServico == (w.CodigoItemServico) || a.ValorTotal <= 0M));

                    foreach (var item in itensDiferentes)
                    {
                        Excluir(item, recursoId, 3);
                    }

                    var idsExcluidos = itensDiferentes.Select(x => x.Id).ToArray();
                    var itensParaSalvar = itensDiferentes.Where(x => !idsExcluidos.Contains(x.Id) && x.ValorTotal > 0M);

                    itensParaSalvar = (itensParaSalvar != null && itensParaSalvar.Any()) ? itensParaSalvar : objModel;

                    var itensCancelamento = new List<EmpenhoCancelamentoItem>();
                    foreach (var item in itensParaSalvar)
                    {
                        item.Id = (int)Salvar(item, recursoId, actionId);
                        itens.Add(item);
                    }

                    return AcaoEfetuada.Sucesso;
                }
                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        public IEnumerable<EmpenhoCancelamentoItem> Buscar(EmpenhoCancelamentoItem objModel)
        {
            var itens = _item.Fetch(objModel).ToList();

            foreach (var obj in itens)
            {
                obj.QuantidadeMaterialServico = obj.QuantidadeMaterialServico.ParaDecimal();
                obj.ValorTotal = obj.ValorTotal.ParaDecimal();
                obj.ValorUnitario = obj.ValorUnitario.ParaDecimal();
            }

            return itens;
        }
    }
}
