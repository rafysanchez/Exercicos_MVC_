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
    public class ListaReDal : ICrudListaRe
    {
        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaRE> FetchForGrid(ListaRE entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaRE> Fetch(ListaRE entity)
        {
            throw new NotImplementedException();
        }

        public int Save(ListaRE entity)
        {
            var paramNumeroRE = new SqlParameter("@cd_relob_re", entity.NumeroRE);
            var paramNumeroOB = new SqlParameter("@nr_ob_re", entity.NumeroOB);
            var paramFlagPrioridade = new SqlParameter("@fg_prioridade", entity.FlagPrioridade);
            var paramTipoOB = new SqlParameter("@cd_tipo_ob", entity.TipoOB);
            var paramNomeFavorecido = new SqlParameter("@ds_nome_favorecido", entity.NomeFavorecido);
            var paramBancoFavorecido = new SqlParameter("@ds_banco_favorecido", entity.BancoFavorecido);
            var paramAgenciaFavorecida = new SqlParameter("@cd_agencia_favorecido", entity.AgenciaFavorecida);
            var paramContaFavorecida = new SqlParameter("@ds_conta_favorecido", entity.ContaFavorecida);
            var paramValorOB = new SqlParameter("@vl_ob", entity.ValorOB);
           
            var dbResult = DataHelper.Get<int>("PR_ITENS_OBS_RE_SALVAR", paramNumeroRE, paramNumeroOB, paramFlagPrioridade,
                paramTipoOB, paramNomeFavorecido, paramBancoFavorecido, paramAgenciaFavorecida, paramContaFavorecida, paramValorOB);

            return dbResult;
        }

        ListaRE ICrudPagamentoContaUnica<ListaRE>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
