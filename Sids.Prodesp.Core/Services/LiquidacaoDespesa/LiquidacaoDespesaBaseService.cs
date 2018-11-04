using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.Model.Interface.LiquidacaoDespesa;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    public class LiquidacaoDespesaBaseService : CommonService
    {
        public LiquidacaoDespesaBaseService(ILogError log, ICommon common, IChaveCicsmo chave) : base(log, common, chave)
        {

        }

        protected void CalcularValoresNl<T>(T entity, Usuario user, string ug) where T : ILiquidacaoDespesa
        {
            if (entity.Itens.Count() > 0)
            {
                var nl = this.ConsultaNL(user, ug, string.Empty, entity);

                decimal soma = 0;
                var itens = entity.Itens.ToList();
                foreach (var item in itens)
                {
                    var codigoInt = int.Parse(item.CodigoItemServico.RemoveSpecialChar());
                    var itemDaNl = nl.ItensLiquidados.FirstOrDefault(x => x.Codigo == codigoInt);
                    if (itemDaNl != null)
                    {
                        var valorDecimal = Convert.ToDecimal(itemDaNl.Valor);
                        item.Valor = valorDecimal;
                        soma += valorDecimal;
                    }
                }
                entity.Itens = itens;
                entity.Valor = (int)(soma * 100);
            }
        }

        protected void SomarEventos<T>(T entity) where T : ILiquidacaoDespesa
        {
            var somaEventos = entity.Eventos.Sum(x => x.ValorUnitario);
            entity.Valor = somaEventos > 0 ? somaEventos : entity.Valor;
        }
    }
}
