using Sids.Prodesp.Model.Interface.Movimentacao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoCancelamento : MovimentacaoItemBase
    {
        [Column("id_cancelamento_movimentacao")]
        public int Id { get; set; }

        [Column("tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria")]
        public int IdMovimentacao { get; set; }

        [Column("tb_fonte_id_fonte")]
        public int IdFonte { get; set; }

        [Column("nr_agrupamento")]
        public int NrAgrupamento { get; set; }

        [Column("nr_seq")]
        public int NrSequencia { get; set; }

        // NumeroSiafem na classe base

        [Column("valor")]
        public decimal Valor { get; set; }

        [Column("cd_unidade_gestora")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Column("cd_gestao_favorecido")]
        public string GestaoFavorecida { get; set; }

        [Column("evento")]
        public string Evento { get; set; }

        [Column("nr_categoria_gasto")]
        public string CategoriaGasto { get; set; }

        [Column("eventoNC")]
        public string EventoNC { get; set; }

        [Column("ds_observacao")]
        public string Observacao { get; set; }

        [Column("ds_observacao2")]
        public string Observacao2 { get; set; }

        [Column("ds_observacao3")]
        public string Observacao3 { get; set; }

        public int IdTipoDocumento { get; set; }
    }
}
