using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoNotaDeCredito : MovimentacaoItemBase
    {
        [Column("id_nota_credito")]
        public int Id { get; set; }

        [Column("tb_programa_id_programa")]
        public int IdPrograma { get; set; }

        [Column("tb_fonte_id_fonte")]
        public int IdFonte { get; set; }

        [Column("tb_estrutura_id_estrutura")]
        public int IdEstrutura { get; set; }

        [Column("tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria")]
        public int IdMovimentacao { get; set; }

        [Column("nr_agrupamento")]
        public int NrAgrupamento { get; set; }

        [Column("nr_seq")]
        public int NrSequencia { get; set; }

        [Column("cd_candis")]
        public string CanDis { get; set; }

        // Numero Siafem na classe base

        [Column("vr_credito")]
        public decimal Valor { get; set; }

        [Column("cd_unidade_gestora_emitente")]
        public string UnidadeGestoraEmitente { get; set; }

        [Column("cd_unidade_gestora_favorecido")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Column("cd_uo")]
        public string Uo { get; set; }

        [Column("plano_interno")]
        public string PlanoInterno { get; set; }

        [Column("eventoNC")]
        public string EventoNC { get; set; }

        [Column("cd_gestao_favorecido")]
        public string GestaoFavorecida { get; set; }

        [Column("cd_ugo")]
        public string Ugo { get; set; }

        [Column("fonte_recurso")]
        public string FonteRecurso { get; set; }

        [Column("ds_observacao")]
        public string Observacao { get; set; }

        [Column("ds_observacao2")]
        public string Observacao2 { get; set; }

        [Column("ds_observacao3")]
        public string Observacao3 { get; set; }

        public DateTime DataCadastro { get; set; }
        public string GestaoEmitente { get; set; }
    }
}
