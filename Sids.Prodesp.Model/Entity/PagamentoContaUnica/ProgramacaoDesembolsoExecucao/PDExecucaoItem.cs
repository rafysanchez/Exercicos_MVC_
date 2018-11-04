using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao
{
    public class PDExecucaoItem : IPagamentoContaUnica
    {
        public int id_confirmacao_pagamento;
        [NotMapped]
        public int IdConfirmacaoPagamentoItem;
        
        [Column("id_programacao_desembolso_execucao_item")]
        public int? Codigo { get; set; }

        [Column("id_execucao_pd")]
        public int id_execucao_pd { get; set; }

        [Column("ds_numpd")]
        public string NumPD { get; set; }

        [Column("nr_op")]
        public string NumOP { get; set; }

        [Column("id_tipo_pagamento")]
        public int? TipoPagamento { get; set; }

        [Column("id_tipo_documento")]
        public int IdTipoDocumento { get; set; }

        [Column("nr_documento")]
        public string NumeroDocumento { get; set; }

        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Column("nr_documento_gerador")]
        public string NumeroDocumentoGerador { get; set; }

        [Column("ug")]
        public string UG { get; set; }

        [Column("gestao")]
        public string Gestao { get; set; }

        [NotMapped]
        [Column("nr_cnpj_cpf_credor")]
        public string NumeroCnpjCpfCredor { get; set; }

        [Column("nr_cnpj_cpf_pgto")]
        public string NumeroCnpjcpfPagto { get; set; }

        [Column("ug_pagadora")]
        public string UGPagadora { get; set; }

        [Column("ug_liquidante")]
        public string UGLiquidante { get; set; }

        [Column("gestao_pagadora")]
        public string GestaoPagadora { get; set; }

        [Column("gestao_liguidante")]
        public string GestaoLiquidante { get; set; }

        [Column("favorecido")]
        public string Favorecido { get; set; }

        [Column("favorecidoDesc")]
        public string FavorecidoDesc { get; set; }

        [Column("ordem")]
        public string Ordem { get; set; }

        [Column("ano_pd")]
        public string AnoAserpaga { get; set; }

        [Column("dt_confirmacao")]

        public DateTime? Dt_confirmacao { get; set; }

        [Column("valor")]
        public string Valor { get; set; }

        [Column("ds_noup")]
        public string NouP { get; set; }

        [NotMapped]
        public string NormalOuPrioritario { get; set; }

        [NotMapped]
        public bool Prioritario { get; set; }

        [NotMapped]
        public int NumeroAgrupamentoProgramacaoDesembolso { get; set; }

        [Column("nr_agrupamento_pd")]
        public int? AgrupamentoItemPD { get; set; }

        //[Column("ds_numob")]
        //public string NumOB { get; set; }

        [Column("ds_numob")]
        public string NumOBItem { get; set; }

        [Column("ob_cancelada")]
        public bool? OBCancelada { get; set; }

        [Column("fl_sistema_prodesp")]
        public bool? fl_sistema_prodesp { get; set; }

        [Column("cd_transmissao_status_prodesp")]
        public string cd_transmissao_status_prodesp { get; set; }

        [Column("fl_transmissao_transmitido_prodesp")]
        public bool? fl_transmissao_transmitido_prodesp { get; set; }

        [Column("dt_transmissao_transmitido_prodesp")]
        public DateTime? dt_transmissao_transmitido_prodesp { get; set; }

        [Column("ds_transmissao_mensagem_prodesp")]
        public string ds_transmissao_mensagem_prodesp { get; set; }

        [Column("cd_transmissao_status_siafem")]
        public string cd_transmissao_status_siafem { get; set; }

        [Column("fl_transmissao_transmitido_siafem")]
        public bool? fl_transmissao_transmitido_siafem { get; set; }

        [Column("dt_transmissao_transmitido_siafem")]
        public DateTime? dt_transmissao_transmitido_siafem { get; set; }

        [Column("ds_transmissao_mensagem_siafem")]
        public string ds_transmissao_mensagem_siafem { get; set; }

        [Column("ds_consulta_op_prodesp")]
        public string ds_consulta_op_prodesp { get; set; }

        [Column("ds_causa_cancelamento")]
        public string CausaCancelamento { get; set; }

        #region IPagamentoContaUnica
        public int Id { get; set; }

        public bool CadastroCompleto { get; set; }

        public string CodigoAplicacaoObra { get; set; }

        [Column("dt_emissao")]
        public DateTime DataEmissao { get; set; }

        [Column("dt_vencimento")]
        public DateTime DataVencimento { get; set; }
        
        public int RegionalId { get; set; }

        public DateTime DataCadastro { get; set; }

        public int DocumentoTipoId { get; set; }

        public DocumentoTipo DocumentoTipo { get; set; }

        public DateTime DataConfirmacao { get; set; }
        [NotMapped]
        public bool Executar { get; set; }

        public bool IsDesdobramento { get
            {
                return !string.IsNullOrWhiteSpace(NumeroDocumentoGerador) && !NumeroDocumentoGerador.Substring(NumeroDocumentoGerador.Length - 1).Equals("0");
            }
        }

        #endregion IPagamentoContaUnica

        public PDExecucaoItem ToConfirmacaoPagamentoItem()
        {
            var item = new PDExecucaoItem();

            item.Id = this.Id;
            return item;
        }

    }
}
