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
    public class MovimentacaoTipoDal : ICrudMovimentacaoTipo
    {
        public MovimentacaoTipoDal() { }



        public IEnumerable<MovimentacaoTipo> Fetch(MovimentacaoTipo entity)
        {
            var paramId = new SqlParameter("@id_tipo_movimentacao_orcamentaria", entity.Id);
            var paramDescricao = new SqlParameter("@ds_tipo_movimentacao_orcamentaria", entity.Descricao);

            var dbResult = DataHelper.List<MovimentacaoTipo>("PR_MOVIMENTACAO_TIPO_CONSULTAR", paramId, paramDescricao);

            return dbResult;
        }

        public IEnumerable<MovimentacaoTipo> FetchForGrid(MovimentacaoTipo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovimentacaoTipo> FetchForGrid(MovimentacaoTipo entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public MovimentacaoTipo Get(int id)
        {
            throw new NotImplementedException();
        }

        public MovimentacaoTipo Get(int? id, int? idAutorizacaoOB)
        {
            throw new NotImplementedException();
        }

        public int Remove(MovimentacaoTipo entity)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(MovimentacaoTipo entity)
        {
            throw new NotImplementedException();
        }
    }
}
