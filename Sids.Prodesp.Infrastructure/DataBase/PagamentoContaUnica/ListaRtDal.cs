using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ListaRtDal : ICrudListaRt
    {
        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaRT> FetchForGrid(ListaRT entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaRT> Fetch(ListaRT entity)
        {
            throw new NotImplementedException();
        }

        public int Save(ListaRT entity)
        {
            var paramNumeroRT = new SqlParameter("@cd_relob_rt", entity.NumeroRT);
            var paramNumeroOB = new SqlParameter("@nr_ob_rt", entity.NumeroOB);
            var paramContaBancariaEmitente = new SqlParameter("@cd_conta_bancaria_emitente", entity.ContaBancariaEmitente);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora_favorecida", entity.UnidadeGestoraFavorecida);
            var paramGestaoFavorecida = new SqlParameter("@cd_gestao_favorecida", entity.GestaoFavorecida);
            var paramMnemonicoUfFavorecida = new SqlParameter("@ds_mnemonico_ug_favorecida", entity.MnemonicoUfFavorecida);
            var paramBancoFavorecido = new SqlParameter("@ds_banco_favorecido", entity.BancoFavorecido);
            var paramAgenciaFavorecida = new SqlParameter("@cd_agencia_favorecido", entity.AgenciaFavorecida);
            var paramContaFavorecida = new SqlParameter("@ds_conta_favorecido", entity.ContaFavorecida);
            var paramValorOB = new SqlParameter("@vl_ob", entity.ValorOB);

            var dbResult = DataHelper.Get<int>("PR_ITENS_OBS_RT_SALVAR", paramNumeroRT, paramNumeroOB, paramContaBancariaEmitente,
                paramUnidadeGestoraFavorecida, paramGestaoFavorecida, paramMnemonicoUfFavorecida, paramBancoFavorecido, paramAgenciaFavorecida, 
                paramContaFavorecida, paramValorOB);

            return dbResult;
        }

        ListaRT ICrudPagamentoContaUnica<ListaRT>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
