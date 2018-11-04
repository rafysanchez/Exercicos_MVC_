using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    [Table("tb_impressao_relacao_re_rt")]
    public class ImpressaoRelacaoRERT : Base.PagamentoContaUnica
    {
        [Key]
        [Column("id_impressao_relacao_re_rt")]
        public override int Id { get; set; }

        [Column("cd_relatorio")]
        public string CodigoRelatorio { get; set; }

        [Column("cd_relob")]
        public string CodigoRelacaoRERT { get; set; }

        [Column("nr_ob")]
        public string CodigoOB { get; set; }

        [Display(Name = "Nº Agrupamento")]
        [Column("nr_agrupamento")]
        public int? NumeroAgrupamento { get; set; }

        [Display(Name = "Unidade Gestora")]
        [Column("cd_unidade_gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Column("ds_nome_unidade_gestora")]
        public string NomeUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        [Column("cd_gestao")]
        public string CodigoGestao { get; set; }

        [Column("ds_nome_gestao")]
        public string NomeGestao { get; set; }
        
        [Display(Name = "Banco")]
        [Column("cd_banco")]
        public string CodigoBanco { get; set; }

        [Column("ds_nome_banco")]
        public string NomeBanco { get; set; }

        [Column("ds_texto_autorizacao")]
        public string TextoAutorizacao { get; set; }

        [Column("ds_cidade")]
        public string Cidade { get; set; }

        [Column("ds_nome_gestor_financeiro")]
        public string NomeGestorFinanceiro { get; set; }

        [Column("ds_nome_ordenador_assinatura")]
        public string NomeOrdenadorAssinatura { get; set; }

        [Display(Name = "Data de Solicitação")]
        [Column("dt_solicitacao")]
        public DateTime DataSolicitacao { get; set; }

        [Column("dt_referencia")]
        public DateTime DataReferencia { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Column("dt_cadastramento")]
        public DateTime DataCadastramento { get; set; }

        [Display(Name = "Data de Emissão")]
        [Column("dt_emissao")]
        public DateTime DataEmissao { get; set; }

        [Column("vl_total_documento")]
        public decimal ValorTotalDocumento { get; set; }

        [Column("vl_extenso")]
        public string ValorExtenso { get; set; }

        [Column("fg_transmitido_siafem")]
        public bool FlagTransmitidoSiafem { get; set; }

        [Column("fg_transmitir_siafem")]
        public bool FlagTransmitirSiafem { get; set; }

        [Display(Name = "Data de Transmissão")]
        [Column("dt_transmitido_siafem")]
        public DateTime DataTransmissaoSiafem { get; set; }

        [Display(Name = "Status SIAFEM")]
        [Column("ds_status_siafem")]
        public string StatusSiafem { get; set; }

        [Column("ds_msgRetornoTransmissaoSiafem")]
        public string MsgRetornoTransmissaoSiafem { get; set; }

        [Column("fg_cancelamento_relacao_re_rt")]
        public bool? FlagCancelamentoRERT { get; set; }

        [Column("nr_agencia")]
        public string Agencia { get; set; }

        [Column("ds_nome_agencia")]
        public string NomeAgencia { get; set; }

        [Column("nr_conta_c")]
        public string NumeroConta { get; set; }
    }
}
