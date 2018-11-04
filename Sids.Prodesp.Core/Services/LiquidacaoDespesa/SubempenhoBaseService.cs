using Sids.Prodesp.Model.Base.LiquidacaoDespesa;
using Sids.Prodesp.Model.Entity.LiquidacaoDespesa;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
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
    public class SubempenhoBaseService : LiquidacaoDespesaBaseService
    {
        public SubempenhoBaseService(ILogError log, ICommon common, IChaveCicsmo chave) : base(log, common, chave)
        {

        }
        
        //Somente os Items marcados para trasmitir.
        protected IEnumerable<LiquidacaoDespesaItem> VerificarItensParaTransmitir(ILiquidacaoDespesa entity)
        {
            return entity.Itens.Where(o => o.Transmitir == true).AsEnumerable();
        }
        //Enviar somente os items com valor <> 0. Quando o cenário for NL tradicional ou NLPREGAO (1,2) 
        protected void EliminarItensZerados(ILiquidacaoDespesa entity)
        {
            var cenarios = new List<int>() { 1, 2, 9, 10 };
            if (cenarios.Contains(entity.CenarioSiafemSiafisico))
            {
                entity.Itens = entity.Itens.Where(o => o.QuantidadeMaterialServico > 0).AsEnumerable();
            }
        }
    }
}
