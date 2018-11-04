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

    public class EmpenhoReforcoItemService : BaseService, IEmpenhoItemService<EmpenhoReforcoItem>
    {
        private ICrudEmpenhoReforcoItem _item;
        public EmpenhoReforcoItemService(ILogError l, ICrudEmpenhoReforcoItem item) : base(l)
        {
            _item = item;
        }

        public AcaoEfetuada Excluir(EmpenhoReforcoItem objModel, int recursoId, int actionId)
        {
            try
            {
                _item.Remove(objModel.Id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)actionId, recursoId);
            }
        }

        public AcaoEfetuada Alterar(EmpenhoReforcoItem objModel, int recursoId, int actionId)
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

        public AcaoEfetuada Salvar(EmpenhoReforcoItem objModel, int recursoId, int actionId)
        {
            try
            {
                objModel.QuantidadeMaterialServico = objModel.QuantidadeMaterialServico.ParaInteiro();
                objModel.ValorUnitario = objModel.ValorUnitario.ParaInteiro();
                objModel.ValorTotal = objModel.ValorTotal.ParaInteiro();

                if(!string.IsNullOrWhiteSpace(objModel.CodigoItemServico))
                objModel.CodigoItemServico =  objModel.CodigoItemServico.Replace("-", "");

                if (objModel.Id == 0)
                    objModel.Id = _item.Add(objModel);
                else
                    _item.Edit(objModel);

                var arg = string.Format("EmpenhoReforco item {0}, Codigo {1}", objModel.Id, objModel.EmpenhoId);
                
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

        public AcaoEfetuada Salvar(int reforcoId, IEnumerable<EmpenhoReforcoItem> objModel, int recursoId, short acaoId)
        {
            try
            {
                if (objModel != null)
                {

                    var itens = Buscar(new EmpenhoReforcoItem { EmpenhoId = reforcoId });

                    ((List<EmpenhoReforcoItem>)objModel).ForEach(e => e.Id = itens.FirstOrDefault(f => f.CodigoItemServico == e.CodigoItemServico)?.Id ?? 0);

                    var itensDiferentes = itens.Where(w => !objModel.Any(a => a.CodigoItemServico == (w.CodigoItemServico) || a.ValorTotal <= 0M));

                    foreach (var item in itensDiferentes)
                    {
                        Excluir(item, recursoId, 3);
                    }

                    var idsExcluidos = itensDiferentes.Select(x => x.Id).ToArray();
                    var itensParaSalvar = itensDiferentes.Where(x => !idsExcluidos.Contains(x.Id) && x.ValorTotal > 0M);

                    itensParaSalvar = (itensParaSalvar != null && itensParaSalvar.Any()) ? itensParaSalvar : objModel;

                    foreach (var item in itensParaSalvar)
                        Salvar(item, recursoId, acaoId);

                    return AcaoEfetuada.Sucesso;
                }
                else
                {
                    return AcaoEfetuada.Sucesso;
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }

        public IEnumerable<EmpenhoReforcoItem> Buscar(EmpenhoReforcoItem objModel)
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
