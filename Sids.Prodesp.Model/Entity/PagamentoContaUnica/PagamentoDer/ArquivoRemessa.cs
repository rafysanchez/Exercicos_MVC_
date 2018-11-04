using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class ArquivoRemessa 
    {

        [Column("id_arquivo_remessa")]
        public int Id { get; set; }

        [Column("tb_arquivo_id_arquivo")]
        public int IdArquivo { get; set; }

        [Column("nr_geracao_arquivo")]
        public int? NumeroGeracao { get; set; }


        [Display(Name = "Data da Preparação da OP")]
        [Column("dt_preparacao_pagamento")]
        public DateTime? DataPreparacao{ get; set; }


        [Display(Name = "Data para Pagamento")]
        [Column("dt_pagamento")]
        public DateTime? DataPagamento { get; set; }

        [Column("cd_assinatura")]
        public string CodigoAssinatura { get; set; }

        [Column("cd_grupo_assinatura")]
        public string CodigoGrupoAssinatura { get; set; }

        [Column("cd_orgao_assinatura")]
        public string CodigoOrgaoAssinatura { get; set; }

        [Column("nm_assinatura")]
        public string NomeAssinatura { get; set; }

        [Column("ds_cargo")]
        public string DesCargo { get; set; }


        [Column("cd_contra_assinatura")]
        public string CodigoContraAssinatura { get; set; }

        [Column("cd_grupo_contra_assinatura")]
        public string CodigoContraGrupoAssinatura { get; set; }

        [Column("cd_orgao_contra_assinatura")]
        public string CodigoContraOrgaoAssinatura { get; set; }

        [Column("nm_contra_assinatura")]
        public string NomeContraAssinatura { get; set; }

        [Column("ds_cargo_contra_assinatura")]
        public string DesContraCargo { get; set; }

        [Display(Name = "Código da Conta")]
        [Column("cd_conta")]
        public int? CodigoConta { get; set; }

        [Column("ds_banco")]
        public string Banco { get; set; }


        [Display(Name = "Agência")]
        [Column("ds_agencia")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        [Column("ds_conta")]
        public string NumeroConta { get; set; }


        [Display(Name = "Quantidade de Op do Arquivo")]
        [Column("qt_ordem_pagamento_arquivo")]
        public int? QtOpArquivo { get; set; }

        [Display(Name = "Quantidade de Depósitos")]
        [Column("qt_deposito_arquivo")]
        public int? QtDeposito { get; set; }

        [Display(Name = "Valor Total")]
        [Column("vr_total_pago")]
        public int? ValorTotal { get; set; }

        [Display(Name = "Quantidade de DOC/TED")]
        [Column("qt_doc_ted_arquivo")]
        public int? QtDocTed { get; set; }

        [Display(Name = "Data do Cadastro")]
        [Column("dt_cadastro")]
        public DateTime DataCadastro { get; set; }


        [Column("fg_trasmitido_prodesp")]
        public string StatusProdesp { get; set; }

        [Display(Name = "Data da Transmissão")]
        [Column("dt_trasmitido_prodesp")]
        public DateTime? DataTrasmitido { get; set; }

        [Column("fg_arquivo_cancelado")]
        public bool? Cancelado { get; set; }

        [Display(Name = "Regional")]
        [Column("id_regional")]
        public int RegionalId { get; set; }

        [Column("bl_cadastro_completo")]
        public bool CadastroCompleto { get; set; }

        [Column("ds_msg_retorno")]
        public string MensagemServicoProdesp { get; set; }


        [Column("bl_transmitir_prodesp")]
        public  bool TransmitirProdesp { get; set; }

        [Column("bl_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }


        //campos Arquivo preparado
        [Display(Name = "Órgão")]
        public string Orgao { get; set; }

        [Display(Name = "Situação do Arquivo")]
        public string Situacao { get; set; }


        [Display(Name = "Data da Preparação do Arquivo")]
        public string ResultadoPreparacao { get; set; }


        [Display(Name = "Data da Geração do Arquivo")]
        public string DataGeracao { get; set; }

        [Display(Name = "Data do  Retorno da Prévia")]
        public string dataPrevia { get; set; }

        [Display(Name = "Resultado da Prévia")]
        public string ResultadoPrevia { get; set; }

        [Display(Name = "Data Retorno do Processamento")]
        public string dataProcessamento { get; set; }

        [Display(Name = "Resultado do Processamento")]
        public string ResultadoProcessamento { get; set; }

        [Display(Name = "Data Retorno Consolidado")]
        public string dataConsolidado { get; set; }

        [Display(Name = "Resultado Consolidado")]
        public string ResultadoConsolidado{ get; set; }

        [Display(Name = "Valor do Depósito")]
        public string ValorDep { get; set; }

        [Display(Name = "Valor do DOC/TED")]
        public string ValorDocTed { get; set; }

        [Display(Name = "Valor Total Creditado")]
        public string ValorCreditado { get; set; }

        [Display(Name = "Valor não Creditado")]
        public string ValorNaoCreditado { get; set; }


        public List<ArquivoRemessaOP> ListOps { get; set; }
        //public IEnumerable<ArquivoRemessaOP> ListOps { get; set; }


        public string SelArquivo { get; set; }

    }
}
