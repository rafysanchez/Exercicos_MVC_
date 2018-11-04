using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Sids.Prodesp.Model.Interface.PagamentoContaDer;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Interface.Movimentacao.Base;
using System.Globalization;
using System.Data;
using Sids.Prodesp.Model.ValueObject;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoDer
{
    public class MovimentacaoDal : ICrudMovimentacao
    {
        public int GetLastGroup()
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_ORCAMENTARIA_AGRUPAMENTO_CONSULTAR_NUMERO");
        }
        
        public IEnumerable<MovimentacaoOrcamentaria> Fetch(MovimentacaoOrcamentaria entity)
        {

            return DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR",
                new SqlParameter("@id_movimentacao_orcamentaria", entity.IdMovimentacao),
                new SqlParameter("@nr_agrupamento_movimentacao", entity.NrAgrupamento),
                new SqlParameter("@nr_siafem", entity.NumSiafem),
                new SqlParameter("@tb_regional_id_regional", entity.IdRegional),
                new SqlParameter("@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao", entity.IdTipoDocumento),
                new SqlParameter("@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria", entity.IdTipoMovimentacao),
                new SqlParameter("@cd_unidade_gestora_emitente", entity.UnidadeGestoraEmitente),
                new SqlParameter("@cd_gestao_emitente", entity.GestaoEmitente),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem),
                new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp),
                new SqlParameter("@dt_cadastro", entity.DataCadastro)
                );
        }

        public IEnumerable<MovimentacaoOrcamentaria> FetchForGrid(MovimentacaoOrcamentaria entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovimentacaoOrcamentaria> FetchForGrid(MovimentacaoOrcamentaria entity, DateTime since, DateTime until)
        {
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento_movimentacao", entity.NrAgrupamento);
            var paramNumSiafem = new SqlParameter("@nr_siafem", entity.NumSiafem);
            var paramIdTipoDocumento = new SqlParameter("@tipo_documento", entity.IdTipoDocumento);
            var paramIdMovimentacao = new SqlParameter("@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria", entity.IdTipoMovimentacao);
            var paramUnidadeGestoraEmitente = new SqlParameter("@cd_unidade_gestora_emitente", entity.UnidadeGestoraEmitente);
            var paramGestaoEmitente = new SqlParameter("@cd_gestao_emitente", entity.GestaoEmitente);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora_favorecido", entity.UnidadeGestoraFavorecida);
            var paramIdGestaoFavorecida = new SqlParameter("@cd_gestao_favorecido", entity.IdGestaoFavorecida);
            var paramIdPrograma = new SqlParameter("@cd_programa", entity.IdCFP);
            var paramIdEstrutura = new SqlParameter("@cd_natureza", entity.IdCED);
            var paramDesde = new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull());
            var paramAte = new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull());
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);

            var dbResult = DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID", paramNrAgrupamento, paramNumSiafem, paramIdTipoDocumento, paramIdMovimentacao,
                paramUnidadeGestoraEmitente, paramGestaoEmitente, paramUnidadeGestoraFavorecida, paramIdGestaoFavorecida, paramIdPrograma, paramIdEstrutura, paramDesde, paramAte, paramStatusSiafem, paramStatusProdesp);

            return dbResult;
        }

        public int Save(MovimentacaoOrcamentaria entity)
        {
            var paramId = new SqlParameter("@id_movimentacao_orcamentaria", entity.Id);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento_movimentacao", entity.NrAgrupamento);
            var paramIdRegional = new SqlParameter("@tb_regional_id_regional", entity.IdRegional);
            var paramIdTipoMovimentacao = new SqlParameter("@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria", entity.IdTipoMovimentacao);
            var paramIdTipoDocumento = new SqlParameter("@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao", entity.IdTipoDocumento);
            var paramUnidadeGestoraEmitente = new SqlParameter("@cd_unidade_gestora_emitente", entity.UnidadeGestoraEmitente);
            var paramGestaoEmitente = new SqlParameter("@cd_gestao_emitente", entity.GestaoEmitente);
            var paramAnoExercicio = new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);
            var paramTransmitidoSiafem = new SqlParameter("@bl_transmitido_siafem", entity.TransmitidoSiafem);
            var paramTransmitirSiafem = new SqlParameter("@bl_transmitir_siafem", entity.TransmitirSiafem);
            var paramDataSiafem = new SqlParameter("@dt_trasmitido_siafem", entity.DataSiafem.ValidateDBNull());
            var paramNumSiafem = new SqlParameter("@nr_siafem", entity.NumSiafem);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramTransmitidoProdesp = new SqlParameter("@bl_transmitido_prodesp", entity.TransmitidoProdesp);
            var paramTransmitirProdesp = new SqlParameter("@bl_transmitir_prodesp", entity.TransmitirProdesp);
            var paramDataProdesp = new SqlParameter("@dt_trasmitido_prodesp", entity.DataProdesp.ValidateDBNull());
            var paramMensagemProdesp = new SqlParameter("@ds_msgRetornoProdesp", entity.MensagemProdesp);
            var paramMensagemSiafem = new SqlParameter("@ds_msgRetornoSiafem", entity.MensagemSiafem);
            var paramCadastroCompleto = new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto);
            var paramDataCadastro = new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull());
            var paramIdPrograma = new SqlParameter("@tb_programa_id_programa", entity.IdPrograma);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramIdEstrutura = new SqlParameter("@tb_estrutura_id_estrutura", entity.IdEstrutura);


            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR", paramId, paramNrAgrupamento, paramIdRegional, paramIdTipoMovimentacao, paramIdTipoDocumento,
                paramUnidadeGestoraEmitente, paramGestaoEmitente, paramAnoExercicio, paramStatusSiafem, paramTransmitidoSiafem, paramTransmitirSiafem, paramDataSiafem, paramNumSiafem,
                paramStatusProdesp, paramTransmitidoProdesp, paramTransmitirProdesp, paramDataProdesp, paramMensagemProdesp, paramMensagemSiafem, paramCadastroCompleto, paramDataCadastro,
                paramIdPrograma, paramIdFonte, paramIdEstrutura);

            return dbResult;
        }

        public MovimentacaoOrcamentaria Get(int id)
        {
            var paramId = new SqlParameter("@id_movimentacao_orcamentaria", id);
            var dbResult = DataHelper.Get<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR", paramId);

            return dbResult;
        }

        public IEnumerable<MovimentacaoCancelamento> Fetch(MovimentacaoCancelamento entity)
        {
            var paramId = new SqlParameter("@id_cancelamento_movimentacao", entity.Id);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramNrSiafem = new SqlParameter("@nr_siafem", entity.NumeroSiafem);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora", entity.UnidadeGestoraFavorecida);
            var paramGestaoFavorecida = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecida);
            var paramCategoriaGasto = new SqlParameter("@nr_categoria_gasto", entity.CategoriaGasto);
            var paramObservacao = new SqlParameter("@ds_observacao", entity.Observacao);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);

            var dbResult = DataHelper.List<MovimentacaoCancelamento>("PR_MOVIMENTACAO_CANCELAMENTO_CONSULTAR", paramId, paramIdFonte, paramIdMovimentacao,
                paramNrAgrupamento, paramNrSequencia, paramNrSiafem, paramUnidadeGestoraFavorecida, paramGestaoFavorecida, paramCategoriaGasto, paramObservacao,
                paramStatusProdesp, paramStatusSiafem);

            return dbResult;
        }

        public IEnumerable<MovimentacaoReducaoSuplementacao> Fetch(MovimentacaoReducaoSuplementacao entity)
        {
            var paramId = new SqlParameter("@id_reducao_suplementacao", entity.Id);
            var paramIdNotaCredito = new SqlParameter("@tb_credito_movimentacao_id_nota_credito", entity.IdNotaCredito);
            var paramIdDistribuicao = new SqlParameter("@tb_distribuicao_movimentacao_id_distribuicao_movimentacao", entity.IdDistribuicao);
            var paramIdCancelamento = new SqlParameter("@tb_cancelamento_movimentacao_id_cancelamento_movimentacao", entity.IdCancelamento);
            var paramIdPrograma = new SqlParameter("@tb_programa_id_programa", entity.IdPrograma);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramNrSuplementacaoReducao = new SqlParameter("@nr_suplementacao_reducao", entity.NrSuplementacaoReducao);
            var paramFlProc = new SqlParameter("@fl_proc", entity.FlProc);
            var paramNrProcesso = new SqlParameter("@nr_processo", entity.NrProcesso);
            var paramNrOrgao = new SqlParameter("@nr_orgao", entity.NrOrgao);
            var paramNrObra = new SqlParameter("@nr_obra", entity.NrObra);
            var paramRedSup = new SqlParameter("@flag_red_sup", entity.RedSup);
            var paramNrCnpjCpf = new SqlParameter("@nr_cnpj_cpf_ug_credor", entity.NrCnpjCpf);
            var paramAutorizadoSupraFolha = new SqlParameter("@ds_autorizado_supra_folha", entity.AutorizadoSupraFolha);
            var paramOrigemRecurso = new SqlParameter("@cd_origem_recurso", entity.OrigemRecurso);
            var paramDestinoRecurso = new SqlParameter("@cd_destino_recurso", entity.DestinoRecurso);
            var paramEspecDespesa = new SqlParameter("@cd_especificacao_despesa", entity.EspecDespesa);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);

            var dbResult = DataHelper.List<MovimentacaoReducaoSuplementacao>("PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_CONSULTAR", paramId, paramIdNotaCredito, paramIdDistribuicao,
                paramIdCancelamento, paramIdPrograma, paramIdMovimentacao, paramNrAgrupamento, paramNrSequencia, paramNrSuplementacaoReducao, paramFlProc, paramNrProcesso,
                paramNrOrgao, paramNrObra, paramRedSup, paramNrCnpjCpf, paramAutorizadoSupraFolha, paramOrigemRecurso, paramDestinoRecurso, paramEspecDespesa,
                paramStatusProdesp, paramStatusSiafem);

            return dbResult;
        }

        public IEnumerable<MovimentacaoDistribuicao> Fetch(MovimentacaoDistribuicao entity)
        {
            var paramId = new SqlParameter("@id_distribuicao_movimentacao", entity.Id);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramNrNotaDeCredito = new SqlParameter("@nr_siafem", entity.NumeroSiafem);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora_favorecido", entity.UnidadeGestoraFavorecida);
            var paramGestaoFavorecida = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecida);
            var paramCategoriaGasto = new SqlParameter("@nr_categoria_gasto", entity.CategoriaGasto);

            var dbResult = DataHelper.List<MovimentacaoDistribuicao>("PR_MOVIMENTACAO_DISTRIBUICAO_CONSULTAR", paramId, paramIdMovimentacao, paramNrAgrupamento, paramNrSequencia, paramIdFonte,
                paramNrNotaDeCredito, paramUnidadeGestoraFavorecida, paramGestaoFavorecida, paramCategoriaGasto);

            return dbResult;
        }

        public IEnumerable<MovimentacaoNotaDeCredito> Fetch(MovimentacaoNotaDeCredito entity)
        {
            var paramId = new SqlParameter("@id_nota_credito", entity.Id);
            var paramIdPrograma = new SqlParameter("@tb_programa_id_programa", entity.IdPrograma);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramIdEstrutura = new SqlParameter("@tb_estrutura_id_estrutura", entity.IdEstrutura);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramCanDis = new SqlParameter("@cd_candis", entity.CanDis);
            var paramNrSiafem = new SqlParameter("@nr_siafem", entity.NumeroSiafem);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora_favorecido", entity.UnidadeGestoraFavorecida);
            var paramGestaoFavorecida = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecida);
            var paramUo = new SqlParameter("@cd_uo", entity.Uo);
            var paramPlanoInterno = new SqlParameter("@plano_interno", entity.PlanoInterno);

            var dbResult = DataHelper.List<MovimentacaoNotaDeCredito>("PR_MOVIMENTACAO_CREDITO_CONSULTAR", paramId, paramIdPrograma, paramIdFonte, paramIdEstrutura, paramIdMovimentacao,
                paramNrAgrupamento, paramNrSequencia, paramCanDis, paramNrSiafem, paramUnidadeGestoraFavorecida, paramGestaoFavorecida, paramUo, paramPlanoInterno);

            return dbResult;
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_ORCAMENTARIA_GERAL_EXCLUIR", new SqlParameter("@id", id));
        }

        public int Remove(MovimentacaoOrcamentaria entity)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_ORCAMENTARIA_EXCLUIR", new SqlParameter("@id_movimentacao_orcamentaria", entity.IdMovimentacao));
        }

        public int Remove(MovimentacaoCancelamento entity)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_CANCELAMENTO_EXCLUIR", new SqlParameter("@id_cancelamento_movimentacao", entity.IdMovimentacao));
        }

        public int Remove(MovimentacaoNotaDeCredito entity)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_CREDITO_EXCLUIR", new SqlParameter("@id_nota_credito", entity.IdMovimentacao));
        }

        public int Remove(MovimentacaoDistribuicao entity)
        {
            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_DISTRIBUICAO_EXCLUIR", new SqlParameter("@id_distribuicao_movimentacao", entity.IdMovimentacao));

            return dbResult;
        }

        public int Remove(MovimentacaoReducaoSuplementacao entity)
        {
            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_EXCLUIR", new SqlParameter("@id_reducao_suplementacao", entity.Id));

            return dbResult;
        }

        public int Save(MovimentacaoCancelamento entity)
        {
            var paramId = new SqlParameter("@id_cancelamento_movimentacao", entity.Id);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramNrNotaDeCredito = new SqlParameter("@nr_siafem", entity.NumeroSiafem);
            var paramUnidadeGestoraEmitente = new SqlParameter("@cd_unidade_gestora", entity.UnidadeGestoraFavorecida);
            var paramGestaoEmitente = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecida);
            var paramEvento = new SqlParameter("@evento", entity.Evento);
            var paramCategoriaGasto = new SqlParameter("@nr_categoria_gasto", entity.CategoriaGasto);
            var paramEventoNC = new SqlParameter("@eventoNC", entity.EventoNC);
            var paramObservacao = new SqlParameter("@ds_observacao", entity.Observacao);
            var paramObservacao2 = new SqlParameter("@ds_observacao2", entity.Observacao2);
            var paramObservacao3 = new SqlParameter("@ds_observacao3", entity.Observacao3);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramMensagemProdesp = new SqlParameter("@ds_msgRetornoProdesp", entity.MensagemProdesp);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);
            var paramMensagemSiafem = new SqlParameter("@ds_msgRetornoSiafem", entity.MensagemSiafem);
            var paramValor = new SqlParameter("@valor", entity.Valor);

            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_CANCELAMENTO_SALVAR", paramId, paramIdFonte, paramIdMovimentacao, paramNrAgrupamento
                , paramNrSequencia, paramNrNotaDeCredito, paramUnidadeGestoraEmitente, paramGestaoEmitente, paramEvento, paramCategoriaGasto, paramEventoNC
                , paramObservacao, paramObservacao2, paramObservacao3, paramStatusProdesp, paramMensagemProdesp, paramStatusSiafem, paramMensagemSiafem, paramValor);

            return dbResult;
        }

        public int Save(MovimentacaoReducaoSuplementacao entity)
        {
            var paramId = new SqlParameter("@id_reducao_suplementacao", entity.Id);
            var paramIdNotaCredito = new SqlParameter("@tb_credito_movimentacao_id_nota_credito", entity.IdNotaCredito);
            var paramIdDistribuicao = new SqlParameter("@tb_distribuicao_movimentacao_id_distribuicao_movimentacao", entity.IdDistribuicao);
            var paramIdCancelamento = new SqlParameter("@tb_cancelamento_movimentacao_id_cancelamento_movimentacao", entity.IdCancelamento);
            var paramIdPrograma = new SqlParameter("@tb_programa_id_programa", entity.IdPrograma);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramNrSuplementacaoReducao = new SqlParameter("@nr_suplementacao_reducao", entity.NrSuplementacaoReducao);
            var paramFlProc = new SqlParameter("@fl_proc", entity.FlProc);
            var paramNrProcesso = new SqlParameter("@nr_processo", entity.NrProcesso);
            var paramNrOrgao = new SqlParameter("@nr_orgao", entity.NrOrgao);
            var paramNrObra = new SqlParameter("@nr_obra", entity.NrObra);
            var paramRedSup = new SqlParameter("@flag_red_sup", entity.RedSup);
            var paramNrCnpjCpf = new SqlParameter("@nr_cnpj_cpf_ug_credor", entity.NrCnpjCpf);
            var paramAutorizadoSupraFolha = new SqlParameter("@ds_autorizado_supra_folha", entity.AutorizadoSupraFolha);
            var paramOrigemRecurso = new SqlParameter("@cd_origem_recurso", entity.OrigemRecurso);
            var paramDestinoRecurso = new SqlParameter("@cd_destino_recurso", entity.DestinoRecurso);
            var paramEspecDespesa = new SqlParameter("@cd_especificacao_despesa", entity.EspecDespesa);
            var paramDescEspecDespesa = new SqlParameter("@ds_especificacao_despesa", entity.DescEspecDespesa);
            var paramCodigoAutorizadoAssinatura = new SqlParameter("@cd_autorizado_assinatura", entity.CodigoAutorizadoAssinatura);
            var paramCodigoAutorizadoGrupo = new SqlParameter("@cd_autorizado_grupo", entity.CodigoAutorizadoGrupo);
            var paramCodigoAutorizadoOrgao = new SqlParameter("@cd_autorizado_orgao", entity.CodigoAutorizadoOrgao);
            var paramDescricaoAutorizadoCargo = new SqlParameter("@ds_autorizado_cargo", entity.DescricaoAutorizadoCargo);
            var paramNomeAutorizadoAssinatura = new SqlParameter("@nm_autorizado_assinatura", entity.NomeAutorizadoAssinatura);
            var paramCodigoExaminadoAssinatura = new SqlParameter("@cd_examinado_assinatura", entity.CodigoExaminadoAssinatura);
            var paramCodigoExaminadoGrupo = new SqlParameter("@cd_examinado_grupo", entity.CodigoExaminadoGrupo);
            var paramCodigoExaminadoOrgao = new SqlParameter("@cd_examinado_orgao", entity.CodigoExaminadoOrgao);
            var paramDescricaoExaminadoCargo = new SqlParameter("@ds_examinado_cargo", entity.DescricaoExaminadoCargo);
            var paramNomeExaminadoAssinatura = new SqlParameter("@nm_examinado_assinatura", entity.NomeExaminadoAssinatura);
            var paramCodigoResponsavelAssinatura = new SqlParameter("@cd_responsavel_assinatura", entity.CodigoResponsavelAssinatura);
            var paramCodigoResponsavelGrupo = new SqlParameter("@cd_responsavel_grupo", entity.CodigoResponsavelGrupo);
            var paramCodigoResponsavelOrgao = new SqlParameter("@cd_responsavel_orgao", entity.CodigoResponsavelOrgao);
            var paramDescricaoResponsavelCargo = new SqlParameter("@ds_responsavel_cargo", entity.DescricaoResponsavelCargo);
            var paramNomeResponsavelAssinatura = new SqlParameter("@nm_responsavel_assinatura", entity.NomeResponsavelAssinatura);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramMensagemProdesp = new SqlParameter("@ds_msgRetornoProdesp", entity.MensagemProdesp);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);
            var paramMensagemSiafem = new SqlParameter("@ds_msgRetornoSiafem", entity.MensagemSiafem);
            var paramValor = new SqlParameter("@valor", entity.Valor);
            var paramUnidadeGestora = new SqlParameter("@cd_unidade_gestora", entity.UnidadeGestora);
            var paramGestaoFavorecido = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecido);
            var paramTotalQ1 = new SqlParameter("@TotalQ1", entity.TotalQ1);
            var paramTotalQ2 = new SqlParameter("@TotalQ2", entity.TotalQ2);
            var paramTotalQ3 = new SqlParameter("@TotalQ3", entity.TotalQ3);
            var paramTotalQ4 = new SqlParameter("@TotalQ4", entity.TotalQ4);

            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_REDUCAO_SUPLEMENTACAO_SALVAR", paramId, paramIdNotaCredito, paramIdDistribuicao, paramIdCancelamento,
                paramIdPrograma, paramIdMovimentacao, paramNrAgrupamento, paramNrSequencia, paramNrSuplementacaoReducao, paramFlProc, paramNrProcesso, paramNrOrgao,
                paramNrObra, paramRedSup, paramNrCnpjCpf, paramAutorizadoSupraFolha, paramOrigemRecurso, paramDestinoRecurso, paramEspecDespesa, paramDescEspecDespesa,
                paramCodigoAutorizadoAssinatura, paramCodigoAutorizadoGrupo, paramCodigoAutorizadoOrgao, paramDescricaoAutorizadoCargo, paramNomeAutorizadoAssinatura,
                paramCodigoExaminadoAssinatura, paramCodigoExaminadoGrupo, paramCodigoExaminadoOrgao, paramDescricaoExaminadoCargo, paramNomeExaminadoAssinatura,
                paramCodigoResponsavelAssinatura, paramCodigoResponsavelGrupo, paramCodigoResponsavelOrgao, paramDescricaoResponsavelCargo, paramNomeResponsavelAssinatura,
                paramStatusProdesp, paramMensagemProdesp, paramStatusSiafem, paramMensagemSiafem, paramValor, paramUnidadeGestora, paramGestaoFavorecido,
                paramTotalQ1, paramTotalQ2, paramTotalQ3, paramTotalQ4);

            return dbResult;
        }

        public int Save(MovimentacaoNotaDeCredito entity)
        {
            var paramId = new SqlParameter("@id_nota_credito", entity.Id);
            var paramIdPrograma = new SqlParameter("@tb_programa_id_programa", entity.IdPrograma);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramIdEstrutura = new SqlParameter("@tb_estrutura_id_estrutura", entity.IdEstrutura);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramCanDis = new SqlParameter("@cd_candis", entity.CanDis);
            var paramNrNotaCredito = new SqlParameter("@nr_siafem", entity.NumeroSiafem);
            var paramUnidadeGestoraEmitente = new SqlParameter("@cd_unidade_gestora_emitente", entity.UnidadeGestoraEmitente);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora_favorecido", entity.UnidadeGestoraFavorecida);
            var paramGestaoFavorecida = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecida);
            var paramUo = new SqlParameter("@cd_uo", entity.Uo);
            var paramUgo = new SqlParameter("@cd_ugo", entity.Ugo);
            var paramPlanoInterno = new SqlParameter("@plano_interno", entity.PlanoInterno);
            var paramValor = new SqlParameter("@vr_credito", entity.Valor);
            var paramObservacao = new SqlParameter("@ds_observacao", entity.Observacao);
            var paramObservacao2 = new SqlParameter("@ds_observacao2", entity.Observacao2);
            var paramObservacao3 = new SqlParameter("@ds_observacao3", entity.Observacao3);
            var paramEventoNC = new SqlParameter("@eventoNC", entity.EventoNC);
            var paramFonteRecurso = new SqlParameter("@fonte_recurso", entity.FonteRecurso);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramMensagemProdesp = new SqlParameter("@ds_msgRetornoProdesp", entity.MensagemProdesp);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);
            var paramMensagemSiafem = new SqlParameter("@ds_msgRetornoSiafem", entity.MensagemSiafem);

            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_CREDITO_SALVAR", paramId, paramIdPrograma, paramIdFonte, paramIdEstrutura, paramIdMovimentacao, paramNrAgrupamento, 
                paramNrSequencia, paramCanDis, paramNrNotaCredito, paramUnidadeGestoraEmitente, paramUnidadeGestoraFavorecida, paramGestaoFavorecida, paramUo, paramUgo, paramPlanoInterno, 
                paramValor, paramObservacao, paramObservacao2, paramObservacao3, paramEventoNC, paramFonteRecurso, 
                paramStatusProdesp, paramMensagemProdesp, paramStatusSiafem, paramMensagemSiafem);

            return dbResult;
        }

        public int Save(MovimentacaoDistribuicao entity)
        {
            var paramId = new SqlParameter("@id_distribuicao_movimentacao", entity.Id);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramIdFonte = new SqlParameter("@tb_fonte_id_fonte", entity.IdFonte);
            var paramNrNotaDeCredito = new SqlParameter("@nr_siafem", entity.NumeroSiafem);
            var paramUnidadeGestoraFavorecida = new SqlParameter("@cd_unidade_gestora_favorecido", entity.UnidadeGestoraFavorecida);
            var paramGestaoFavorecida = new SqlParameter("@cd_gestao_favorecido", entity.GestaoFavorecida);
            var paramEvento = new SqlParameter("@evento", entity.Evento);
            var paramCategoriaGasto = new SqlParameter("@nr_categoria_gasto", entity.CategoriaGasto);
            var paramEventoNC = new SqlParameter("@eventoNC", entity.EventoNC);
            var paramObservacao = new SqlParameter("@ds_observacao", entity.Observacao);
            var paramObservacao2 = new SqlParameter("@ds_observacao2", entity.Observacao2);
            var paramObservacao3 = new SqlParameter("@ds_observacao3", entity.Observacao3);
            var paramValor = new SqlParameter("@valor", entity.Valor);
            var paramStatusProdesp = new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp);
            var paramMensagemProdesp = new SqlParameter("@ds_msgRetornoProdesp", entity.MensagemProdesp);
            var paramStatusSiafem = new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafem);
            var paramMensagemSiafem = new SqlParameter("@ds_msgRetornoSiafem", entity.MensagemSiafem);

            var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_DISTRIBUICAO_SALVAR", paramId, paramIdMovimentacao, paramNrAgrupamento, paramNrSequencia,
                paramIdFonte, paramNrNotaDeCredito, paramUnidadeGestoraFavorecida, paramGestaoFavorecida, paramEvento, paramCategoriaGasto, paramEventoNC,
                paramObservacao, paramObservacao2, paramObservacao3, paramValor, paramStatusProdesp, paramMensagemProdesp, paramStatusSiafem, paramMensagemSiafem);

            return dbResult;
        }

        MovimentacaoOrcamentaria ICrudMovimentacao<MovimentacaoOrcamentaria>.Get(int? id, int? idAutorizacaoOB)
        {
            throw new NotImplementedException();
        }

        public AssinaturasVo BuscarUltimaAssinatura()
        {
            var dbResult = DataHelper.Get<AssinaturasVo>("PR_MOVIMENTACAO_ORCAMENTARIA_ULTIMA_ASSINATURA");

            return dbResult;
        }

        //public IEnumerable<MovimentacaoTipo> Fetch(MovimentacaoTipo entity)
        //{
        //    return DataHelper.List<MovimentacaoTipo>("PR_MOVIMENTACAO_TIPO_CONSULTAR",
        //        new SqlParameter("@ds_tipo_movimentacao_orcamentaria", entity.Descricao),
        //        new SqlParameter("@id_tipo_movimentacao_orcamentaria", entity.Id)
        //    );
        //}
        //public IEnumerable<MovimentacaoDocumentoTipo> Fetch(MovimentacaoDocumentoTipo entity)
        //{
        //    return DataHelper.List<MovimentacaoDocumentoTipo>("PR_MOVIMENTACAO_TIPO_DOCUMENTO_CONSULTAR",
        //        new SqlParameter("@ds_tipo_documento_movimentacao", entity.Descricao),
        //        new SqlParameter("@id_tipo_documento_movimentacao", entity.Id)
        //    );
        //}
        //public IEnumerable<MovimentacaoOrcamentaria> FetchCR(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoOrcamentaria> FetchDS(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoReducaoSuplementacao> FetchRS(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoReducaoSuplementacao>("PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoNotaDeCredito> FetchNC(MovimentacaoNotaDeCredito entity)
        //{
        //    return DataHelper.List<MovimentacaoNotaDeCredito>("PR_MOVIMENTACAO_NC_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoReducaoSuplementacao> FetchR(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoReducaoSuplementacao>("PR_MOVIMENTACAO_REDUCAO_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoOrcamentaria> FetchS(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_SUPLEMENTACAO_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoOrcamentaria> FetchC(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_CANCEL_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public IEnumerable<MovimentacaoOrcamentaria> FetchD(MovimentacaoOrcamentaria entity)
        //{
        //    return DataHelper.List<MovimentacaoOrcamentaria>("PR_MOVIMENTACAO_DIST_CONSULTAR",
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento)
        //    );
        //}
        //public int Remove(MovimentacaoEvento entity)
        //{
        //    var dbResult = DataHelper.Get<int>("PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_EXCLUIR", new SqlParameter("@id_evento", entity.IdMovimentacao));
        //    return dbResult;
        //}
        //public int Save(MovimentacaoEvento entity)
        //{
        //    return DataHelper.Get<int>("PR_MOVIMENTACAO_ORCAMENTARIA_EVENTO_SALVAR",
        //        new SqlParameter("@id_evento", entity.Id),
        //        new SqlParameter("@cd_evento", entity.CodigoEvento),
        //        new SqlParameter("@tb_cancelamento_movimentacao_id_cancelamento_movimentacao", entity.IdCancelamento),
        //        new SqlParameter("@tb_distribuicao_movimentacao_id_distribuicao_movimentacao", entity.IdDistribuicao),
        //        new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
        //        new SqlParameter("@nr_agrupamento", entity.NrAgrupamento),
        //        new SqlParameter("@nr_seq", entity.NrSequencia),
        //        new SqlParameter("@cd_inscricao_evento", entity.InscricaoEvento),
        //        new SqlParameter("@cd_classificacao", entity.Classificacao),
        //        new SqlParameter("@cd_fonte", entity.Fonte),
        //        new SqlParameter("@rec_despesa", entity.RecDesp),
        //        new SqlParameter("@vr_evento", entity.ValorEvento)
        //        );
        //}
        //public MovimentacaoOrcamentaria Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}
        //public int Get(MovimentacaoCancelamento item)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
