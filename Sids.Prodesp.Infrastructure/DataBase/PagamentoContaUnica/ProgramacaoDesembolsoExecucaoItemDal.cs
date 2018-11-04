using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Infrastructure.Helpers;
using System.Data.SqlClient;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoExecucaoItemDal : ICrudProgramacaoDesembolsoExecucaoItem
    {
        public IEnumerable<PDExecucaoItem> Fetch(PDExecucaoItem entity)
        {
            var retorno = DataHelper.List<PDExecucaoItem>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR", new SqlParameter("@id_execucao_pd", entity.id_execucao_pd));

            return retorno;
        }

        public IEnumerable<PDExecucaoItem> FetchForGrid(PDExecucaoItem entity, DateTime since, DateTime until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PDExecucaoItem> FetchForGrid(PDExecucaoItem entity, int? tipoExecucao, DateTime? since, DateTime? until)
        {

            var paramTipo = new SqlParameter("@tipo", 1);
            var paramNumPd = new SqlParameter("@ds_numpd", entity.NumPD);
            var paramNumeroDocumentoGerador = new SqlParameter("@filtro_nr_documento_gerador", entity.NumeroDocumentoGerador);
            var paramNumOb = new SqlParameter("@ds_numob", entity.NumOBItem);
            var paramObCancelada = new SqlParameter("@ob_cancelada", entity.OBCancelada);
            var paramFavorecidoDesc = new SqlParameter("@favorecidoDesc", entity.FavorecidoDesc);
            var paramTipoExecucao = new SqlParameter("@tipoExecucao", tipoExecucao);
            var paramCodigoStatusSiafem = new SqlParameter("@cd_transmissao_status_siafem", entity.cd_transmissao_status_siafem);
            var paramCodigoStatusProdesp = new SqlParameter("@cd_transmissao_status_prodesp", entity.cd_transmissao_status_prodesp);
            var paramDe = new SqlParameter("@de", since);
            var paramAte = new SqlParameter("@ate", until);
            var paramGestao = new SqlParameter("@gestao", entity.Gestao);
            var paramUg = new SqlParameter("@ug", entity.UG);
            var paramValor = new SqlParameter("@valor", entity.Valor);
            var paramNumeroContrato = new SqlParameter("@filtro_nr_contrato", entity.NumeroContrato);
            var paramCodigoAplicacaoObra = new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra);

            var itens = DataHelper.List<PDExecucaoItem>("[dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_GRID]", paramTipo, paramNumPd, paramNumeroDocumentoGerador, paramNumOb, paramObCancelada, paramFavorecidoDesc, paramTipoExecucao,
                paramCodigoStatusSiafem, paramCodigoStatusProdesp, paramDe, paramAte, paramGestao, paramUg, paramValor, paramNumeroContrato, paramCodigoAplicacaoObra);

            return itens;
        }

        public PDExecucaoItem Get(int id)
        {
            return DataHelper.Get<PDExecucaoItem>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR",
            new SqlParameter("@id_programacao_desembolso_execucao_item", id));
        }

        public PDExecucaoItem Get(string dsNumPD )
        {
            return DataHelper.Get<PDExecucaoItem>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_CONSULTAR",
            new SqlParameter("@ds_numpd", dsNumPD));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR",
            new SqlParameter("@id_programacao_desembolso_execucao_item", id));
        }

        public int DeletarNaoAgrupados(int idExecucaoPD)
        {
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_EXCLUIR_NAOAGRUPADOS",
            new SqlParameter("@id_execucao_pd", idExecucaoPD));
        }

        public int Save(PDExecucaoItem entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity, new string[] { "@nr_cnpj_cpf_credor" });
            return DataHelper.Get<int>("PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_ITEM_SALVAR", sqlParameterList);
        }
    }
}
