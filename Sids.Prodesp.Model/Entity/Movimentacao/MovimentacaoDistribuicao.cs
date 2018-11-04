using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoDistribuicao : MovimentacaoItemBase
    {


        [Column("id_distribuicao_movimentacao")]
        public int Id { get; set; }

        [Column("tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria")]
        public int IdMovimentacao { get; set; }


        [Column("nr_agrupamento")]
        public int NrAgrupamento { get; set; }


        [Column("nr_seq")]
        public int NrSequencia { get; set; }


        [Column("tb_fonte_id_fonte")]
        public string IdFonte { get; set; }        

        [Column("cd_unidade_gestora_favorecido")]
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

        [Column("valor")]
        public decimal Valor { get; set; }
        public int IdTipoDocumento { get; set; }
    }
}
