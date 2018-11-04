namespace Sids.Prodesp.Infrastructure.DataBase.Reserva
{
    using Helpers;
    using Model.Entity.Reserva;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ReservaReforcoDal : ICrudReservaReforco
    {
        public string GetTableName()
        {
            return "tb_reforco";
        }

        public IEnumerable<ReservaReforco> Fetch(ReservaReforco entity)
        {
            return DataHelper.List<ReservaReforco>("PR_RESERVA_REFORCO_CONSULTAR",
               new SqlParameter("@id_reforco", entity.Codigo),
               new SqlParameter("@id_fonte", entity.Fonte),
               new SqlParameter("@id_estrutura", entity.Estrutura),
               new SqlParameter("@id_programa", entity.Programa),
               new SqlParameter("@id_regional", entity.Regional),
               new SqlParameter("@cd_reserva", entity.Reserva),
               new SqlParameter("@cd_contrato", entity.Contrato),
               new SqlParameter("@cd_processo", entity.Processo),
               new SqlParameter("@nr_reforco_prodesp", entity.NumProdesp),
               new SqlParameter("@nr_reforco_siafem_siafisico", entity.NumSiafemSiafisico),
               new SqlParameter("@cd_obra", entity.Obra),
               new SqlParameter("@nr_oc", entity.Oc),
               new SqlParameter("@cd_ugo", entity.Ugo),
               new SqlParameter("@cd_uo", entity.Uo),
               new SqlParameter("@cd_evento", entity.Evento),
               new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
               new SqlParameter("@cd_origem_recurso", entity.OrigemRecurso),
               new SqlParameter("@cd_destino_recurso", entity.DestinoRecurso),
               new SqlParameter("@ds_observacao", entity.Observacao),
               new SqlParameter("@fg_transmitido_prodesp", entity.TransmitidoProdesp),
               new SqlParameter("@fg_transmitido_siafem", entity.TransmitidoSiafem),
               new SqlParameter("@fg_transmitido_siafisico", entity.TransmitidoSiafisico),
               new SqlParameter("@bl_transmitir_prodesp     ", entity.TransmitirProdesp),
               new SqlParameter("@bl_transmitir_siafem", entity.TransmitirSiafem),
               new SqlParameter("@bl_transmitir_siafisico", entity.TransmitirSiafisico),
               new SqlParameter("@ds_autorizado_supra_folha", entity.AutorizadoSupraFolha),
               new SqlParameter("@cd_especificacao_despesa", entity.EspecificacaoDespesa),
               new SqlParameter("@ds_especificacao_despesa", entity.DescEspecificacaoDespesa),
               new SqlParameter("@cd_autorizado_assinatura", entity.AutorizadoAssinatura),
               new SqlParameter("@cd_autorizado_grupo", entity.AutorizadoGrupo),
               new SqlParameter("@cd_autorizado_orgao", entity.AutorizadoOrgao),
               new SqlParameter("@nm_autorizado_assinatura", entity.NomeAutorizadoAssinatura),
               new SqlParameter("@ds_autorizado_cargo", entity.AutorizadoCargo),
               new SqlParameter("@cd_examinado_assinatura", entity.ExaminadoAssinatura),
               new SqlParameter("@cd_examinado_grupo", entity.ExaminadoGrupo),
               new SqlParameter("@cd_examinado_orgao", entity.ExaminadoOrgao),
               new SqlParameter("@nm_examinado_assinatura", entity.NomeExaminadoAssinatura),
               new SqlParameter("@ds_examinado_cargo", entity.ExaminadoCargo),
               new SqlParameter("@cd_responsavel_assinatura", entity.ResponsavelAssinatura),
               new SqlParameter("@cd_responsavel_grupo", entity.ResponsavelGrupo),
               new SqlParameter("@cd_responsavel_orgao", entity.ResponsavelOrgao),
               new SqlParameter("@nm_responsavel_assinatura", entity.NomeResponsavelAssinatura),
               new SqlParameter("@ds_responsavel_cargo", entity.ResponsavelCargo),
               new SqlParameter("@dt_emissao_reforco", entity.DataEmissao.ValidateDBNull()),
               new SqlParameter("@ds_status_siafem_siafisico", entity.StatusSiafemSiafisico),
               new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
               new SqlParameter("@ds_status_documento", entity.StatusDoc),
               new SqlParameter("@dt_transmissao_prodesp", entity.DataTransmissaoProdesp.ValidateDBNull()),
               new SqlParameter("@dt_transmissao_siafem_siafisico", entity.DataTransmissaoSiafemSiafisico.ValidateDBNull()),
               new SqlParameter("@dt_cadastramento", entity.DataCadastro.ValidateDBNull()),
               new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto)
            );
        }

        public int Add(ReservaReforco entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_REFORCO_INCLUIR",
                new SqlParameter("@id_fonte", entity.Fonte),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@cd_reserva", entity.Reserva),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reforco_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reforco_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@nr_oc", entity.Oc),
                new SqlParameter("@cd_ugo", entity.Ugo),
                new SqlParameter("@cd_uo", entity.Uo),
                new SqlParameter("@cd_evento", entity.Evento),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@cd_origem_recurso", entity.OrigemRecurso),
                new SqlParameter("@cd_destino_recurso", entity.DestinoRecurso),
                new SqlParameter("@ds_observacao", entity.Observacao),
                new SqlParameter("@fg_transmitido_prodesp", entity.TransmitidoProdesp),
                new SqlParameter("@fg_transmitido_siafem", entity.TransmitidoSiafem),
                new SqlParameter("@fg_transmitido_siafisico", entity.TransmitidoSiafisico),
                new SqlParameter("@bl_transmitir_prodesp", entity.TransmitirProdesp),
                new SqlParameter("@bl_transmitir_siafem", entity.TransmitirSiafem),
                new SqlParameter("@bl_transmitir_siafisico", entity.TransmitirSiafisico),
                new SqlParameter("@ds_autorizado_supra_folha", entity.AutorizadoSupraFolha),
                new SqlParameter("@cd_especificacao_despesa", entity.EspecificacaoDespesa),
                new SqlParameter("@ds_especificacao_despesa", entity.DescEspecificacaoDespesa),
                new SqlParameter("@cd_autorizado_assinatura", entity.AutorizadoAssinatura),
                new SqlParameter("@cd_autorizado_grupo", entity.AutorizadoGrupo),
                new SqlParameter("@cd_autorizado_orgao", entity.AutorizadoOrgao),
                new SqlParameter("@nm_autorizado_assinatura", entity.NomeAutorizadoAssinatura),
                new SqlParameter("@ds_autorizado_cargo", entity.AutorizadoCargo),
                new SqlParameter("@cd_examinado_assinatura", entity.ExaminadoAssinatura),
                new SqlParameter("@cd_examinado_grupo", entity.ExaminadoGrupo),
                new SqlParameter("@cd_examinado_orgao", entity.ExaminadoOrgao),
                new SqlParameter("@nm_examinado_assinatura", entity.NomeExaminadoAssinatura),
                new SqlParameter("@ds_examinado_cargo", entity.ExaminadoCargo),
                new SqlParameter("@cd_responsavel_assinatura", entity.ResponsavelAssinatura),
                new SqlParameter("@cd_responsavel_grupo", entity.ResponsavelGrupo),
                new SqlParameter("@cd_responsavel_orgao", entity.ResponsavelOrgao),
                new SqlParameter("@nm_responsavel_assinatura", entity.NomeResponsavelAssinatura),
                new SqlParameter("@ds_responsavel_cargo", entity.ResponsavelCargo),
                new SqlParameter("@dt_emissao_reforco", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@ds_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@ds_status_documento", entity.StatusDoc),
                new SqlParameter("@dt_transmissao_prodesp", entity.DataTransmissaoProdesp.ValidateDBNull()),
                new SqlParameter("@dt_transmissao_siafem_siafisico", entity.DataTransmissaoSiafemSiafisico.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento", entity.DataCadastro.ValidateDBNull()),
                new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto)
              );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RESERVA_REFORCO_EXCLUIR",
                new SqlParameter("@id_reforco", id)
            );
        }

        public int Edit(ReservaReforco entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_REFORCO_ALTERAR",
                new SqlParameter("@id_reforco", entity.Codigo),
                new SqlParameter("@id_fonte", entity.Fonte),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@cd_reserva", entity.Reserva),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reforco_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reforco_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@nr_oc", entity.Oc),
                new SqlParameter("@cd_ugo", entity.Ugo),
                new SqlParameter("@cd_uo", entity.Uo),
                new SqlParameter("@cd_evento", entity.Evento),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@cd_origem_recurso", entity.OrigemRecurso),
                new SqlParameter("@cd_destino_recurso", entity.DestinoRecurso),
                new SqlParameter("@ds_observacao", entity.Observacao),
                new SqlParameter("@fg_transmitido_prodesp", entity.TransmitidoProdesp),
                new SqlParameter("@fg_transmitido_siafem", entity.TransmitidoSiafem),
                new SqlParameter("@fg_transmitido_siafisico", entity.TransmitidoSiafisico),
                new SqlParameter("@bl_transmitir_prodesp", entity.TransmitirProdesp),
                new SqlParameter("@bl_transmitir_siafem", entity.TransmitirSiafem),
                new SqlParameter("@bl_transmitir_siafisico", entity.TransmitirSiafisico),
                new SqlParameter("@ds_autorizado_supra_folha", entity.AutorizadoSupraFolha),
                new SqlParameter("@cd_especificacao_despesa", entity.EspecificacaoDespesa),
                new SqlParameter("@ds_especificacao_despesa", entity.DescEspecificacaoDespesa),
                new SqlParameter("@cd_autorizado_assinatura", entity.AutorizadoAssinatura),
                new SqlParameter("@cd_autorizado_grupo", entity.AutorizadoGrupo),
                new SqlParameter("@cd_autorizado_orgao", entity.AutorizadoOrgao),
                new SqlParameter("@nm_autorizado_assinatura", entity.NomeAutorizadoAssinatura),
                new SqlParameter("@ds_autorizado_cargo", entity.AutorizadoCargo),
                new SqlParameter("@cd_examinado_assinatura", entity.ExaminadoAssinatura),
                new SqlParameter("@cd_examinado_grupo", entity.ExaminadoGrupo),
                new SqlParameter("@cd_examinado_orgao", entity.ExaminadoOrgao),
                new SqlParameter("@nm_examinado_assinatura", entity.NomeExaminadoAssinatura),
                new SqlParameter("@ds_examinado_cargo", entity.ExaminadoCargo),
                new SqlParameter("@cd_responsavel_assinatura", entity.ResponsavelAssinatura),
                new SqlParameter("@cd_responsavel_grupo", entity.ResponsavelGrupo),
                new SqlParameter("@cd_responsavel_orgao", entity.ResponsavelOrgao),
                new SqlParameter("@nm_responsavel_assinatura", entity.NomeResponsavelAssinatura),
                new SqlParameter("@ds_responsavel_cargo", entity.ResponsavelCargo),
                new SqlParameter("@dt_emissao_reforco", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@ds_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@ds_status_documento", entity.StatusDoc),
                new SqlParameter("@dt_transmissao_prodesp", entity.DataTransmissaoProdesp.ValidateDBNull()),
                new SqlParameter("@dt_transmissao_siafem_siafisico", entity.DataTransmissaoSiafemSiafisico.ValidateDBNull()),
                new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto),
                new SqlParameter("@ds_msgRetornoTransmissaoProdesp", entity.MsgRetornoTransmissaoProdesp),
                new SqlParameter("@ds_msgRetornoTransSiafemSiafisico", entity.MsgRetornoTransSiafemSiafisico)
            );
        }

        public IEnumerable<ReservaReforco> FetchForGrid(ReservaReforco entity)
        {
            return DataHelper.List<ReservaReforco>("PR_RESERVA_REFORCO_CONSULTAR_GRID",
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reforco_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reforco_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@ds_status_siafem_siafisico", entity.StatusSiafemSiafisico),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@dt_emissao_reforcoDe", entity.DataEmissaoDe),
                new SqlParameter("@dt_emissao_reforcoAte", entity.DataEmissaoAte)
            );
        }

        public ReservaReforco BuscarAssinaturas(ReservaReforco entity)
        {

            const string sql = "PR_RESERVA_REFORCO_CONSULTAR_ASSINATURA";
            return DataHelper.Get<ReservaReforco>(sql,
                new SqlParameter("@id_regional", entity.Regional));
        }
    }
}
