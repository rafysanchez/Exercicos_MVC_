using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class ListaBoletosDal: ICrudListaBoletos
    {
        public IEnumerable<ListaBoletos> FetchForGrid(ListaBoletos entity, DateTime since, DateTime until)
        {

            return DataHelper.List<ListaBoletos>("PR_LISTA_DE_BOLETOS_CONSULTA_GRID",
                new SqlParameter("@id_lista_de_boletos", entity.Id),
                new SqlParameter("@nr_siafem_siafisico", entity.NumeroSiafem),
                new SqlParameter("@cd_gestao", entity.CodigoGestao),
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@ds_nome_da_lista", entity.NomeLista),
                new SqlParameter("@nr_cnpj_favorecido", entity.NumeroCnpjcpfFavorecido),
                new SqlParameter("@id_tipo_documento", entity.DocumentoTipoId),
                new SqlParameter("@nr_documento", entity.NumeroDocumento),
                new SqlParameter("@cd_barras", entity.CodigoDeBarras),
                new SqlParameter("@cd_transmissao_status_siafem_siafisico", entity.StatusSiafem),
                new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull()),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@id_tipo_de_boleto", entity.TipoBoletoId));
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_LISTA_DE_BOLETOS_EXCLUIR",
                new SqlParameter("@id_lista_de_boletos", id));
        }

        public IEnumerable<ListaBoletos> Fetch(ListaBoletos entity)
        {
            return DataHelper.List<ListaBoletos>("PR_LISTA_DE_BOLETOS_CONSULTAR",
                new SqlParameter("@id_lista_de_boletos", entity.Id),
                new SqlParameter("@id_regional", entity.RegionalId));
        }

        public int Save(ListaBoletos entity)
        {
            var sqlParameterList = DataHelper.GetSqlParameterList(entity);

            return DataHelper.Get<int>("PR_LISTA_DE_BOLETOS_SALVAR", sqlParameterList);
        }

        public ListaBoletos Get(int id)
        {
            return DataHelper.Get<ListaBoletos>("PR_LISTA_DE_BOLETOS_CONSULTAR",
                new SqlParameter("@id_lista_de_boletos", id));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }
    }
}
