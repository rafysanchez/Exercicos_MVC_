namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class RapInscricaoDal : ICrudRapInscricao
    {
        public int Edit(RapInscricao entity)
        {
            return DataHelper.Get<int>("PR_RAP_INSCRICAO_ALTERAR",
            new SqlParameter("@id_rap_inscricao", entity.Id), //int 
                new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),//int
                new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),//int
                new SqlParameter("@tb_natureza_tipo_id_natureza_tipo", entity.CodigoNaturezaItem),//char
                new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),//int
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId), //smallint
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),//varchar
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),//varchar
                new SqlParameter("@nr_empenho_prodesp", entity.NumeroOriginalProdesp),//varchar
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),//varchar
                new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),//int
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),//varchar
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),//varchar
                new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),//varchar
                new SqlParameter("@cd_gestao", entity.CodigoGestao),//varchar
                new SqlParameter("@cd_nota_fiscal_prodesp", entity.CodigoNotaFiscalProdesp),//varchar
                new SqlParameter("@vl_caucao_caucionado", entity.ValorCaucionado),//int
                new SqlParameter("@vl_valor", entity.Valor),//int
                new SqlParameter("@vl_realizado", entity.ValorRealizado),//int
                new SqlParameter("@nr_ano_exercicio", entity.NumeroAnoExercicio),//int
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),//date
                new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),//char
                new SqlParameter("@nr_mes_medicao", entity.MesMedicao),//char
                new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),//varchar
                new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),//varchar
                new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),//varchar
                new SqlParameter("@cd_despesa", entity.CodigoDespesa),//varchar
                new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),//char
                new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),//char
                new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),//varchar
                new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),//varchar
                new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),//varchar
                new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),//varchar
                new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),//varchar
                new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),//varchar
                new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),//varchar
                new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),//varchar
                new SqlParameter("@cd_assinatura_autorizado", entity.CodigoAutorizadoAssinatura),//int
                new SqlParameter("@cd_assinatura_autorizado_grupo", entity.CodigoAutorizadoGrupo),//int
                new SqlParameter("@cd_assinatura_autorizado_orgao", entity.CodigoAutorizadoOrgao),//char
                new SqlParameter("@ds_assinatura_autorizado_cargo", entity.DescricaoAutorizadoCargo),//varchar
                new SqlParameter("@nm_assinatura_autorizado", entity.NomeAutorizadoAssinatura),//varchar
                new SqlParameter("@cd_assinatura_examinado", entity.CodigoExaminadoAssinatura),//int
                new SqlParameter("@cd_assinatura_examinado_grupo", entity.CodigoExaminadoGrupo),//int
                new SqlParameter("@cd_assinatura_examinado_orgao", entity.CodigoExaminadoOrgao),//char
                new SqlParameter("@ds_assinatura_examinado_cargo", entity.DescricaoExaminadoCargo),//varchar
                new SqlParameter("@nm_assinatura_examinado", entity.NomeExaminadoAssinatura),//varchar
                new SqlParameter("@cd_assinatura_responsavel_grupo", entity.CodigoResponsavelGrupo),//int
                new SqlParameter("@cd_assinatura_responsavel_orgao", entity.CodigoResponsavelOrgao),//int
                new SqlParameter("@cd_assinatura_responsavel", entity.CodigoResponsavelAssinatura),//char
                new SqlParameter("@ds_assinatura_responsavel_cargo", entity.DescricaoResponsavelCargo),//varchar
                new SqlParameter("@nm_assinatura_responsavel", entity.NomeResponsavelAssinatura),//varchar
                new SqlParameter("@nr_caucao_guia", entity.DadosCaucao),//varchar
                new SqlParameter("@nm_caucao_quota_geral_autorizado_por", entity.QuotaGeralAutorizadaPor),//varchar

                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),//char
                new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),//bit
                new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),//date
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),//varchar
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),//char
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),//char
                new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),//bit
                new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),//bit
                new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),//date
                new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),//varchar
                new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),//bit
                new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),//bit
                new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),//bit
                new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),//bit
                new SqlParameter("@fl_documento_status", entity.StatusDocumento),//bit
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),//varchar
                new SqlParameter("@nr_contrato", entity.NumeroContrato),//varchar

                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull())//date
           );
        }

        public IEnumerable<RapInscricao> Fetch(RapInscricao entity)
        {
            return DataHelper.List<RapInscricao>("PR_RAP_INSCRICAO_CONSULTAR",
                new SqlParameter("@id_rap_inscricao", entity.Id)
            );
        }

        public RapInscricao GetLastSignatures(RapInscricao entity)
        {
            return DataHelper.Get<RapInscricao>("PR_RAP_INSCRICAO_CONSULTAR_ASSINATURA",
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId)
            );
        }

        public IEnumerable<RapInscricao> FetchForGrid(RapInscricao entity, DateTime since, DateTime until)
        {
            return DataHelper.List<RapInscricao>("PR_RAP_INSCRICAO_CONSULTAR_GRID",
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),
                new SqlParameter("@nr_empenho_prodesp", entity.NumeroOriginalProdesp),
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),
                new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico), 
                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull())
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RAP_INSCRICAO_EXCLUIR",
                new SqlParameter("@id_rap_inscricao", id)
            );
        }

        public int Add(RapInscricao entity)
        {
            return DataHelper.Get<int>("PR_RAP_INSCRICAO_INCLUIR",
                new SqlParameter("@id_rap_inscricao", entity.Id), //int 
                new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),//int
                new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),//int
                new SqlParameter("@tb_natureza_tipo_id_natureza_tipo", entity.CodigoNaturezaItem),//char
                new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),//int
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId), //smallint
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),//varchar
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),//varchar
                new SqlParameter("@nr_empenho_prodesp", entity.NumeroOriginalProdesp),//varchar
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),//varchar
                new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),//int
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),//varchar
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),//varchar
                new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),//varchar
                new SqlParameter("@cd_gestao", entity.CodigoGestao),//varchar
                new SqlParameter("@cd_nota_fiscal_prodesp", entity.CodigoNotaFiscalProdesp),//varchar
                new SqlParameter("@vl_caucao_caucionado", entity.ValorCaucionado),//int
                new SqlParameter("@vl_valor", entity.Valor),//int
                new SqlParameter("@vl_realizado", entity.ValorRealizado),//int
                new SqlParameter("@nr_ano_exercicio", entity.NumeroAnoExercicio),//int
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),//date
                new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),//char
                new SqlParameter("@nr_mes_medicao", entity.MesMedicao),//char
                new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),//varchar
                new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),//varchar
                new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),//varchar
                new SqlParameter("@cd_despesa", entity.CodigoDespesa),//varchar
                new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),//char
                new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),//char
                new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),//varchar
                new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),//varchar
                new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),//varchar
                new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),//varchar
                new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),//varchar
                new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),//varchar
                new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),//varchar
                new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),//varchar
                new SqlParameter("@cd_assinatura_autorizado", entity.CodigoAutorizadoAssinatura),//int
                new SqlParameter("@cd_assinatura_autorizado_grupo", entity.CodigoAutorizadoGrupo),//int
                new SqlParameter("@cd_assinatura_autorizado_orgao", entity.CodigoAutorizadoOrgao),//char
                new SqlParameter("@ds_assinatura_autorizado_cargo", entity.DescricaoAutorizadoCargo),//varchar
                new SqlParameter("@nm_assinatura_autorizado", entity.NomeAutorizadoAssinatura),//varchar
                new SqlParameter("@cd_assinatura_examinado", entity.CodigoExaminadoAssinatura),//int
                new SqlParameter("@cd_assinatura_examinado_grupo", entity.CodigoExaminadoGrupo),//int
                new SqlParameter("@cd_assinatura_examinado_orgao", entity.CodigoExaminadoOrgao),//char
                new SqlParameter("@ds_assinatura_examinado_cargo", entity.DescricaoExaminadoCargo),//varchar
                new SqlParameter("@nm_assinatura_examinado", entity.NomeExaminadoAssinatura),//varchar
                new SqlParameter("@cd_assinatura_responsavel_grupo", entity.CodigoResponsavelGrupo),//int
                new SqlParameter("@cd_assinatura_responsavel_orgao", entity.CodigoResponsavelOrgao),//int
                new SqlParameter("@cd_assinatura_responsavel", entity.CodigoResponsavelAssinatura),//char
                new SqlParameter("@ds_assinatura_responsavel_cargo", entity.DescricaoResponsavelCargo),//varchar
                new SqlParameter("@nm_assinatura_responsavel", entity.NomeResponsavelAssinatura),//varchar
                new SqlParameter("@nr_caucao_guia", entity.DadosCaucao),//varchar
                new SqlParameter("@nm_caucao_quota_geral_autorizado_por", entity.QuotaGeralAutorizadaPor),//varchar

                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),//char
                new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),//bit
                new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),//date
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),//varchar
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),//char
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),//char
                new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),//bit
                new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),//bit
                new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),//date
                new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),//varchar
                new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),//bit
                new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),//bit
                new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),//bit
                new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),//bit
                new SqlParameter("@fl_documento_status", entity.StatusDocumento),//bit
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),//varchar
                new SqlParameter("@nr_contrato", entity.NumeroContrato),//varchar

                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull())//date
            );
        }

        public string GetTableName()
        {
            return "tb_rap_inscricao";
        }

        public int Save(RapInscricao entity)
        {
            return DataHelper.Get<int>("PR_RAP_INSCRICAO_SALVAR",
                new SqlParameter("@id_rap_inscricao", entity.Id), //int 
                new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),//int
                new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),//int
                new SqlParameter("@tb_natureza_tipo_id_natureza_tipo", entity.CodigoNaturezaItem),//char
                new SqlParameter("@tb_servico_tipo_id_servico_tipo", entity.TipoServicoId),//int
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId), //smallint
                new SqlParameter("@nr_prodesp", entity.NumeroProdesp),//varchar
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafemSiafisico),//varchar
                new SqlParameter("@nr_empenho_prodesp", entity.NumeroOriginalProdesp),//varchar
                new SqlParameter("@nr_empenho_siafem_siafisico", entity.NumeroOriginalSiafemSiafisico),//varchar
                new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),//int
                new SqlParameter("@nr_despesa_processo", entity.NumeroProcesso),//varchar
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),//varchar
                new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),//varchar
                new SqlParameter("@cd_gestao", entity.CodigoGestao),//varchar
                new SqlParameter("@cd_nota_fiscal_prodesp", entity.CodigoNotaFiscalProdesp),//varchar
                new SqlParameter("@vl_caucao_caucionado", entity.ValorCaucionado),//int
                new SqlParameter("@nr_caucao_guia", entity.NumeroGuia),//int
                new SqlParameter("@vl_valor", entity.Valor),//int
                new SqlParameter("@vl_realizado", entity.ValorRealizado),//int
                new SqlParameter("@nr_ano_exercicio", entity.NumeroAnoExercicio),//int
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),//date
                new SqlParameter("@nr_ano_medicao", entity.AnoMedicao),//char
                new SqlParameter("@nr_mes_medicao", entity.MesMedicao),//char
                new SqlParameter("@ds_observacao_1", entity.DescricaoObservacao1),//varchar
                new SqlParameter("@ds_observacao_2", entity.DescricaoObservacao2),//varchar
                new SqlParameter("@ds_observacao_3", entity.DescricaoObservacao3),//varchar
                new SqlParameter("@cd_despesa", entity.CodigoDespesa),//varchar
                new SqlParameter("@ds_despesa_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),//char
                new SqlParameter("@cd_despesa_especificacao_despesa", entity.CodigoEspecificacaoDespesa),//char
                new SqlParameter("@ds_despesa_especificacao_1", entity.DescricaoEspecificacaoDespesa1),//varchar
                new SqlParameter("@ds_despesa_especificacao_2", entity.DescricaoEspecificacaoDespesa2),//varchar
                new SqlParameter("@ds_despesa_especificacao_3", entity.DescricaoEspecificacaoDespesa3),//varchar
                new SqlParameter("@ds_despesa_especificacao_4", entity.DescricaoEspecificacaoDespesa4),//varchar
                new SqlParameter("@ds_despesa_especificacao_5", entity.DescricaoEspecificacaoDespesa5),//varchar
                new SqlParameter("@ds_despesa_especificacao_6", entity.DescricaoEspecificacaoDespesa6),//varchar
                new SqlParameter("@ds_despesa_especificacao_7", entity.DescricaoEspecificacaoDespesa7),//varchar
                new SqlParameter("@ds_despesa_especificacao_8", entity.DescricaoEspecificacaoDespesa8),//varchar
                new SqlParameter("@cd_assinatura_autorizado", entity.CodigoAutorizadoAssinatura),//int
                new SqlParameter("@cd_assinatura_autorizado_grupo", entity.CodigoAutorizadoGrupo),//int
                new SqlParameter("@cd_assinatura_autorizado_orgao", entity.CodigoAutorizadoOrgao),//char
                new SqlParameter("@ds_assinatura_autorizado_cargo", entity.DescricaoAutorizadoCargo),//varchar
                new SqlParameter("@nm_assinatura_autorizado", entity.NomeAutorizadoAssinatura),//varchar
                new SqlParameter("@cd_assinatura_examinado", entity.CodigoExaminadoAssinatura),//int
                new SqlParameter("@cd_assinatura_examinado_grupo", entity.CodigoExaminadoGrupo),//int
                new SqlParameter("@cd_assinatura_examinado_orgao", entity.CodigoExaminadoOrgao),//char
                new SqlParameter("@ds_assinatura_examinado_cargo", entity.DescricaoExaminadoCargo),//varchar
                new SqlParameter("@nm_assinatura_examinado", entity.NomeExaminadoAssinatura),//varchar
                new SqlParameter("@cd_assinatura_responsavel_grupo", entity.CodigoResponsavelGrupo),//int
                new SqlParameter("@cd_assinatura_responsavel_orgao", entity.CodigoResponsavelOrgao),//int
                new SqlParameter("@cd_assinatura_responsavel", entity.CodigoResponsavelAssinatura),//char
                new SqlParameter("@ds_assinatura_responsavel_cargo", entity.DescricaoResponsavelCargo),//varchar
                new SqlParameter("@nm_assinatura_responsavel", entity.NomeResponsavelAssinatura),//varchar
                new SqlParameter("@nm_caucao_quota_geral_autorizado_por", entity.QuotaGeralAutorizadaPor),//varchar

                new SqlParameter("@cd_transmissao_status_prodesp", entity.StatusProdesp),//char
                new SqlParameter("@fl_transmissao_transmitido_prodesp", entity.TransmitidoProdesp),//bit
                new SqlParameter("@dt_transmissao_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),//date
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemProdesp),//varchar
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafemSiafisico),//char
                new SqlParameter("@cd_transmissao_status_siafisico", entity.StatusSiafemSiafisico),//char
                new SqlParameter("@fl_transmissao_transmitido_siafem_siafisico", entity.TransmitidoSiafem),//bit
                new SqlParameter("@fl_transmissao_transmitido_siafisico", entity.TransmitidoSiafisico),//bit
                new SqlParameter("@dt_transmissao_transmitido_siafem_siafisico", entity.DataTransmitidoSiafemSiafisico.ValidateDBNull()),//date
                new SqlParameter("@ds_transmissao_mensagem_siafem_siafisico", entity.MensagemSiafemSiafisico),//varchar
                new SqlParameter("@fl_sistema_prodesp", entity.TransmitirProdesp),//bit
                new SqlParameter("@fl_sistema_siafem_siafisico", entity.TransmitirSiafem),//bit
                new SqlParameter("@fl_sistema_siafisico", entity.TransmitirSiafisico),//bit
                new SqlParameter("@fl_documento_completo", entity.CadastroCompleto),//bit
                new SqlParameter("@fl_documento_status", entity.StatusDocumento),//bit
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),//varchar
                new SqlParameter("@nr_contrato", entity.NumeroContrato),//varchar

                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull()),//date
                new SqlParameter("@ds_uso_autorizado_por", entity.DescricaoUsoAutorizadoPor),
                new SqlParameter("@nr_cnpj_cpf_fornecedor", entity.NumeroCNPJCPFFornecedor)

            );
        }

        public RapInscricao Get(int id)
        {
            return DataHelper.Get<RapInscricao>("PR_RAP_INSCRICAO_SELECIONAR",
             new SqlParameter("@id_rap_inscricao", id)
            );
        }
    }
}
