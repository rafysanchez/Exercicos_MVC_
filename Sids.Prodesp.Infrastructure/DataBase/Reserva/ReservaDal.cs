namespace Sids.Prodesp.Infrastructure.DataBase.Reserva
{
    using Helpers;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ReservaDal : ICrudReserva
    {
        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RESERVA_EXCLUIR",
                new SqlParameter("@id_reserva", id)
            );
        }

        public string GetTableName()
        {
            return "tb_reserva";
        }

        public int Add(Model.Entity.Reserva.Reserva entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_INCLUIR",
                new SqlParameter("@id_fonte", entity.Fonte),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_tipo_reserva", entity.Tipo),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reserva_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reserva_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@nr_oc", entity.Oc),
                new SqlParameter("@cd_ugo", entity.Ugo),
                new SqlParameter("@cd_uo", entity.Uo),
                new SqlParameter("@cd_evento", entity.Evento),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@nr_ano_referencia_reserva", entity.AnoReferencia),
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
                new SqlParameter("@dt_emissao_reserva", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@dt_cadastro", entity.DataCadastro.ValidateDBNull()),
                new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto)
            );
        }
        public Model.Entity.Reserva.Reserva BuscarAssinaturas(Model.Entity.Reserva.Reserva entity)
        {

            const string sql = "PR_RESERVA_CONSULTAR_ASSINATURA";
            return DataHelper.Get<Model.Entity.Reserva.Reserva>(sql,
              new SqlParameter("@id_regional", entity.Regional));

        }

        public IEnumerable<Model.Entity.Reserva.Reserva> Fetch(Model.Entity.Reserva.Reserva entity)
        {
            return DataHelper.List<Model.Entity.Reserva.Reserva>("PR_RESERVA_CONSULTAR",
                new SqlParameter("@id_reserva", entity.Codigo),
                new SqlParameter("@id_fonte", entity.Fonte),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_tipo_reserva", entity.Tipo),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reserva_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reserva_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@nr_oc", entity.Oc),
                new SqlParameter("@cd_ugo", entity.Ugo),
                new SqlParameter("@cd_uo", entity.Uo),
                new SqlParameter("@cd_evento", entity.Evento),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@nr_ano_referencia_reserva", entity.AnoReferencia),
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
                new SqlParameter("@dt_emissao_reservaDe", entity.DataEmissaoDe.ValidateDBNull()),
                new SqlParameter("@dt_emissao_reservaAte", entity.DataEmissaoAte.ValidateDBNull())
            );
         }

        public IEnumerable<Model.Entity.Reserva.Reserva> FetchForGrid(Model.Entity.Reserva.Reserva entity)
        {
            return DataHelper.List<Model.Entity.Reserva.Reserva>("PR_RESERVA_CONSULTAR_GRID",
                new SqlParameter("@id_reserva", entity.Codigo),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reserva_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reserva_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@id_tipo_reserva", entity.Tipo),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@fg_transmitido_siafem", entity.StatusSiafemSiafisico),
                new SqlParameter("@fg_transmitido_prodesp", entity.StatusProdesp),
                new SqlParameter("@cd_ugo", entity.Ugo),
                new SqlParameter("@dt_emissao_reservaDe", entity.DataEmissaoDe),
                new SqlParameter("@dt_emissao_reservaAte", entity.DataEmissaoAte)
            );
        }

        public int Edit(Model.Entity.Reserva.Reserva entity)
        {
            return DataHelper.Get<int>("PR_RESERVA_ALTERAR",
                new SqlParameter("@id_reserva", entity.Codigo),
                new SqlParameter("@id_fonte", entity.Fonte),
                new SqlParameter("@id_estrutura", entity.Estrutura),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@id_tipo_reserva", entity.Tipo),
                new SqlParameter("@id_regional", entity.Regional),
                new SqlParameter("@cd_contrato", entity.Contrato),
                new SqlParameter("@cd_processo", entity.Processo),
                new SqlParameter("@nr_reserva_prodesp", entity.NumProdesp),
                new SqlParameter("@nr_reserva_siafem_siafisico", entity.NumSiafemSiafisico),
                new SqlParameter("@cd_obra", entity.Obra),
                new SqlParameter("@nr_oc", entity.Oc),
                new SqlParameter("@cd_ugo", entity.Ugo),
                new SqlParameter("@cd_uo", entity.Uo),
                new SqlParameter("@cd_evento", entity.Evento),
                new SqlParameter("@nr_ano_exercicio", entity.AnoExercicio),
                new SqlParameter("@nr_ano_referencia_reserva", entity.AnoReferencia),
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
                new SqlParameter("@dt_emissao_reserva", entity.DataEmissao.ValidateDBNull()),
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
    }
}
