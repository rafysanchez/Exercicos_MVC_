using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Sids.Prodesp.Model.Interface.PagamentoContaDer;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Interface.Movimentacao.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class MovimentacaoDocumentoTipoDal : ICrudMovimentacaoDocumentoTipo
    {
        public MovimentacaoDocumentoTipoDal() { }


        public IEnumerable<MovimentacaoDocumentoTipo> Fetch(MovimentacaoDocumentoTipo entity)
        {
            return DataHelper.List<MovimentacaoDocumentoTipo>("PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR",
             new SqlParameter("@ds_tipo_documento_movimentacao", entity.Descricao),
             new SqlParameter("@id_tipo_documento_movimentacao", entity.Id)
          );
        }

        public IEnumerable<MovimentacaoDocumentoTipo> FetchForGrid(MovimentacaoDocumentoTipo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovimentacaoDocumentoTipo> FetchForGrid(MovimentacaoDocumentoTipo entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovimentacaoDocumentoTipo> FetchGrid(MovimentacaoDocumentoTipo entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public MovimentacaoDocumentoTipo Get(int id)
        {
            throw new NotImplementedException();
        }

        public MovimentacaoDocumentoTipo Get(int? id, int? idAutorizacaoOB)
        {
            throw new NotImplementedException();
        }

        public int Remove(MovimentacaoDocumentoTipo entity)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(MovimentacaoDocumentoTipo entity)
        {
            throw new NotImplementedException();
        }
    }
}
