using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Interface.Configuracao;

namespace Sids.Prodesp.Infrastructure.DataBase.Configuracao
{
    public class MunicipioDal: ICrudMunicipio
    {
        public string GetTableName()
        {
            return "tb_destino";
        }

        public int Add(Municipio entity)
        {
            const string sql = "PR_MUNICIPIO_INSERIR";
            return DataHelper.Get<int>(sql,
                new SqlParameter("@cd_municipio", entity.Codigo),
                new SqlParameter("@ds_municipio", entity.Descricao)
                );
        }

        public int Edit(Municipio entity)
        {
            const string sql = "PR_MUNICIPIO_ALTERAR";
            return DataHelper.Get<int>(sql,
                new SqlParameter("@cd_municipio", entity.Codigo),
                new SqlParameter("@ds_municipio", entity.Descricao)
                );
        }

        public int Remove(int id)
        {
            const string sql = "PR_DESTINO_EXCLUIR";
            return DataHelper.Get<int>(sql,
                new SqlParameter("@cd_municipio", id)
            );
        }

        public IEnumerable<Municipio> Fetch(Municipio entity)
        {
            const string proc = "PR_MUNICIPIO_CONSULTAR";
            var destino = DataHelper.List<Municipio>(proc,
                new SqlParameter("@cd_municipio", entity.Codigo),
                new SqlParameter("@ds_municipio", entity.Descricao)
            );
            return destino;
        }
    }
}
