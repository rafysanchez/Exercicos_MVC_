using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class IdentificacaoDesdobramentoDal: ICrudIdentificacaoDesdobramento
    {
        public IEnumerable<IdentificacaoDesdobramento> Fetch(IdentificacaoDesdobramento entity)
        {
            return DataHelper.List<IdentificacaoDesdobramento>("PR_IDENTIFICACAO_DESDOBRAMENTO_CONSULTAR",
                new SqlParameter("@id_identificacao_desdobramento", entity.Id),
                new SqlParameter("@id_desdobramento", entity.Desdobramento));
        }

        public int Remove(IdentificacaoDesdobramento entity)
        {
            return DataHelper.Get<int>("PR_IDENTIFICACAO_DESDOBRAMENTO_EXCLUIR",
                new SqlParameter("@id_identificacao_desdobramento", entity.Id),
                new SqlParameter("@id_desdobramento", entity.Desdobramento));
        }

        public int Save(IdentificacaoDesdobramento entity)
        {
            return DataHelper.Get<int>("PR_IDENTIFICACAO_DESDOBRAMENTO_SALVAR",
                new SqlParameter("@id_desdobramento", entity.Desdobramento),
                new SqlParameter("@id_identificacao_desdobramento", entity.Id),
                new SqlParameter("@id_tipo_desdobramento", entity.DesdobramentoTipoId),
                new SqlParameter("@id_reter", entity.ReterId),
                new SqlParameter("@ds_nome_reduzido_credor", entity.NomeReduzidoCredor),
                new SqlParameter("@vr_percentual_base_calculo", entity.ValorPercentual),
                new SqlParameter("@vr_desdobrado", entity.ValorDesdobrado),
                new SqlParameter("@vr_desdobrado_inicial",entity.ValorDesdobradoInicial),
                new SqlParameter("@vr_distribuicao", entity.ValorDistribuicao),
                new SqlParameter("@bl_tipo_bloqueio", entity.TipoBloqueio),
                new SqlParameter("@nr_sequencia", entity.Sequencia)
                
                );
        }

        public IdentificacaoDesdobramento Get(int id)
        {
            return DataHelper.Get<IdentificacaoDesdobramento>("PR_IDENTIFICACAO_DESDOBRAMENTO_CONSULTAR",
                new SqlParameter("@id_identificacao_desdobramento", id));
        }
    }
}
