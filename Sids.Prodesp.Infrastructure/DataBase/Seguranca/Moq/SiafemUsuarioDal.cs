namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca.Moq
{
    using Helpers;
    using Model.Interface.Base;
    using Model.ValueObject.Service.Moq;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SiafemUsuarioDal : ICrudBase<SiafemUsuario>
    {

        public string GetTableName()
        {
            return "tb_moq_siafem_usuario";
        }
        
        public int Add(SiafemUsuario entity)
        {
            return DataHelper.Get<int>("PR_USUARIO_SIAFEM_INCLUIR",
                new SqlParameter("@ds_login", entity.ChaveAcesso),
                new SqlParameter("@ds_senha", entity.Senha),
                new SqlParameter("@bl_senha_expirada", entity.SenhaExpirada)
            );
        }

        public int Edit(SiafemUsuario entity)
        {
            return DataHelper.Get<int>("PR_USUARIO_SIAFEM_ALTERAR",
                new SqlParameter("@id_usuario", entity.Codigo),
                new SqlParameter("@ds_login", entity.ChaveAcesso),
                new SqlParameter("@ds_senha", entity.Senha),
                new SqlParameter("@bl_senha_expirada", entity.SenhaExpirada)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_USUARIO_SIAFEM_EXCLUIR",
                new SqlParameter("@id_usuario", id)
            );
        }

        public IEnumerable<SiafemUsuario> Fetch(SiafemUsuario entity)
        {
            return DataHelper.List<SiafemUsuario>("PR_USUARIO_SIAFEM_CONSULTAR",
                new SqlParameter("@id_usuario", entity.Codigo),
                new SqlParameter("@ds_login", entity.ChaveAcesso),
                new SqlParameter("@ds_senha", entity.Senha),
                new SqlParameter("@bl_senha_expirada", entity.SenhaExpirada)
            );
        }
    }
}
