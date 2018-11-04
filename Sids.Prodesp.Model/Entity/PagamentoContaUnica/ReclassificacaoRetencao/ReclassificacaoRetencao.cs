using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao
{
    public class ReclassificacaoRetencao : Base.PagamentoContaUnica, IPagamentoContaUnicaSiafem
    {
        public ReclassificacaoRetencao()
        {
            this.Notas = new List<ReclassificacaoRetencaoNota>();
            this.Eventos = new List<ReclassificacaoRetencaoEvento>();
        }

        [Key]
        [Column("id_reclassificacao_retencao")]
        public override int Id { get; set; }

        [Column("nr_siafem_siafisico")]
        public string NumeroSiafem { get; set; }

        [Column("fl_sistema_siafem_siafisico")]
        public bool TransmitirSiafem { get; set; }
        [Column("fl_transmissao_transmitido_siafem_siafisico")]
        public bool TransmitidoSiafem { get; set; }
        [Column("dt_transmissao_transmitido_siafem_siafisico")]
        public DateTime DataTransmitidoSiafem { get; set; }

        [Column("ds_transmissao_mensagem_siafem_siafisico")]
        public string MensagemServicoSiafem { get; set; }

        [Column("cd_transmissao_status_siafem_siafisico")]
        public string StatusSiafem { get; set; }

        [Column("id_confirmacao_pagamento")]
        public int id_confirmacao_pagamento { get; set; }


        [Column("cd_credor_organizacao")]
        public int CodigoCredorOrganizacaoId { get; set; }

        [Column("nr_cnpj_cpf_fornecedor")]
        public string NumeroCNPJCPFFornecedorId { get; set; }

        [Column("nr_empenho_siafem_siafisico")]
        public string NumeroOriginalSiafemSiafisico { get; set; }

        [Column("cd_unidade_gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Column("cd_gestao")]
        public string CodigoGestao { get; set; }

        [Column("vl_valor")]
        public decimal Valor { get; set; }

        [Column("cd_evento")]
        public string CodigoEvento { get; set; }

        [Column("ds_inscricao")]
        public string CodigoInscricao { get; set; }

        [Column("cd_classificacao")]
        public string CodigoClassificacao { get; set; }

        [Column("cd_fonte")]
        public string CodigoFonte { get; set; }

        [Column("nr_cnpj_cpf_credor")]
        public string NumeroCNPJCPFCredor { get; set; }

        [Column("cd_gestao_credor")]
        public string CodigoGestaoCredor { get; set; }

        [Column("nr_ano_medicao")]
        public string AnoMedicao { get; set; }

        [Column("nr_mes_medicao")]
        public string MesMedicao { get; set; }

        [Column("ds_observacao_1")]
        public string DescricaoObservacao1 { get; set; }

        [Column("ds_observacao_2")]
        public string DescricaoObservacao2 { get; set; }

        [Column("ds_observacao_3")]
        public string DescricaoObservacao3 { get; set; }

        [Column("ds_normal_estorno")]
        public string NormalEstorno { get; set; }

        [Column("nr_cnpj_prefeitura")]
        public string NumeroCnpjPrefeitura { get; set; }

        [Column("nr_nota_lancamento_medicao")]
        public string NotaLancamenoMedicao { get; set; }

        [Column("id_resto_pagar")]
        public string RestoPagarId { get; set; }

        [Column("id_tipo_reclassificacao_retencao")]
        public int ReclassificacaoRetencaoTipoId { get; set; }

        public virtual IEnumerable<ReclassificacaoRetencaoEvento> Eventos { get; set; }
        public virtual IEnumerable<ReclassificacaoRetencaoNota> Notas { get; set; }

        [Column("nr_ano_exercicio")]
        public int? AnoExercicio { get; set; }

        [Column("nr_processo")]
        public string NumeroProcesso { get; set; }



        #region Confirmação de Pagamento
        [Column("cd_origem")]
        public OrigemReclassificacaoRetencao? Origem { get; set; }

        [Column("cd_agrupamento_confirmacao")]
        public int? AgrupamentoConfirmacao { get; set; }
        #endregion
        [Column("cd_aplicacao_obra")]
        public string CodigoAplicacaoObra { get; set; }
        [Column("bl_cadastro_completo")]
        public bool CadastroCompleto { get; set; }

        public List<ConfirmacaoPagamentoItem> itensPertenceNl { get; set; } = new List<ConfirmacaoPagamentoItem>();
    }


}
