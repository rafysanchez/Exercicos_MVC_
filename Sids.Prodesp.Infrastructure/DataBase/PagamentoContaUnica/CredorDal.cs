using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class CredorDal: ICrudCredor
    {
        public void Save(Credor entity)
        {

             DataHelper.Get<int>("PR_CREDOR_INCLUIR",
                new SqlParameter("@nm_prefeitura", entity.Prefeitura),
                new SqlParameter("@cd_cpf_cnpj", entity.CpfCnpjUgCredor),
                new SqlParameter("@bl_conveniado", entity.Conveniado),
                new SqlParameter("@bl_base_calculo", entity.BaseCalculo),
                new SqlParameter("@nm_reduzido_credor", entity.NomeReduzidoCredor)
            );
        }

        public void Delete()
        {
            DataHelper.Get<int>("PR_CREDOR_EXCLUIR");
        }

        public IEnumerable<Credor> Fetch(Credor entity)
        {
            return DataHelper.List<Credor>("PR_CREDOR_SELECIONAR",
                new SqlParameter("@nm_prefeitura", entity.Prefeitura),
                new SqlParameter("@nm_reduzido_credor", entity.NomeReduzidoCredor)
                );
        }
    }
}
