namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Entity.Empenho;
    using Model.Entity.Movimentacao;
    using Model.Interface.Empenho;
    using Model.Interface.Movimentacao;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class MovimentacaoMesDal : ICrudMovimentacaoMes
    {
        public int Edit(MovimentacaoMes entity)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_MES_ALTERAR",
                new SqlParameter("@id_empenho_mes", entity.Id),
                new SqlParameter("@tb_empenho_id_empenho", entity.Id),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public IEnumerable<MovimentacaoMes> Fetch(MovimentacaoMes entity)
        {
            return DataHelper.List<MovimentacaoMes>("PR_MOVIMENTACAO_MES_CONSULTAR",
                new SqlParameter("@id_mes", entity.Id),
                new SqlParameter("@tb_distribuicao_movimentacao_id_distribuicao_movimentacao", entity.IdDistribuicao),
                new SqlParameter("@tb_reducao_suplementacao_id_reducao_suplementacao", entity.IdReducaoSuplementacao),
                new SqlParameter("@tb_cancelamento_movimentacao_id_cancelamento_movimentacao", entity.IdCancelamento),
                new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
                new SqlParameter("@nr_agrupamento", entity.NrAgrupamento),
                new SqlParameter("@nr_seq", entity.NrSequencia),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes),
                new SqlParameter("@red_sup", entity.tab)
            );
        }


        public IEnumerable<MovimentacaoMes> FetchCancelamento(MovimentacaoMes entity)
        {
            return DataHelper.List<MovimentacaoMes>("PR_MOVIMENTACAO_CANCELAMENTO_MES_CONSULTAR",
                new SqlParameter("@id_mes", entity.Id),
                new SqlParameter("@tb_cancelamento_movimentacao_id_cancelamento_movimentacao", entity.IdCancelamento),
                new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
                new SqlParameter("@nr_agrupamento", entity.NrAgrupamento),
                new SqlParameter("@nr_seq", entity.NrSequencia),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }

        public IEnumerable<MovimentacaoMes> FetchDistribuicao(MovimentacaoMes entity)
        {
            var paramId = new SqlParameter("@id_mes", entity.Id);
            var paramIdDistribuicao = new SqlParameter("@tb_distribuicao_movimentacao_id_distribuicao_movimentacao", entity.IdDistribuicao);
            var paramIdMovimentacao = new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao);
            var paramNrAgrupamento = new SqlParameter("@nr_agrupamento", entity.NrAgrupamento);
            var paramNrSequencia = new SqlParameter("@nr_seq", entity.NrSequencia);
            var paramDescricao = new SqlParameter("@ds_mes", entity.Descricao);
            var paramValorMes = new SqlParameter("@vr_mes", entity.ValorMes);

            var dbResult = DataHelper.List<MovimentacaoMes>("PR_MOVIMENTACAO_DISTRIBUICAO_MES_CONSULTAR", paramId, paramIdDistribuicao, paramIdMovimentacao, paramNrAgrupamento,
                paramNrSequencia, paramDescricao, paramValorMes);

            return dbResult;
        }


        public IEnumerable<MovimentacaoMes> FetchReducaoSuplementacao(MovimentacaoMes entity)
        {
            return DataHelper.List<MovimentacaoMes>("PR_MOVIMENTACAO_REDUCAOSUPLEMENTACAO_MES_CONSULTAR",
                new SqlParameter("@id_mes", entity.Id),
                new SqlParameter("@tb_reducao_suplementacao_id_reducao_suplementacao", entity.IdReducaoSuplementacao),
                new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
                new SqlParameter("@nr_agrupamento", entity.NrAgrupamento),
                new SqlParameter("@nr_seq", entity.NrSequencia),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes)
            );
        }


        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_MES_EXCLUIR", 
                new SqlParameter("@id_mes", id)
            );
        }

        public int Add(MovimentacaoMes entity)
        {
            return DataHelper.Get<int>("PR_MOVIMENTACAO_MES_SALVAR",
                new SqlParameter("@id_mes", entity.Id),
                new SqlParameter("@tb_distribuicao_movimentacao_id_distribuicao_movimentacao", entity.IdDistribuicao),
                new SqlParameter("@tb_reducao_suplementacao_id_reducao_suplementacao", entity.IdReducaoSuplementacao),
                new SqlParameter("@tb_cancelamento_movimentacao_id_cancelamento_movimentacao", entity.IdCancelamento),
                new SqlParameter("@tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria", entity.IdMovimentacao),
                new SqlParameter("@nr_agrupamento", entity.NrAgrupamento),
                new SqlParameter("@nr_seq", entity.NrSequencia),
                new SqlParameter("@ds_mes", entity.Descricao),
                new SqlParameter("@vr_mes", entity.ValorMes),
                new SqlParameter("@cd_unidade_gestora", entity.UnidadeGestora)
            );
        }

        public string GetTableName()
        {
            return "tb_movimentacao_orcamentaria_mes";
        }
    }
}
