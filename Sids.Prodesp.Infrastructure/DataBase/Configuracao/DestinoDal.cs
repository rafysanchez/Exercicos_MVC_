namespace Sids.Prodesp.Infrastructure.DataBase.Configuracao
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Interface.Configuracao;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class DestinoDal : ICrudDestino
    {
        public string GetTableName()
        {
            return "tb_destino";
        }

        public IEnumerable<Destino> Fetch(Destino entity)
        {
            return DataHelper.List<Destino>("PR_DESTINO_CONSULTAR",
                new SqlParameter("@id_destino", entity.Id),
                new SqlParameter("@cd_destino", entity.Codigo),
                new SqlParameter("@ds_destino", entity.Descricao)
            );
        }


        public int Add(Destino entity)
        {
            return DataHelper.Get<int>("PR_DESTINO_INCLUIR",
                new SqlParameter("@cd_destino", entity.Codigo),
                new SqlParameter("@ds_destino", entity.Descricao)
                );
        }

        public int Edit(Destino entity)
        {
            return DataHelper.Get<int>("PR_DESTINO_ALTERAR",
                new SqlParameter("@id_destino", entity.Id),
                new SqlParameter("@cd_destino", entity.Codigo),
                new SqlParameter("@ds_destino", entity.Descricao)
             );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_DESTINO_EXCLUIR",
                new SqlParameter("@id_destino", id)
            );
        }
    }
}