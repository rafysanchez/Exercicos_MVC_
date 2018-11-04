using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class PreparacaoPagamentoDal: ICrudPreparacaoPagamento
    {
        public IEnumerable<PreparacaoPagamento> FetchForGrid(PreparacaoPagamento entity, DateTime since, DateTime until)
        {

            return DataHelper.List<PreparacaoPagamento>("PR_PREPARACAO_PAGAMENTO_CONSULTA_GRID",
                new SqlParameter("@id_preparacao_pagamento", entity.Id),
                new SqlParameter("@nr_op_inicial", entity.NumeroOpInicial),
                new SqlParameter("@id_tipo_preparacao_pagamento", entity.PreparacaoPagamentoTipoId),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull()),
                new SqlParameter("@id_regional", entity.RegionalId));
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PREPARACAO_PAGAMENTO_EXCLUIR",
                new SqlParameter("@id_preparacao_pagamento", id));
        }

        public IEnumerable<PreparacaoPagamento> Fetch(PreparacaoPagamento entity)
        {
            return DataHelper.List<PreparacaoPagamento>("PR_PREPARACAO_PAGAMENTO_CONSULTAR",
                new SqlParameter("@id_preparacao_pagamento", entity.Id),
                new SqlParameter("@id_regional", entity.RegionalId));
        }

        public int Save(PreparacaoPagamento entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.Get<int>("PR_PREPARACAO_PAGAMENTO_SALVAR", sqlParameterList);
        }

        public PreparacaoPagamento Get(int id)
        {
            return DataHelper.Get<PreparacaoPagamento>("PR_PREPARACAO_PAGAMENTO_CONSULTAR",
                new SqlParameter("@id_preparacao_pagamento", id));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }
    }
}
