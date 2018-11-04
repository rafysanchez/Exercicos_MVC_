using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Sids.Prodesp.Model.Interface.PagamentoContaDer;


namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class ArquivoRemessaDal : ICrudArquivoRemessa
    {
        public ArquivoRemessaDal() { }

        public IEnumerable<ArquivoRemessa> Fetch(ArquivoRemessa entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArquivoRemessa> FetchForGrid(ArquivoRemessa entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArquivoRemessa> FetchForGrid(ArquivoRemessa entity, DateTime since, DateTime until)
        {
                return DataHelper.List<ArquivoRemessa>("PR_ARQUIVO_REMESSA_CONSULTA_GRID",
                new SqlParameter("@id_arquivo_remessa", entity.Id),
                new SqlParameter("@nr_geracao_arquivo", entity.NumeroGeracao),
                new SqlParameter("@nr_codigo_conta", entity.CodigoConta),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull()),
                new SqlParameter("@fg_trasmitido_prodesp", entity.StatusProdesp),
                new SqlParameter("@fg_arquivo_cancelado", entity.Cancelado));


        }

        public ArquivoRemessa Get(int id)
        {
            return DataHelper.Get<ArquivoRemessa>("PR_ARQUIVO_REMESSA_CONSULTAR",
                 new SqlParameter("@id_arquivo_remessa", id));
        }

        public ArquivoRemessa Get(int? id, int? idAutorizacaoOB)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_ARQUIVO_REMESSA_EXCLUIR",
                new SqlParameter("@id_arquivo_remessa", id));
        }

        public int Save(ArquivoRemessa entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.Get<int>("PR_ARQUIVO_REMESSA_SALVAR", sqlParameterList);
        }


    }
}
