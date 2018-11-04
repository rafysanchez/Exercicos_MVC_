using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoEvento
    {


        [Column("id_evento")]
        public int Id { get; set; }


        [Column("cd_evento")]
        public string CodigoEvento { get; set; }

        [Column("tb_cancelamento_movimentacao_id_cancelamento_movimentacao")]
        public int IdCancelamento { get; set; }

        [Column("tb_distribuicao_movimentacao_id_distribuicao_movimentacao")]
        public int IdDistribuicao { get; set; }


        [Column("tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria")]
        public int IdMovimentacao { get; set; }

        [Column("nr_agrupamento")]
        public int NrAgrupamento { get; set; }


        [Column("nr_seq")]
        public int NrSequencia { get; set; }

        [Column("cd_inscricao_evento")]
        public int InscricaoEvento { get; set; }

        [Column("cd_classificacao")]
        public int Classificacao { get; set; }

        [Column("cd_fonte")]
        public int Fonte { get; set; }

        [Column("rec_despesa")]
        public int RecDesp { get; set; }

        [Column("vr_evento")]
        public int ValorEvento { get; set; }


    }
}
