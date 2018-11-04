namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class OrigemMaterialDal : ICrudOrigemMaterial
    {
        public IEnumerable<OrigemMaterial> Fetch(OrigemMaterial entity)
        {
            return DataHelper.List<OrigemMaterial>("PR_ORIGEM_MATERIAL_CONSULTAR",
                new SqlParameter("@id_origem_material", entity.Id),
                new SqlParameter("@ds_origem_material", entity.Descricao)
            );
        }
    }
}
