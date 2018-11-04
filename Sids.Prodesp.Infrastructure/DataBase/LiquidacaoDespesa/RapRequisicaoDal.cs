namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class RapRequisicaoDal : ICrudRapRequisicao
    {
        public int Edit(RapRequisicao entity)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_ALTERAR",
            new SqlParameter("@id_rap_requisicao", entity.Id),
            new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),
            new SqlParameter("@tb_natureza_tipo_id_natureza_tipo", entity.CodigoNaturezaItem),
            new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),
            new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
            new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),
            new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
            new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
            new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
            new SqlParameter("@nr_prodesp_original", entity.NumeroOriginalProdesp),
            new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
            new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
            new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
            new SqlParameter("@nr_recibo", entity.NumeroRecibo),
            new SqlParameter("@nr_contrato", entity.NumeroContrato),
            new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
            new SqlParameter("@cd_unidade_gestora_obra", entity.CodigoUnidadeGestoraObra),
            new SqlParameter("@cd_gestao", entity.CodigoGestao),
            new SqlParameter("@cd_gestao_fornecedora", entity.CodigoGestaoFornecedora),
            new SqlParameter("@nr_medicao", entity.DataEmissao),
            new SqlParameter("@vl_valor", entity.Valor),
            new SqlParameter("@nr_medicao", entity.NumeroMedicao),

            new SqlParameter("@cd_nota_fiscal_prodesp", entity.CodigoNotaFiscalProdesp),
            new SqlParameter("@cd_tarefa", entity.CodigoTarefa),
            new SqlParameter("@tarefa", entity.Tarefa),
            new SqlParameter("@nr_classificacao", entity.Classificacao),
            new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),
            new SqlParameter("@nr_mes_medicao", entity.MesMedicao),
            new SqlParameter("@vl_realizado", entity.ValorRealizado),
            new SqlParameter("@ds_prazo_pagamento", entity.DescricaoPrazoPagamento),
            new SqlParameter("@dt_realizado", entity.DataRealizado.ValidateDBNull()),
            new SqlParameter("@ds_despesa_referencia", entity.Referencia),
            new SqlParameter("@ds_nl_retencao_inss", entity.NlRetencaoInss),
            new SqlParameter("@ds_lista", entity.Lista),
            new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),

            new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),
            new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),
            new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),
            new SqlParameter("@cd_despesa", entity.CodigoDespesa),
            new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
            new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),
            new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),
            new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),
            new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),
            new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),
            new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),
            new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),
            new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),

            new SqlParameter("@cd_assinatura_autorizado", entity.CodigoAutorizadoAssinatura),
            new SqlParameter("@cd_assinatura_autorizado_grupo", entity.CodigoAutorizadoGrupo),
            new SqlParameter("@cd_assinatura_autorizado_orgao", entity.CodigoAutorizadoOrgao),
            new SqlParameter("@ds_assinatura_autorizado_cargo", entity.DescricaoAutorizadoCargo),
            new SqlParameter("@nm_assinatura_autorizado", entity.NomeAutorizadoAssinatura),
            new SqlParameter("@cd_assinatura_examinado", entity.CodigoExaminadoAssinatura),
            new SqlParameter("@cd_assinatura_examinado_grupo", entity.CodigoExaminadoGrupo),
            new SqlParameter("@cd_assinatura_examinado_orgao", entity.CodigoExaminadoOrgao),
            new SqlParameter("@ds_assinatura_examinado_cargo", entity.DescricaoExaminadoCargo),
            new SqlParameter("@nm_assinatura_examinado", entity.NomeExaminadoAssinatura),
            new SqlParameter("@cd_assinatura_responsavel", entity.CodigoResponsavelGrupo),
            new SqlParameter("@cd_assinatura_responsavel_grupo", entity.CodigoResponsavelOrgao),
            new SqlParameter("@cd_assinatura_responsavel_orgao", entity.CodigoResponsavelAssinatura),
            new SqlParameter("@ds_assinatura_responsavel_cargo", entity.DescricaoResponsavelCargo),
            new SqlParameter("@nm_assinatura_responsavel", entity.NomeResponsavelAssinatura),

            new SqlParameter("@nm_caucao_autorizado_por", entity.QuotaGeralAutorizadaPor),
            new SqlParameter("@nm_dados_caucao", entity.DadosCaucao),
            new SqlParameter("@vl_caucao_caucionado", entity.ValorCaucionado),
            //new SqlParameter("@vl_caucao_valor_realizado", entity.ValorRealizado),

            new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
            new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),
            new SqlParameter("@fl_sistema_podesp", entity.TransmitirProdesp),
            new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
            new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),
            new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
            new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
            new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),
            new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),
            new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),
            new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),
            new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),
            new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),
            new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),
            new SqlParameter("@fl_documento_status", entity.StatusDocumento),
            new SqlParameter("@dt_vencimento", entity.DataVencimento.ValidateDBNull()),
            new SqlParameter("@fl_referencia_digitada", entity.ReferenciaDigitada)
            );
        }

        public IEnumerable<RapRequisicao> Fetch(RapRequisicao entity)
        {
            return DataHelper.List<RapRequisicao>("PR_RAP_REQUISICAO_CONSULTAR",
                new SqlParameter("@id_rap_requisicao", entity.Id)
            );
        }

        public RapRequisicao GetLastSignatures(RapRequisicao entity)
        {
            return DataHelper.Get<RapRequisicao>("PR_RAP_REQUISICAO_CONSULTAR_ASSINATURA",
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId)
            );
        }

        public IEnumerable<RapRequisicao> FetchForGrid(RapRequisicao entity, DateTime since, DateTime until)
        {
            return DataHelper.List<RapRequisicao>("PR_RAP_REQUISICAO_CONSULTAR_GRID",
             new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
             new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
             new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
             new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
             new SqlParameter("@nr_prodesp_original", entity.NumeroOriginalProdesp),
             new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
             new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
             new SqlParameter("@nr_contrato", entity.NumeroContrato),
             new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),
             new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
             new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull())
           );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_EXCLUIR",
                new SqlParameter("@id_rap_requisicao", id)
            );
        }

        public int Add(RapRequisicao entity)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_INCLUIR",
              new SqlParameter("@id_rap_requisicao", entity.Id),
              new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),
              new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),
              new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
              new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
              new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
              new SqlParameter("@nr_prodesp_original", entity.NumeroOriginalProdesp),
              new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
              new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
              new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
              new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
              new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
              new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
              new SqlParameter("@nr_recibo", entity.NumeroRecibo),
              new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
              new SqlParameter("@cd_unidade_gestora_obra", entity.CodigoUnidadeGestoraObra),
              new SqlParameter("@cd_gestao", entity.CodigoGestao),
              new SqlParameter("@cd_gestao_fornecedora", entity.CodigoGestaoFornecedora),
              new SqlParameter("@nr_medicao", entity.DataEmissao),
              new SqlParameter("@vl_valor", entity.Valor),
              new SqlParameter("@nr_medicao", entity.NumeroMedicao),
              new SqlParameter("@tb_natureza_tipo_id_natureza_tipo", entity.CodigoNaturezaItem),
              new SqlParameter("@cd_nota_fiscal_prodesp", entity.CodigoNotaFiscalProdesp),
              new SqlParameter("@cd_tarefa", entity.CodigoTarefa),
              new SqlParameter("@tarefa", entity.Tarefa),
              new SqlParameter("@nr_classificacao", entity.Classificacao),
              new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),
              new SqlParameter("@nr_mes_medicao", entity.MesMedicao),
              new SqlParameter("@vl_realizado", entity.ValorRealizado),
              new SqlParameter("@ds_prazo_pagamento", entity.DescricaoPrazoPagamento),
              new SqlParameter("@dt_realizado", entity.DataRealizado.ValidateDBNull()),
              new SqlParameter("@ds_despesa_referencia", entity.Referencia),
              new SqlParameter("@ds_nl_retencao_inss", entity.NlRetencaoInss),
              new SqlParameter("@ds_lista", entity.Lista),
              new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),

              new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),
              new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),
              new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),
              new SqlParameter("@nr_medicao", entity.NumeroMedicao),
              new SqlParameter("@cd_despesa", entity.CodigoDespesa),
              new SqlParameter("@cd_despesa", entity.CodigoEspecificacaoDespesa),
              new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),
              new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),
              new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),
              new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),
              new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),
              new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),
              new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),
              new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),

              new SqlParameter("@cd_assinatura_autorizado", entity.CodigoAutorizadoAssinatura),
              new SqlParameter("@cd_assinatura_autorizado_grupo", entity.CodigoAutorizadoGrupo),
              new SqlParameter("@cd_assinatura_autorizado_orgao", entity.CodigoAutorizadoOrgao),
              new SqlParameter("@ds_assinatura_autorizado_cargo", entity.DescricaoAutorizadoCargo),
              new SqlParameter("@nm_assinatura_autorizado", entity.NomeAutorizadoAssinatura),
              new SqlParameter("@cd_assinatura_examinado", entity.CodigoExaminadoAssinatura),
              new SqlParameter("@cd_assinatura_examinado_grupo", entity.CodigoExaminadoGrupo),
              new SqlParameter("@cd_assinatura_examinado_orgao", entity.CodigoExaminadoOrgao),
              new SqlParameter("@ds_assinatura_examinado_cargo", entity.DescricaoExaminadoCargo),
              new SqlParameter("@nm_assinatura_examinado", entity.NomeExaminadoAssinatura),
              new SqlParameter("@cd_assinatura_responsavel", entity.CodigoResponsavelGrupo),
              new SqlParameter("@cd_assinatura_responsavel_grupo", entity.CodigoResponsavelOrgao),
              new SqlParameter("@cd_assinatura_responsavel_orgao", entity.CodigoResponsavelAssinatura),
              new SqlParameter("@ds_assinatura_responsavel_cargo", entity.DescricaoResponsavelCargo),
              new SqlParameter("@nm_assinatura_responsavel", entity.NomeResponsavelAssinatura),
              new SqlParameter("@nm_caucao_autorizado_por", entity.QuotaGeralAutorizadaPor),
              new SqlParameter("@nm_dados_caucao", entity.DadosCaucao),
              new SqlParameter("@vl_caucao_caucionado", entity.ValorCaucionado),
              //new SqlParameter("@vl_caucao_valor_realizado", entity.ValorRealizado),  campo não utilizado na requisição
              new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
              new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),
              new SqlParameter("@fl_sistema_podesp", entity.TransmitirProdesp),
              new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
              new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),
              new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
              new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
              new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),
              new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),
              new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),
              new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),
              new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),
              new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),
              new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),
              new SqlParameter("@fl_documento_status", entity.StatusDocumento),
              new SqlParameter("@dt_vencimento", entity.DataVencimento.ValidateDBNull()),
              new SqlParameter("@fl_referencia_digitada", entity.ReferenciaDigitada)
            );
        }

        public string GetTableName()
        {
            return "tb_rap_requisicao";
        }

        public int Save(RapRequisicao entity)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_SALVAR",
            new SqlParameter("@id_rap_requisicao", entity.Id),
            new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),
            new SqlParameter("@tb_natureza_tipo_id_natureza_tipo", entity.NaturezaSubempenhoId),
            new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),
            new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
            new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),
            new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
            new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
            new SqlParameter("@nr_prodesp_original", entity.NumeroOriginalProdesp),
            new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
            new SqlParameter("@nr_subempenho", entity.NumeroSubempenho),
            new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
            new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
            new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
            new SqlParameter("@nr_recibo", entity.NumeroRecibo),
            new SqlParameter("@nr_contrato", entity.NumeroContrato),
            new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
            new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
            new SqlParameter("@nr_medicao", entity.NumeroMedicao),
            new SqlParameter("@cd_unidade_gestora_obra", entity.CodigoUnidadeGestoraObra),
            new SqlParameter("@cd_gestao", entity.CodigoGestao),
            new SqlParameter("@cd_gestao_fornecedora", entity.CodigoGestaoFornecedora),
            new SqlParameter("@vl_valor", entity.Valor),
            new SqlParameter("@vl_realizado", entity.ValorRealizado),
            new SqlParameter("@vl_caucao_caucionado", entity.ValorCaucionado),
            new SqlParameter("@cd_nota_fiscal_prodesp", entity.CodigoNotaFiscalProdesp),
            new SqlParameter("@cd_tarefa", entity.CodigoTarefa),
            new SqlParameter("@tarefa", entity.Tarefa),
            new SqlParameter("@nr_classificacao", entity.Classificacao),
            new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),
            new SqlParameter("@nr_mes_medicao", entity.MesMedicao),
            new SqlParameter("@ds_prazo_pagamento", entity.DescricaoPrazoPagamento),
            new SqlParameter("@dt_realizado", entity.DataRealizado.ValidateDBNull()),
            new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
            new SqlParameter("@ds_despesa_referencia", entity.Referencia),
            new SqlParameter("@ds_nl_retencao_inss", entity.NlRetencaoInss),
            new SqlParameter("@ds_lista", entity.Lista),
            new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),
            new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),
            new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),
            new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),
            new SqlParameter("@cd_despesa", entity.CodigoDespesa),
            new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
            new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),
            new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),
            new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),
            new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),
            new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),
            new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),
            new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),
            new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),
            new SqlParameter("@cd_assinatura_autorizado", entity.CodigoAutorizadoAssinatura),
            new SqlParameter("@cd_assinatura_autorizado_grupo", entity.CodigoAutorizadoGrupo),
            new SqlParameter("@cd_assinatura_autorizado_orgao", entity.CodigoAutorizadoOrgao),
            new SqlParameter("@ds_assinatura_autorizado_cargo", entity.DescricaoAutorizadoCargo),
            new SqlParameter("@nm_assinatura_autorizado", entity.NomeAutorizadoAssinatura),
            new SqlParameter("@cd_assinatura_examinado", entity.CodigoExaminadoAssinatura),
            new SqlParameter("@cd_assinatura_examinado_grupo", entity.CodigoExaminadoGrupo),
            new SqlParameter("@cd_assinatura_examinado_orgao", entity.CodigoExaminadoOrgao),
            new SqlParameter("@ds_assinatura_examinado_cargo", entity.DescricaoExaminadoCargo),
            new SqlParameter("@nm_assinatura_examinado", entity.NomeExaminadoAssinatura),
            new SqlParameter("@cd_assinatura_responsavel", entity.CodigoResponsavelAssinatura),
            new SqlParameter("@cd_assinatura_responsavel_grupo", entity.CodigoResponsavelGrupo),
            new SqlParameter("@cd_assinatura_responsavel_orgao", entity.CodigoResponsavelOrgao),
            new SqlParameter("@ds_assinatura_responsavel_cargo", entity.DescricaoResponsavelCargo),
            new SqlParameter("@nm_assinatura_responsavel", entity.NomeResponsavelAssinatura),
            new SqlParameter("@nm_caucao_quota_geral_autorizado_por", entity.QuotaGeralAutorizadaPor),
            new SqlParameter("@nr_caucao_guia", entity.NumeroGuia),
            new SqlParameter("@nm_dados_caucao", entity.DadosCaucao),
            new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
            new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),
            new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),
            new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
            new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),
            new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
            new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
            new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),
            new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),
            new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),
            new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),
            new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),
            new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull()),
            new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),
            new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),
            new SqlParameter("@fl_documento_status", entity.StatusDocumento),
            new SqlParameter("@cd_cenario_prodesp", entity.CenarioProdesp),
            new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),
            new SqlParameter("@nr_cnpj_cpf_fornecedor", entity.NumeroCNPJCPFFornecedor),
            new SqlParameter("@dt_vencimento", entity.DataVencimento.ValidateDBNull()),
            new SqlParameter("@fl_referencia_digitada", entity.ReferenciaDigitada),
            new SqlParameter("@nr_empenho", entity.NumeroEmpenho)
            );
        }

        public RapRequisicao Get(int id)
        {
            return DataHelper.Get<RapRequisicao>("PR_RAP_REQUISICAO_SELECIONAR",
                new SqlParameter("@id_rap_requisicao", id)
            );
        }
    }
}
