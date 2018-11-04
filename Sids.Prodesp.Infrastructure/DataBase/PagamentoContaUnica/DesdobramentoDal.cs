using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;

namespace Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica
{
    public class DesdobramentoDal: ICrudDesdobramento
    {
        public IEnumerable<Desdobramento> FetchForGrid(Desdobramento entity, DateTime since, DateTime until)
        {
            return DataHelper.List<Desdobramento>("PR_DESDOBRAMENTO_CONSULTAR_GRID",
                new SqlParameter("@id_desdobramento", entity.Id),
                new SqlParameter("@id_tipo_desdobramento", entity.DesdobramentoTipoId),
                new SqlParameter("@id_tipo_documento", entity.DocumentoTipoId),
                new SqlParameter("@nr_documento", entity.NumeroDocumento),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
                new SqlParameter("@dt_cadastramento_de", since.ValidateDBNull()),
                new SqlParameter("@dt_cadastramento_ate", until.ValidateDBNull()),
                new SqlParameter("@id_regional", entity.RegionalId),
                new SqlParameter("@bl_situacao_desdobramento", entity.SituacaoDesdobramento));
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_DESDOBRAMENTO_EXCLUIR",
                new SqlParameter("@id_desdobramento", id));
        }

        public IEnumerable<Desdobramento> Fetch(Desdobramento entity)
        {
            return DataHelper.List<Desdobramento>("PR_DESDOBRAMENTO_CONSULTAR",
                new SqlParameter("@id_desdobramento", entity.Id),
                new SqlParameter("@id_regional", entity.RegionalId));
        }

        public int Save(Desdobramento entity)
        {

            return DataHelper.Get<int>("PR_DESDOBRAMENTO_SALVAR",
                new SqlParameter("@id_desdobramento", entity.Id),
                new SqlParameter("@id_tipo_desdobramento", entity.DesdobramentoTipoId),
                new SqlParameter("@id_tipo_documento", entity.DocumentoTipoId),
                new SqlParameter("@nr_documento", entity.NumeroDocumento),
                new SqlParameter("@nr_contrato", entity.NumeroContrato),
                new SqlParameter("@cd_servico", entity.CodigoServico),
                new SqlParameter("@ds_servico", entity.DescricaoServico),
                new SqlParameter("@ds_credor", entity.DescricaoCredor),
                new SqlParameter("@nm_reduzido_credor", entity.NomeReduzidoCredor),
                new SqlParameter("@cd_tipo_despesa", entity.TipoDespesa),
                new SqlParameter("@bl_aceitar_credor", entity.AceitaCredor),
                new SqlParameter("@nr_tipo_desdobramento", entity.NumeroDesdobramentoTipoId),
                new SqlParameter("@vr_distribuicao", entity.ValorDistribuido),
                new SqlParameter("@vr_total_issqn", entity.ValorIssqn),
                new SqlParameter("@vr_total_ir", entity.ValorIr),
                new SqlParameter("@vr_total_inss", entity.ValorInss),
                new SqlParameter("@dt_emissao", entity.DataEmissao.ValidateDBNull()),
                new SqlParameter("@cd_aplicacao_obra", entity.CodigoAplicacaoObra),
                new SqlParameter("@bl_transmitir_prodesp", entity.TransmitirProdesp),
                new SqlParameter("@bl_transmitido_prodesp", entity.TransmitidoProdesp),
                new SqlParameter("@ds_status_prodesp", entity.StatusProdesp),
                new SqlParameter("@dt_transmitido_prodesp", entity.DataTransmitidoProdesp.ValidateDBNull()),
                new SqlParameter("@ds_transmissao_mensagem_prodesp", entity.MensagemServicoProdesp),
                new SqlParameter("@ds_status_documento", entity.StatusDocumento),
                new SqlParameter("@bl_cadastro_completo", entity.CadastroCompleto),
                new SqlParameter("@bl_situacao_desdobramento", entity.SituacaoDesdobramento),
                new SqlParameter("@id_regional", entity.RegionalId));
        }

        public Desdobramento Get(int id)
        {
            return DataHelper.Get<Desdobramento>("PR_DESDOBRAMENTO_CONSULTAR",
                new SqlParameter("@id_desdobramento", id));
        }

        public int GetNumeroAgrupamento()
        {
            throw new NotImplementedException();
        }
    }
}
