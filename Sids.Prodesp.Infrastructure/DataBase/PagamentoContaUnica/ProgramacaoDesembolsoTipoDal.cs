using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoTipoDal: ICrudProgramacaoDesembolsoTipo
    {
        public IEnumerable<ProgramacaoDesembolsoTipo> Fatch(ProgramacaoDesembolsoTipo entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.List<ProgramacaoDesembolsoTipo>("PR_TIPO_PROGRAMACAO_DESEMBOLSO_CONSULTAR", sqlParameterList);
        }
    }
}
