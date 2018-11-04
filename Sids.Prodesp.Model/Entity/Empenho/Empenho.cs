namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Empenho : BaseEmpenho
    {
        [Display(Name = "Tipo de Empenho")]
        [Column("tb_empenho_tipo_id_empenho_tipo")]
        public int EmpenhoTipoId { get; set; }

        [Display(Name = "UG Obra")]
        [Column("cd_ugo_obra")]
        public int CodigoUGObra { get; set; }

        [Display(Name = "Ano de Assinatura do Contrato")]
        [Column("nr_ano_contrato")]
        public int NumeroAnoContrato { get; set; }

        [Display(Name = "Mês de Assinatura do Contrato")]
        [Column("nr_mes_contrato")]
        public int NumeroMesContrato { get; set; }

        [Display(Name = "Nº da Obra")]
        [Column("nr_obra")]
        public string NumeroObra { get; set; }

        [Display(Name = "Data de Entrega")]
        [Column("dt_entrega_material")]
        public DateTime DataEntregaMaterial { get; set; }

        [Display(Name = "Logradouro")]
        [Column("ds_logradouro_entrega")]
        public string DescricaoLogradouroEntrega { get; set; }

        [Display(Name = "Bairro")]
        [Column("ds_bairro_entrega")]
        public string DescricaoBairroEntrega { get; set; }

        [Display(Name = "Cidade")]
        [Column("ds_cidade_entrega")]
        public string DescricaoCidadeEntrega { get; set; }

        [Display(Name = "CEP")]
        [Column("cd_cep_entrega")]
        public string NumeroCEPEntrega { get; set; }

        [Display(Name = "Informações Adicoinais")]
        [Column("ds_informacoes_adicionais_entrega")]
        public string DescricaoInformacoesAdicionaisEntrega { get; set; }

        [Display(Name = "OC")]
        [Column("nr_oc")]
        public string NumeroOC { get; set; }

        [Display(Name = "UGO")]
        [Column("cd_ugo")]
        public int CodigoUGO { get; set; }

        
        [Display(Name = "Nº do Contrato (fornec.)")]
        [Column("nr_contrato_fornecedor")]
        public string NumeroContratoFornecedor { get; set; }

        [Display(Name = "Número do Edital")]
        [Column("nr_edital")]
        public string NumeroEdital { get; set; }

        [Display(Name = "Referência Legal")]
        [Column("ds_referencia_legal")]
        public string DescricaoReferenciaLegal { get; set; }
    }
}
