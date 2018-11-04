using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoMes : IMes
    {

        [Column("id_mes")]
        public int Id { get; set; }

        [Column("tb_distribuicao_movimentacao_id_distribuicao_movimentacao")]
        public int IdDistribuicao { get; set; }

        [Column("tb_reducao_suplementacao_id_reducao_suplementacao")]
        public int IdReducaoSuplementacao { get; set; }

        [Column("tb_cancelamento_movimentacao_id_cancelamento_movimentacao")]
        public int IdCancelamento { get; set; }

        [Column("tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria")]
        public int IdMovimentacao { get; set; }

        [Column("nr_agrupamento")]
        public int NrAgrupamento { get; set; }

        [Column("ds_mes")]
        public string Descricao { get; set; }

        [Column("nr_seq")]
        public int NrSequencia { get; set; }

        [Column("vr_mes")]
        public decimal ValorMes { get; set; }

        [Column("cd_unidade_gestora")]
        public string UnidadeGestora { get; set; }



        public string UnidadeGestoraFavorecida { get; set; }

        public int Codigo { get; set; }

        public string tab { get; set; }

    }
}
