namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Exceptions;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PerfilDal : BaseDal, ICrudPerfil
    {
        public string GetTableName()
        {
            return "tb_perfil";
        }

        public int Add(Perfil entity)
        {
            try
            {
                entity.Codigo = DataHelper.Get<int>("PR_PERFIL_INCLUIR",
                    new SqlParameter("@ds_perfil", entity.Descricao),
                    new SqlParameter("@ds_detalhe", entity.Detalhe),
                    new SqlParameter("@bl_ativo", entity.Status),
                    new SqlParameter("@dt_criacao", entity.DataCriacao)
                );

                return entity.Codigo;
            }
            catch
            {
                throw new SidsException("Erro ao Salvar Perfil!");
            }
        }
        public int Edit(Perfil entity)
        {
            try
            {
                return DataHelper.Get<int>("PR_PERFIL_ALTERAR",
                    new SqlParameter("@id_perfil", entity.Codigo),
                    new SqlParameter("@ds_perfil", entity.Descricao),
                    new SqlParameter("@ds_detalhe", entity.Detalhe),
                    new SqlParameter("@bl_ativo", entity.Status)
                );
            }
            catch
            {
                throw new SidsException("Erro ao Salvar Perfil!");
            }
        }
        public int Remove(int id)
        {
            try
            {
                return DataHelper.Get<int>("PR_PERFIL_EXCLUIR", 
                    new SqlParameter("@id_perfil", id)
                );
            }
            catch
            {
                throw new SidsException("Erro ao Excuir Perfil!");
            }
        }
        public IEnumerable<Perfil> Fetch(Perfil entity)
        {
            return DataHelper.List<Perfil>("PR_PERFIL_CONSULTAR",
                new SqlParameter("@id_perfil", entity.Codigo),
                new SqlParameter("@ds_perfil", entity.Descricao ?? string.Empty),
                new SqlParameter("@ds_detalhe", entity.Detalhe ?? string.Empty),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }
        public IEnumerable<Perfil> FetchByUser(Usuario user)
        {
            return DataHelper.List<Perfil>("PR_GET_PERFIL_POR_USUARIO",
                new SqlParameter("@id_usuario", user.Codigo)
            );
        }

        public int GetUserCountByProfileId(int id)
        {
            return DataHelper.Get<int>("PR_GET_USUARIOS_POR_PERFIL", 
                new SqlParameter("@id_perfil", id)
            );
        }
    }
}
