namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class UsuarioDal : Base.BaseDal, ICrudUsuario
    {
        public string GetTableName()
        {
            return "tb_usuario";
        }

        public int Add(Usuario entity)
        {
            return DataHelper.Get<int>("PR_USUARIO_INCLUIR",
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@id_sistema", entity.SistemaId),
                new SqlParameter("@id_area", entity.AreaId),
                new SqlParameter("@ds_email", entity.Email),
                new SqlParameter("@ds_login", entity.ChaveDeAcesso),
                new SqlParameter("@ds_senha", entity.Senha),
                new SqlParameter("@ds_senha_siafem", string.Empty),
                new SqlParameter("@bl_senha_expirada", entity.SenhaExpirada),
                new SqlParameter("@dt_expiracao_senha", entity.DataExpiracaoSenha.ValidateDBNull()),
                new SqlParameter("@dt_ultimo_acesso", entity.DataUltimoAcesso.ValidateDBNull()),
                new SqlParameter("@nr_tentativa_login_invalidas", entity.TentativasLoginInvalidas),
                new SqlParameter("@bl_bloqueado", entity.Bloqueado),
                new SqlParameter("@bl_alterar_senha", entity.AlterarSenha),
                new SqlParameter("@ds_token", entity.Token),
                new SqlParameter("@ds_nome", entity.Nome),
                new SqlParameter("@ds_cpf", entity.CPF),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@dt_criacao", entity.DataCriacao.ValidateDBNull()),
                new SqlParameter("@id_usuario_criacao", entity.UsuarioCriacao),
                new SqlParameter("@cd_impressora132", entity.Impressora132),
                new SqlParameter("@cd_impressora80", entity.Impressora80),
                 new SqlParameter("@bl_acesso_siafem", entity.AcessaSiafem)
            );
        }

        public int Edit(Usuario entity)
        {
            return DataHelper.Get<int>("PR_USUARIO_ALTERAR",
                new SqlParameter("@id_usuario", entity.Codigo),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@id_sistema", entity.SistemaId),
                new SqlParameter("@id_area", entity.AreaId),
                new SqlParameter("@ds_email", entity.Email),
                new SqlParameter("@ds_login", entity.ChaveDeAcesso),
                new SqlParameter("@ds_senha", entity.Senha),
                new SqlParameter("@ds_senha_siafem", entity.SenhaSiafem),
                new SqlParameter("@bl_senha_expirada", entity.SenhaExpirada),
                new SqlParameter("@dt_expiracao_senha", entity.DataExpiracaoSenha.ValidateDBNull()),
                new SqlParameter("@dt_ultimo_acesso", entity.DataUltimoAcesso.ValidateDBNull()),
                new SqlParameter("@nr_tentativa_login_invalidas", entity.TentativasLoginInvalidas),
                new SqlParameter("@bl_bloqueado", entity.Bloqueado),
                new SqlParameter("@bl_alterar_senha", entity.AlterarSenha),
                new SqlParameter("@ds_token", entity.Token),
                new SqlParameter("@ds_nome", entity.Nome),
                new SqlParameter("@ds_cpf", entity.CPF),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@bl_acessa_siafem", entity.AcessaSiafem),
                new SqlParameter("@bl_senha_siafem_expirada", entity.SenhaSiafemExpirada),
                new SqlParameter("@bl_alterar_senha_siafem", entity.AlterarSenhaSiafem),
                new SqlParameter("@cd_impressora132", entity.Impressora132),
                new SqlParameter("@cd_impressora80", entity.Impressora80));
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_USUARIO_EXCLUIR",
                new SqlParameter("@id_usuario", id)
            );
        }

        public IEnumerable<Usuario> Fetch(Usuario entity)
        {
            return DataHelper.List<Usuario>("PR_USUARIO_CONSULTAR",
                new SqlParameter("@id_usuario", entity.Codigo),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@id_sistema", entity.SistemaId),
                new SqlParameter("@id_area", entity.AreaId),
                new SqlParameter("@ds_email", entity.Email),
                new SqlParameter("@ds_login", entity.ChaveDeAcesso),
                new SqlParameter("@ds_senha", entity.Senha),
                new SqlParameter("@ds_senha_siafem", entity.SenhaSiafem),
                new SqlParameter("@bl_senha_expirada", entity.SenhaExpirada),
                new SqlParameter("@dt_expiracao_senha", entity.DataExpiracaoSenha.ValidateDBNull()),
                new SqlParameter("@dt_ultimo_acesso", entity.DataUltimoAcesso.ValidateDBNull()),
                new SqlParameter("@bl_bloqueado", entity.Bloqueado),
                new SqlParameter("@bl_alterar_senha", entity.AlterarSenha),
                new SqlParameter("@ds_token", entity.Token),
                new SqlParameter("@ds_nome", entity.Nome),
                new SqlParameter("@ds_cpf", entity.CPF),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int UpdateADUser(Usuario entity)
        {
            entity.DataExpiracaoSenha = DateTime.Today.AddDays(Convert.ToInt32(AppConfig.DiasExpiracaoSenha));
            entity.SenhaExpirada = false;
            entity.AlterarSenha = false;
            entity.Token = string.Empty;

            return Edit(entity);
        }

        public int UpdateLastAccess(Usuario entity)
        {
            return DataHelper.Get<int>("PR_USUARIO_ALTERAR_DATA_ULTIMO_ACESSO",
                new SqlParameter("@id_usuario", entity.Codigo),
                new SqlParameter("@dt_ultimo_acesso", DateTime.Now)
            );
        }

        /// <summary>
        /// Altera a data de expiração de senha de um Usuário.
        /// </summary>
        /// <param name="entity"> Usuario Model.</param>
        /// <returns> Retorna 0 em caso de sucesso, 1 caso contrário.</returns>
        public int UpdatePasswordExpirationDate(Usuario entity)
        {
            return DataHelper.Get<int>("SPO_USUARIO_ALTERAR_DATA_EXPIRACAO_SENHA",
                new SqlParameter("@ds_login", entity.ChaveDeAcesso),
                new SqlParameter("@dt_expiracao_senha", entity.DataExpiracaoSenha)
            );
        }
    }
}
