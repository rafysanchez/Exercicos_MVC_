namespace Sids.Prodesp.Infrastructure.DataBase.Configuracao
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Interface.Configuracao;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class FonteDal :  ICrudFonte
    {
        public string GetTableName()
        {
            return "tb_fonte";
        }

        public IEnumerable<Fonte> Fetch(Fonte entity)
        {
            return DataHelper.List<Fonte>("PR_FONTE_CONSULTAR",
                new SqlParameter("@id_fonte", entity.Id),
                new SqlParameter("@cd_fonte", entity.Codigo),
                new SqlParameter("@ds_fonte", entity.Descricao)
            );
        }

        public int Add(Fonte entity)
        {
            return DataHelper.Get<int>("PR_FONTE_INCLUIR",
                new SqlParameter("@cd_fonte", entity.Codigo),
                new SqlParameter("@ds_fonte", entity.Descricao)
            );
        }
     
        public int Edit(Fonte entity)
        {
            return DataHelper.Get<int>("PR_FONTE_ALTERAR",
                new SqlParameter("@id_fonte", entity.Id),
                new SqlParameter("@cd_fonte", entity.Codigo),
                new SqlParameter("@ds_fonte", entity.Descricao)                
             );
         }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_FONTE_EXCLUIR",
                new SqlParameter("@id_fonte", id)
            );         
        }


        public IEnumerable<Fonte> DuplicateCheck(Fonte entity)
        {
            return DataHelper.List<Fonte>("PR_FONTE_ALTERAR_DUPLICADO",
                new SqlParameter("@id_fonte", entity.Id),
                new SqlParameter("@cd_fonte", entity.Codigo),
                new SqlParameter("@ds_fonte", entity.Descricao)
            );
        }
    }
}
