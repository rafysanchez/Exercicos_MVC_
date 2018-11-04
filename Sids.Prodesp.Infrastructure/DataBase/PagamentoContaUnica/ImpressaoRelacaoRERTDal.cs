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
    public class ImpressaoRelacaoRERTDal : ICrudImpressaoRelacaoRERT
    {
        public IEnumerable<ImpressaoRelacaoRERT> FetchForGrid(ImpressaoRelacaoRERT entity, DateTime since, DateTime until)
        {
            var paramNumeroRE = new SqlParameter("@cd_relre", entity.CodigoRelacaoRERT?.Substring(4, 2) == "RE" ? entity.CodigoRelacaoRERT : null);
            var paramNumeroRT = new SqlParameter("@cd_relrt", entity.CodigoRelacaoRERT?.Substring(4, 2) == "RT" ? entity.CodigoRelacaoRERT : null);
            var paramNumeroOB = new SqlParameter("@cd_relob", entity.CodigoOB);
            var paramStatusSiafem = new SqlParameter("@ds_status_siafem", entity.StatusSiafem);
            var paramDataCadastramentoDe = new SqlParameter("@dt_cadastramentoDe", since.ValidateDBNull());
            var paramDataCadastramentoAte = new SqlParameter("@dt_cadastramentoAte", until.ValidateDBNull());
            var paramCodigoUnidadeGestora = new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora);
            var paramCodigoGestao = new SqlParameter("@cd_gestao", entity.CodigoGestao);
            var paramCodigoBanco = new SqlParameter("@cd_banco", entity.CodigoBanco);
            var paramDataSolicitacao = new SqlParameter("@dt_solicitacao", entity.DataSolicitacao.ValidateDBNull());
            var paramNumeroAgrupamento = new SqlParameter("@nr_agrupamento", entity.NumeroAgrupamento);
            var paramFlagCancelamento = new SqlParameter("@fg_cancelamento_relacao_re_rt", entity.FlagCancelamentoRERT);

            var dbResult = DataHelper.List<ImpressaoRelacaoRERT>("PR_IMPRESSAO_RELACAO_RE_RT_CONSULTA_GRID", paramNumeroRE, paramNumeroRT, paramNumeroOB, paramStatusSiafem,
                paramDataCadastramentoDe, paramDataCadastramentoAte, paramCodigoUnidadeGestora, paramCodigoGestao, paramCodigoBanco, paramDataSolicitacao,
                paramNumeroAgrupamento, paramFlagCancelamento);

            return dbResult;
        }

        public int Remove(int id)
        {
            var id_impressao_relacao_re_rt = new SqlParameter("@id_impressao_relacao_re_rt", id);
            var relRERT = new SqlParameter("@relRERT", "");

            var dbResult = DataHelper.Get<int>("PR_IMPRESSAO_RELACAO_RE_RT_EXCLUIR", id_impressao_relacao_re_rt, relRERT);

            return dbResult;
        }

        public int Save(ImpressaoRelacaoRERT entity)
        {
            var paramId = new SqlParameter("@id_impressao_relacao_re_rt", entity.Id);
            var paramCodigoRelacaoRERT = new SqlParameter("@cd_relob", entity.CodigoRelacaoRERT);
            var paramCodigoOB = new SqlParameter("@nr_ob", entity.CodigoOB);
            var paramCodigoRelatorio = new SqlParameter("@cd_relatorio", entity.CodigoRelatorio);
            var paramNumeroAgrupamento = new SqlParameter("@nr_agrupamento", entity.NumeroAgrupamento);
            var paramCodigoUnidadeGestora = new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora);
            var paramNomeUnidadeGestora = new SqlParameter("@ds_nome_unidade_gestora", entity.NomeUnidadeGestora);
            var paramCodigoGestao = new SqlParameter("@cd_gestao", entity.CodigoGestao);
            var paramNomeGestao = new SqlParameter("@ds_nome_gestao", entity.NomeGestao);
            var paramCodigoBanco = new SqlParameter("@cd_banco", entity.CodigoBanco);
            var paramNomeBanco = new SqlParameter("@ds_nome_banco", entity.NomeBanco);
            var paramTextoAutorizacao = new SqlParameter("@ds_texto_autorizacao", entity.TextoAutorizacao);
            var paramCidade = new SqlParameter("@ds_cidade", entity.Cidade);
            var paramNomeGestorFinanceiro = new SqlParameter("@ds_nome_gestor_financeiro", entity.NomeGestorFinanceiro);
            var paramNomeOrdenadorAssinatura = new SqlParameter("@ds_nome_ordenador_assinatura", entity.NomeOrdenadorAssinatura);
            var paramDataSolicitacao = new SqlParameter("@dt_solicitacao", entity.DataSolicitacao.ValidateDBNull());
            var paramDataReferencia = new SqlParameter("@dt_referencia", entity.DataReferencia.ValidateDBNull());
            var paramDataCadastramento = new SqlParameter("@dt_cadastramento", entity.DataCadastramento.ValidateDBNull());
            var paramDataEmissao = new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull());
            var paramValorTotalDocumento = new SqlParameter("@vl_total_documento", entity.ValorTotalDocumento);
            var paramValorExtenso = new SqlParameter("@vl_extenso", entity.ValorExtenso);
            var paramFlagTransmitidoSiafem = new SqlParameter("@fg_transmitido_siafem", entity.FlagTransmitidoSiafem);
            var paramFlagTransmitirSiafem = new SqlParameter("@fg_transmitir_siafem", entity.FlagTransmitirSiafem);
            var paramDataTransmissaoSiafem = new SqlParameter("@dt_transmitido_siafem", entity.DataTransmissaoSiafem.ValidateDBNull());
            var paramStatusSiafem = new SqlParameter("@ds_status_siafem", entity.StatusSiafem);
            var paramMsgRetornoTransmissaoSiafem = new SqlParameter("@ds_msgRetornoTransmissaoSiafem", entity.MsgRetornoTransmissaoSiafem);
            var paramFlagCancelamentoRERT = new SqlParameter("@fg_cancelamento_relacao_re_rt", entity.FlagCancelamentoRERT);
            var paramAgencia = new SqlParameter("@nr_agencia", entity.Agencia);
            var paramNomeAgencia = new SqlParameter("@ds_nome_agencia", entity.NomeAgencia);
            var paramNumeroConta = new SqlParameter("@nr_conta_c", entity.NumeroConta);

            var dbResult = DataHelper.Get<int>("PR_IMPRESSAO_RELACAO_RE_RT_SALVAR", paramId, paramCodigoRelacaoRERT, paramCodigoOB, paramCodigoRelatorio, paramNumeroAgrupamento,
                paramCodigoUnidadeGestora, paramNomeUnidadeGestora, paramCodigoGestao, paramNomeGestao, paramCodigoBanco, paramNomeBanco, paramTextoAutorizacao, paramCidade,
                paramNomeGestorFinanceiro, paramNomeOrdenadorAssinatura, paramDataSolicitacao, paramDataReferencia, paramDataCadastramento, paramDataEmissao, paramValorTotalDocumento,
                paramValorExtenso, paramFlagTransmitidoSiafem, paramFlagTransmitirSiafem, paramDataTransmissaoSiafem, paramStatusSiafem, paramMsgRetornoTransmissaoSiafem, paramFlagCancelamentoRERT,
                paramAgencia, paramNomeAgencia, paramNumeroConta);

            return dbResult;
        }

        public ImpressaoRelacaoReRtConsultaVo Get(int id)
        {
            var id_impressao_relacao_re_rt = new SqlParameter("@id_impressao_relacao_re_rt", id);

            var dbResult = DataHelper.Get<ImpressaoRelacaoReRtConsultaVo>("PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID", id_impressao_relacao_re_rt);

            return dbResult;
        }

        public IEnumerable<ImpressaoRelacaoReRtConsultaVo> Fetch(ImpressaoRelacaoReRtConsultaVo entity)
        {
            var id_impressao_relacao_re_rt = new SqlParameter("@id_impressao_relacao_re_rt", entity.Id);
            var relRERT = new SqlParameter("@relRERT", "");

            var dbResult = DataHelper.List<ImpressaoRelacaoReRtConsultaVo>("PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR", id_impressao_relacao_re_rt, relRERT);

            return dbResult;
        }

        public IEnumerable<ImpressaoRelacaoReRtConsultaVo> Fetch(int idAgrupamento)
        {
            var paramNr_agrupamento = new SqlParameter("@nr_agrupamento", idAgrupamento);

            var dbResult = DataHelper.List<ImpressaoRelacaoReRtConsultaVo>("PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_AGRUPAMENTO", paramNr_agrupamento);

            return dbResult;
        }

        public IEnumerable<ImpressaoRelacaoRERT> Fetch(ImpressaoRelacaoRERT entity)
        {
            throw new NotImplementedException();
        }

        ImpressaoRelacaoRERT ICrudPagamentoContaUnica<ImpressaoRelacaoRERT>.Get(int id)
        {
            var id_impressao_relacao_re_rt = new SqlParameter("@id_impressao_relacao_re_rt", id);

            var dbResult = DataHelper.Get<ImpressaoRelacaoReRtConsultaVo>("PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID", id_impressao_relacao_re_rt);

            return dbResult;
        }

        public int GetNumeroAgrupamento()
        {
            var dbResult = DataHelper.Get<int>("PR_IMPRESSAO_RELACAO_RE_RT_AGRUPAMENTO");

            return dbResult;
        }
    }
}
