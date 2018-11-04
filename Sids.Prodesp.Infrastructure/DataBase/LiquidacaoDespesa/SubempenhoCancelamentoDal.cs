namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SubempenhoCancelamentoDal : ICrudSubempenhoCancelamento
    {
        public int Edit(SubempenhoCancelamento entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_ALTERAR",
                new SqlParameter("@id_subempenho_cancelamento", entity.Id),
                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull()),
                new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),
                new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),
                new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@tb_cenario_id_cenario", entity.CenarioSiafemSiafisico),
                new SqlParameter("@nr_subempenho_prodesp", entity.NumeroOriginalProdesp),
                new SqlParameter("@vl_realizado", entity.ValorRealizado),
                new SqlParameter("@vl_anular", entity.ValorAnular),
                new SqlParameter("@cd_cenario_prodesp", entity.CenarioProdesp),
                new SqlParameter("@nr_nl_referencia", entity.NlReferencia),
                new SqlParameter("@nr_ct", entity.NumeroCT),
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
                new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
                new SqlParameter("@cd_gestao", entity.CodigoGestao),
                new SqlParameter("@vl_valor", entity.Valor),
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@tb_evento_tipo_id_evento_tipo", entity.TipoEventoId),
                new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
                new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),
                new SqlParameter("@nr_mes_medicao", entity.MesMedicao),
                new SqlParameter("@nr_percentual", entity.Percentual),
                new SqlParameter("@tb_obra_tipo_id_obra_tipo", entity.TipoObraId),
                new SqlParameter("@cd_obra_tipo", entity.CodigoTipoDeObra),
                new SqlParameter("@nr_obra", entity.NumeroObra),
                new SqlParameter("@cd_unidade_gestora_obra", entity.CodigoUnidadeGestoraObra),
                new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),
                new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),
                new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
                new SqlParameter("@ds_despesa_referencia", entity.Referencia),
                new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),
                new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
                new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),
                new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),
                new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),
                new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),
                new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),
                new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),
                new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),
                new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),
                new SqlParameter("@ds_despesa_especificacao_9", entity.DescricaoEspecificacaoDespesa9),
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
                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),
                new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),
                new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),
                new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),
                new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),
                new SqlParameter("@fl_documento_status", entity.StatusDocumento)
            );
        }

        public IEnumerable<SubempenhoCancelamento> Fetch(SubempenhoCancelamento entity)
        {
            return DataHelper.List<SubempenhoCancelamento>("PR_SUBEMPENHO_CANCELAMENTO_CONSULTAR",
                new SqlParameter("@id_subempenho_cancelamento", entity.Id)
            );
        }

        public SubempenhoCancelamento GetLastSignatures(SubempenhoCancelamento entity)
        {
            return DataHelper.Get<SubempenhoCancelamento>("PR_SUBEMPENHO_CANCELAMENTO_CONSULTAR_ASSINATURA",
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId)
            );
        }

        public IEnumerable<SubempenhoCancelamento> BuscarGrid(SubempenhoCancelamento entity, DateTime since, DateTime until)
        {
            return DataHelper.List<SubempenhoCancelamento>("PR_SUBEMPENHO_CANCELAMENTO_CONSULTAR_GRID",
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
                new SqlParameter("@nr_subempenho_prodesp", entity.NumeroOriginalProdesp), 
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
                new SqlParameter("@tb_cenario_id_cenario", entity.CenarioSiafemSiafisico),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
                new SqlParameter("dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("dt_cadastramento_ate", until.ValidateDBNull()),
                new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_EXCLUIR",
                new SqlParameter("@id_subempenho_cancelamento", id)
            );
        }

        public int Add(SubempenhoCancelamento entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_INCLUIR",
                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull()),
                new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),
                new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),
                new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@tb_cenario_id_cenario", entity.CenarioSiafemSiafisico),
                new SqlParameter("@nr_subempenho_prodesp", entity.NumeroOriginalProdesp),
                new SqlParameter("@vl_realizado", entity.ValorRealizado),
                new SqlParameter("@cd_cenario_prodesp", entity.CenarioProdesp),
                new SqlParameter("@nr_nl_referencia", entity.NlReferencia),
                new SqlParameter("@nr_ct", entity.NumeroCT),
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
                new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
                new SqlParameter("@cd_gestao", entity.CodigoGestao),
                new SqlParameter("@vl_valor", entity.Valor),
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@tb_evento_tipo_id_evento_tipo", entity.TipoEventoId),
                new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
                new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),
                new SqlParameter("@nr_mes_medicao", entity.MesMedicao),
                new SqlParameter("@nr_percentual", entity.Percentual),
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
                new SqlParameter("@tb_obra_tipo_id_obra_tipo", entity.TipoObraId),
                new SqlParameter("@cd_obra_tipo", entity.CodigoTipoDeObra),
                new SqlParameter("@nr_obra", entity.NumeroObra),
                new SqlParameter("@cd_unidade_gestora_obra", entity.CodigoUnidadeGestoraObra),
                new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),
                new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),
                new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
                new SqlParameter("@ds_despesa_referencia", entity.Referencia),
                new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),
                new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
                new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),
                new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),
                new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),
                new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),
                new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),
                new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),
                new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),
                new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),
                new SqlParameter("@ds_despesa_especificacao_9", entity.DescricaoEspecificacaoDespesa9),
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
                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),
                new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),
                new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),
                new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),
                new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),
                new SqlParameter("@fl_documento_status", entity.StatusDocumento)
            );
        }

        public string GetTableName()
        {
            return "tb_subempenho";
        }

        public int Save(SubempenhoCancelamento entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_SALVAR",
                new SqlParameter("@id_subempenho_cancelamento", entity.Id),
                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull()),
                new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),
                new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),
                new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@tb_cenario_id_cenario", entity.CenarioSiafemSiafisico),
                new SqlParameter("@nr_subempenho_prodesp", entity.NumeroSubempenhoProdesp),
                new SqlParameter("@vl_realizado", entity.ValorRealizado),
                new SqlParameter("@vl_anular", entity.ValorAnular),
                new SqlParameter("@cd_cenario_prodesp", entity.CenarioProdesp),
                new SqlParameter("@nr_nl_referencia", entity.NlReferencia),
                new SqlParameter("@nr_ct", entity.NumeroCT),
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),
                new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
                new SqlParameter("@cd_gestao", entity.CodigoGestao),
                new SqlParameter("@vl_valor", entity.Valor),
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@cd_evento", entity.CodigoEvento),
                new SqlParameter("@tb_evento_tipo_id_evento_tipo", entity.TipoEventoId),
                new SqlParameter("@nr_cnpj_cpf_credor", entity.NumeroCNPJCPFCredor),
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
                new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),
                new SqlParameter("@nr_mes_medicao", entity.MesMedicao),
                new SqlParameter("@nr_percentual", entity.Percentual),
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
                new SqlParameter("@tb_obra_tipo_id_obra_tipo", entity.TipoObraId),
                new SqlParameter("@cd_obra_tipo", entity.CodigoTipoDeObra),
                new SqlParameter("@nr_obra", entity.NumeroObra),
                new SqlParameter("@cd_unidade_gestora_obra", entity.CodigoUnidadeGestoraObra),
                new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),
                new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),
                new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
                new SqlParameter("@ds_despesa_referencia", entity.Referencia),
                new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),
                new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
                new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),
                new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),
                new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),
                new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),
                new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),
                new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),
                new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),
                new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),
                new SqlParameter("@ds_despesa_especificacao_9", entity.DescricaoEspecificacaoDespesa9),
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
                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),
                new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),
                new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),
                new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),
                new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),
                new SqlParameter("@fl_documento_status", entity.StatusDocumento)
            );
        }

        public SubempenhoCancelamento Get(int Id)
        {
            return DataHelper.Get<SubempenhoCancelamento>("PR_SUBEMPENHO_CANCELAMENTO_SELECIONAR",
                new SqlParameter("@id_subempenho_cancelamento", Id)
            );
        }
    }
}
