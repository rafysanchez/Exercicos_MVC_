
namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EmpenhoReforcoDal : ICrudEmpenhoReforco
    {
        public int Edit(EmpenhoReforco entity)
        {
            const string sql = "PR_EMPENHO_REFORCO_ALTERAR";
            return DataHelper.Get<int>(sql,
            new SqlParameter("@id_empenho_reforco", entity.Id),
            new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
            new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),
            new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),
            new SqlParameter("@tb_fonte_id_fonte", entity.FonteId),
            new SqlParameter("@cd_fonte_siafisico", entity.CodigoFonteSiafisico),
            new SqlParameter("@tb_licitacao_id_licitacao", entity.LicitacaoId),
            new SqlParameter("@tb_modalidade_id_modalidade", entity.ModalidadeId),
            new SqlParameter("@tb_aquisicao_tipo_id_aquisicao_tipo", entity.TipoAquisicaoId),
            new SqlParameter("@tb_origem_material_id_origem_material", entity.OrigemMaterialId),
            new SqlParameter("@tb_destino_cd_destino", entity.DestinoId),
            new SqlParameter("@cd_reserva", entity.CodigoReserva),
            new SqlParameter("@cd_empenho", entity.CodigoEmpenho),
            new SqlParameter("@cd_empenho_original", entity.CodigoEmpenhoOriginal),
            new SqlParameter("@ds_acordo", entity.DescricaoAcordo),
            new SqlParameter("@nr_ano_exercicio", entity.NumeroAnoExercicio),
            new SqlParameter("@nr_processo", entity.NumeroProcesso),
            new SqlParameter("@nr_processo_ne", entity.NumeroProcessoNE),
            new SqlParameter("@nr_processo_siafisico", entity.NumeroProcessoSiafisico),
            new SqlParameter("@nr_contrato", entity.NumeroContrato),
            new SqlParameter("@nr_ct", entity.NumeroCT),
            new SqlParameter("@nr_ct_original", entity.NumeroOriginalCT),
            new SqlParameter("@nr_empenhoProdesp", entity.NumeroEmpenhoProdesp),
            new SqlParameter("@nr_empenhoSiafem", entity.NumeroEmpenhoSiafem),
            new SqlParameter("@nr_empenhoSiafisico", entity.NumeroEmpenhoSiafisico),
            new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
            new SqlParameter("@nr_natureza_item", entity.CodigoNaturezaItem),
            new SqlParameter("@nr_natureza_ne", entity.CodigoNaturezaNe),
            new SqlParameter("@dt_cadastramento", entity.DataCadastramento.ValidateDBNull()),
            new SqlParameter("@nr_cnpj_cpf_ug_credor", entity.NumeroCNPJCPFUGCredor),
            new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
            new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),
            new SqlParameter("@nr_cnpj_cpf_fornecedor", entity.NumeroCNPJCPFFornecedor),
            new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
            new SqlParameter("@cd_gestao", entity.CodigoGestao),
            new SqlParameter("@cd_evento", entity.CodigoEvento),
            new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
            new SqlParameter("@cd_unidade_gestora_fornecedora", entity.CodigoUnidadeGestoraFornecedora),
            new SqlParameter("@cd_gestao_fornecedora", entity.CodigoGestaoFornecedora),
            new SqlParameter("@cd_uo", entity.CodigoUO),
            new SqlParameter("@cd_unidade_fornecimento", entity.CodigoUnidadeFornecimento),
            new SqlParameter("@ds_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),
            new SqlParameter("@cd_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
            new SqlParameter("@ds_especificacao_despesa", entity.DescricaoEspecificacaoDespesa),
            new SqlParameter("@cd_autorizado_assinatura", entity.CodigoAutorizadoAssinatura),
            new SqlParameter("@cd_autorizado_grupo", entity.CodigoAutorizadoGrupo),
            new SqlParameter("@cd_autorizado_orgao", entity.CodigoAutorizadoOrgao),
            new SqlParameter("@nm_autorizado_assinatura", entity.NomeAutorizadoAssinatura),
            new SqlParameter("@ds_autorizado_cargo", entity.DescricaoAutorizadoCargo),
            new SqlParameter("@cd_examinado_assinatura", entity.CodigoExaminadoAssinatura),
            new SqlParameter("@cd_examinado_grupo", entity.CodigoExaminadoGrupo),
            new SqlParameter("@cd_examinado_orgao", entity.CodigoExaminadoOrgao),
            new SqlParameter("@nm_examinado_assinatura", entity.NomeExaminadoAssinatura),
            new SqlParameter("@ds_examinado_cargo", entity.DescricaoExaminadoCargo),
            new SqlParameter("@cd_responsavel_assinatura", entity.CodigoResponsavelAssinatura),
            new SqlParameter("@cd_responsavel_grupo", entity.CodigoResponsavelGrupo),
            new SqlParameter("@cd_responsavel_orgao", entity.CodigoResponsavelOrgao),
            new SqlParameter("@nm_responsavel_assinatura", entity.NomeResponsavelAssinatura),
            new SqlParameter("@ds_responsavel_cargo", entity.DescricaoResponsavelCargo),
            new SqlParameter("@bl_transmitir_prodesp", entity.TransmitirProdesp),
            new SqlParameter("@fg_transmitido_prodesp", entity.TransmitidoProdesp),
            new SqlParameter("@dt_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
            new SqlParameter("@bl_transmitir_siafem", entity.TransmitirSiafem),
            new SqlParameter("@fg_transmitido_siafem", entity.TransmitidoSiafem),
            new SqlParameter("@dt_transmitido_siafem", entity.DataTransmitidoSiafem.ValidateDBNull()),
            new SqlParameter("@bl_transmitir_siafisico", entity.TransmitirSiafisico),
            new SqlParameter("@fg_transmitido_siafisico", entity.TransmitidoSiafisico),
            new SqlParameter("@dt_transmitido_siafisico", entity.DataTransmitidoSiafisico.ValidateDBNull()),
            new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
            new SqlParameter("@ds_status_siafem", entity.StatusSiafemSiafisico),
            new SqlParameter("@ds_status_siafisico", entity.StatusSiafemSiafisico),
            new SqlParameter("@ds_status_documento", entity.StatusDocumento),
            new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto),
            new SqlParameter("@ds_msgRetornoTransmissaoProdesp", entity.MensagemServicoProdesp),
            new SqlParameter("@ds_msgRetornoTransmissaoSiafem", entity.MensagemSiafemSiafisico),
            new SqlParameter("@ds_msgRetornoTransmissaoSiafisico", entity.MensagemServicoSiafisico),
            new SqlParameter("@status_siafisico_ne", entity.StatusSiafisicoNE),
            new SqlParameter("@status_siafisico_ct", entity.StatusSiafisicoCT),
            new SqlParameter("@cd_municipio", entity.CodigoMunicipio),
            new SqlParameter("@ds_local_entrega_siafem", entity.DescricaoLocalEntregaSiafem),
            new SqlParameter("@bl_contBec", entity.ContBec));

        }

        public IEnumerable<EmpenhoReforco> Fetch(EmpenhoReforco entity)
        {
            return DataHelper.List<EmpenhoReforco>("PR_EMPENHO_REFORCO_CONSULTAR",
                new SqlParameter("@id_empenho_reforco", entity.Id)
            );
        }

        public IEnumerable<EmpenhoReforco> FetchForGrid(EmpenhoReforco entity)
        {
            return DataHelper.List<EmpenhoReforco>("PR_EMPENHO_REFORCO_CONSULTAR_GRID",
                new SqlParameter("@nr_empenhoProdesp", entity.NumeroEmpenhoProdesp),
                new SqlParameter("@nr_empenhoSiafem", entity.NumeroEmpenhoSiafem),
                new SqlParameter("@nr_empenhoSiafisico", entity.NumeroEmpenhoSiafisico),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
                new SqlParameter("@nr_processo", entity.NumeroProcesso),
                new SqlParameter("@nr_ano_exercicio", entity.NumeroAnoExercicio),
                new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
                new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),
                new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),
                new SqlParameter("@tb_fonte_id_fonte", entity.FonteId),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@ds_status_siafem", entity.StatusSiafemSiafisico),
                new SqlParameter("@ds_status_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@tb_licitacao_id_licitacao", entity.LicitacaoId),
                new SqlParameter("@nr_cnpj_cpf_ug_credor", entity.NumeroCNPJCPFUGCredor),
                new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
                new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),
                new SqlParameter("@nr_cnpj_cpf_fornecedor", entity.NumeroCNPJCPFFornecedor),
                new SqlParameter("@dt_cadastramento_de", entity.DataCadastramentoDe.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", entity.DataCadastramentoAte.ValidateDBNull())
            );
        }

        public EmpenhoReforco BuscarAssinaturas(EmpenhoReforco objModel)
        {
            const string sql = "PR_EMPENHO_REFORCO_CONSULTAR_ASSINATURA";
            return DataHelper.Get<EmpenhoReforco>(sql,
                new SqlParameter("@tb_regional_id_regional", objModel.RegionalId));
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_EMPENHO_REFORCO_EXCLUIR",
                new SqlParameter("@id_empenho_reforco", id)
            );
        }

        public string GetTableName()
        {
            return "tb_empenho";
        }

        public int Add(EmpenhoReforco entity)
        {
            const string sql = "PR_EMPENHO_REFORCO_INCLUIR";
            return DataHelper.Get<int>(sql,
              new SqlParameter("@tb_regional_id_regional", entity.RegionalId),
              new SqlParameter("@tb_programa_id_programa", entity.ProgramaId),
              new SqlParameter("@tb_estrutura_id_estrutura", entity.NaturezaId),
              new SqlParameter("@tb_fonte_id_fonte", entity.FonteId),
              new SqlParameter("@cd_fonte_siafisico", entity.CodigoFonteSiafisico),
              new SqlParameter("@tb_licitacao_id_licitacao", entity.LicitacaoId),
              new SqlParameter("@tb_modalidade_id_modalidade",entity.ModalidadeId),                   
              new SqlParameter("@tb_aquisicao_tipo_id_aquisicao_tipo", entity.TipoAquisicaoId),
              new SqlParameter("@tb_origem_material_id_origem_material", entity.OrigemMaterialId),
              new SqlParameter("@tb_destino_cd_destino", entity.DestinoId),
              new SqlParameter("@cd_reserva", entity.CodigoReserva),
              new SqlParameter("@cd_empenho", entity.CodigoEmpenho),
              new SqlParameter("@cd_empenho_original", entity.CodigoEmpenhoOriginal),
              new SqlParameter("@ds_acordo", entity.DescricaoAcordo),
              new SqlParameter("@nr_ano_exercicio", entity.NumeroAnoExercicio),
              new SqlParameter("@nr_processo", entity.NumeroProcesso),
              new SqlParameter("@nr_processo_ne", entity.NumeroProcessoNE),
              new SqlParameter("@nr_processo_siafisico", entity.NumeroProcessoSiafisico),
              new SqlParameter("@nr_contrato", entity.NumeroContrato),
              new SqlParameter("@nr_ct", entity.NumeroCT),
              new SqlParameter("@nr_ct_original", entity.NumeroOriginalCT),
              new SqlParameter("@nr_empenhoProdesp", entity.NumeroEmpenhoProdesp),
              new SqlParameter("@nr_empenhoSiafem", entity.NumeroEmpenhoSiafem),
              new SqlParameter("@nr_empenhoSiafisico", entity.NumeroEmpenhoSiafisico),
              new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
              new SqlParameter("@nr_natureza_item", entity.CodigoNaturezaItem),
              new SqlParameter("@nr_natureza_ne", entity.CodigoNaturezaNe),
              new SqlParameter("@dt_cadastramento", entity.DataCadastramento.ValidateDBNull()),
              new SqlParameter("@nr_cnpj_cpf_ug_credor", entity.NumeroCNPJCPFUGCredor),
              new SqlParameter("@cd_gestao_credor", entity.CodigoGestaoCredor),
              new SqlParameter("@cd_credor_organizacao", entity.CodigoCredorOrganizacao),
              new SqlParameter("@nr_cnpj_cpf_fornecedor", entity.NumeroCNPJCPFFornecedor),
              new SqlParameter("@cd_unidade_gestora", entity.CodigoUnidadeGestora),
              new SqlParameter("@cd_gestao", entity.CodigoGestao),
              new SqlParameter("@cd_evento", entity.CodigoEvento),
              new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),                  
              new SqlParameter("@cd_unidade_gestora_fornecedora", entity.CodigoUnidadeGestoraFornecedora),
              new SqlParameter("@cd_gestao_fornecedora", entity.CodigoGestaoFornecedora),                   
              new SqlParameter("@cd_uo", entity.CodigoUO),
              new SqlParameter("@cd_unidade_fornecimento", entity.CodigoUnidadeFornecimento),                 
              new SqlParameter("@ds_autorizado_supra_folha", entity.DescricaoAutorizadoSupraFolha),
              new SqlParameter("@cd_especificacao_despesa", entity.CodigoEspecificacaoDespesa),
              new SqlParameter("@ds_especificacao_despesa", entity.DescricaoEspecificacaoDespesa),
              new SqlParameter("@cd_autorizado_assinatura", entity.CodigoAutorizadoAssinatura),
              new SqlParameter("@cd_autorizado_grupo", entity.CodigoAutorizadoGrupo),
              new SqlParameter("@cd_autorizado_orgao", entity.CodigoAutorizadoOrgao),
              new SqlParameter("@nm_autorizado_assinatura", entity.NomeAutorizadoAssinatura),
              new SqlParameter("@ds_autorizado_cargo", entity.DescricaoAutorizadoCargo),
              new SqlParameter("@cd_examinado_assinatura", entity.CodigoExaminadoAssinatura),
              new SqlParameter("@cd_examinado_grupo", entity.CodigoExaminadoGrupo),
              new SqlParameter("@cd_examinado_orgao", entity.CodigoExaminadoOrgao),
              new SqlParameter("@nm_examinado_assinatura", entity.NomeExaminadoAssinatura),
              new SqlParameter("@ds_examinado_cargo", entity.DescricaoExaminadoCargo),
              new SqlParameter("@cd_responsavel_assinatura", entity.CodigoResponsavelAssinatura),
              new SqlParameter("@cd_responsavel_grupo", entity.CodigoResponsavelGrupo),
              new SqlParameter("@cd_responsavel_orgao", entity.CodigoResponsavelOrgao),
              new SqlParameter("@nm_responsavel_assinatura", entity.NomeResponsavelAssinatura),
              new SqlParameter("@ds_responsavel_cargo", entity.DescricaoResponsavelCargo),
              new SqlParameter("@bl_transmitir_prodesp", entity.TransmitirProdesp),
              new SqlParameter("@fg_transmitido_prodesp", entity.TransmitidoProdesp),
              new SqlParameter("@dt_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
              new SqlParameter("@bl_transmitir_siafem", entity.TransmitirSiafem),
              new SqlParameter("@fg_transmitido_siafem", entity.TransmitidoSiafem),
              new SqlParameter("@dt_transmitido_siafem", entity.DataTransmitidoSiafem.ValidateDBNull()),
              new SqlParameter("@bl_transmitir_siafisico", entity.TransmitirSiafisico),
              new SqlParameter("@fg_transmitido_siafisico", entity.TransmitidoSiafisico),
              new SqlParameter("@dt_transmitido_siafisico", entity.DataTransmitidoSiafisico.ValidateDBNull()),
              new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
              new SqlParameter("@ds_status_siafem", entity.StatusSiafemSiafisico),
              new SqlParameter("@ds_status_siafisico", entity.StatusSiafemSiafisico),
              new SqlParameter("@ds_status_documento", entity.StatusDocumento),
              new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto),
              new SqlParameter("@ds_msgRetornoTransmissaoProdesp", entity.MensagemServicoProdesp),
              new SqlParameter("@ds_msgRetornoTransmissaoSiafisico", entity.MensagemServicoSiafisico),
              new SqlParameter("@ds_msgRetornoTransmissaoSiafem", entity.MensagemSiafemSiafisico),
              new SqlParameter("@status_siafisico_ne", entity.StatusSiafisicoNE),
              new SqlParameter("@status_siafisico_ct", entity.StatusSiafisicoCT),
              new SqlParameter("@cd_municipio", entity.CodigoMunicipio),
              new SqlParameter("@ds_local_entrega_siafem", entity.DescricaoLocalEntregaSiafem),
              new SqlParameter("@bl_contBec", entity.ContBec));

        }                      
        public string ObterLogNome()
        {                       
            return "tb_empenho_reforco";
        }                      
    }
}

