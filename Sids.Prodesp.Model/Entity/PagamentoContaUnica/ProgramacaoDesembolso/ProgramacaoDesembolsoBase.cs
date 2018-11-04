using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso
{
    public class ProgramacaoDesembolsoBase : Base.PagamentoContaUnica, IProgramacaoDesembolso
    {
        public ProgramacaoDesembolsoBase()
        {
            DataEmissao = DateTime.Now;
        }

        [Column("id_tipo_programacao_desembolso")]
        public int ProgramacaoDesembolsoTipoId { get; set; }
        
        [Column("nr_processo")]
        public string NumeroProcesso { get; set; }

        [Column("cd_unidade_gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Column("cd_gestao")]
        public string CodigoGestao { get; set; }

        [Column("vl_total")]
        public virtual decimal Valor { get; set; }

        [Column("nr_lista_anexo")]
        public string NumeroListaAnexo { get; set; }

        [Column("nr_nl_referencia")]
        public string NumeroNLReferencia { get; set; }

        [Column("ds_finalidade")]
        public string Finalidade { get; set; }

        [Column("cd_despesa")]
        public string CodigoDespesa { get; set; }

        [Display(Name = "Data de Vencimento")]
        [Column("dt_vencimento")]
        public DateTime DataVencimento { get; set; }

        [Display(Name = "CPF / CNPJ Credor")]
        [Column("nr_cnpj_cpf_credor")]
        public string NumeroCnpjcpfCredor { get; set; }

        [Column("cd_gestao_credor")]
        public string GestaoCredor { get; set; }

        [Column("nr_banco_credor")]
        public string NumeroBancoCredor { get; set; }

        [Column("nr_agencia_credor")]
        public string NumeroAgenciaCredor { get; set; }

        [Column("nr_conta_credor")]
        public string NumeroContaCredor { get; set; }

        [Column("nr_cnpj_cpf_pgto")]
        public string NumeroCnpjcpfPagto { get; set; }

        [Column("cd_gestao_pgto")]
        public string GestaoPagto { get; set; }

        [Column("nr_banco_pgto")]
        public string NumeroBancoPagto { get; set; }

        [Column("nr_agencia_pgto")]
        public string NumeroAgenciaPagto { get; set; }

        [Column("nr_conta_pgto")]
        public string NumeroContaPagto { get; set; }
        
        [Display(Name = "Nº da Programação de Desembolso")]
        [Column("nr_siafem_siafisico")]
        public string NumeroSiafem { get; set; }

        [Column("bl_bloqueio")]
        public bool Bloqueio { get; set; }

        [Column("bl_cancelado")]
        public bool Cancelado { get; set; }

        [Column("fl_sistema_siafem_siafisico")]
        public bool TransmitirSiafem { get; set; }

        [Column("fl_transmissao_transmitido_siafem_siafisico")]
        public bool TransmitidoSiafem { get; set; }
        [Column("dt_transmissao_transmitido_siafem_siafisico")]
        public DateTime DataTransmitidoSiafem { get; set; }

        [Column("ds_transmissao_mensagem_siafem_siafisico")]
        public virtual string MensagemServicoSiafem { get; set; }

        [Column("cd_transmissao_status_siafem_siafisico")]
        public string StatusSiafem { get; set; }
        
        [Column("nr_agrupamento")]
        public int NumeroAgrupamento { get; set; }

        [Display(Name = "Nº do Docto. Gerador")]
        [Column("nr_documento_gerador")]
        public string NumeroDocumentoGerador { get; set; }

        [Column("ds_causa_cancelamento")]
        public string CausaCancelamento { get; set; }
        
        [Column("obs")]
        public string Obs { get; set; }

        [Column("nr_ct")]
        public string NumeroCT { get; set; }

        [Column("nr_ne")]
        public string NumeroNE { get; set; }

        [Column("rec_despesa")]
        public string RecDespesa { get; set; }
        
        [NotMapped]
        public int TipoBloqueio { get; set; }

        [NotMapped]
        public bool? Bloquear { get; set; }

        [Column("nr_op")]
        public string OP { get; set; }

        [NotMapped]
        public string numNLRef { get; set; }
        
        public string ProdespConsultaOPMensagemErro { get; set; }

        [Column("id_execucao_pd")]
        public int IdExecucaoPD { get; set; }

        [Column("id_autorizacao_ob")]
        public int IdAutorizacaoOB { get; set; }

        [Column("ds_transmissao_mensagem_prodesp")]
        public string MensagemConfirmacaoPagtoProdesp { get; set; }
        [Column("cd_transmissao_status_prodesp")]
        public string StatusConfirmacaoPagtoProdesp { get; set; }
    }
}
