using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class DocumentoTipoDal: ICrudDocumentoTipo
    {
        public IEnumerable<DocumentoTipo> Fatch(DocumentoTipo entity)
        {

            var sqlParameterList = DataHelper.GetSqlParameterList(entity);
            return DataHelper.List<DocumentoTipo>("PR_TIPO_DOCUMENTO_CONSULTAR", sqlParameterList);
        }
    }
}
