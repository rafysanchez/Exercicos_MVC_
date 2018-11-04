namespace Sids.Prodesp.Model.Base.LiquidacaoDespesa
{
    using Interface.LiquidacaoDespesa;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Rap : LiquidacaoDespesa, IRap
    {
        [Column("tb_programa_id_programa")]
        public int ProgramaId { get; set; }

        [Column("tb_estrutura_id_estrutura")]
        public int NaturezaId { get; set; }

        [Column("tb_servico_tipo_id_servico_tipo")]
        public int TipoServicoId { get; set; }

        [Column("nr_classificacao")]
        public string Classificacao { get; set; }
        
        [Column("cd_credor_organizacao")]
        public int CodigoCredorOrganizacao { get; set; }
        [Column("cd_nota_fiscal_prodesp")]
        public string CodigoNotaFiscalProdesp { get; set; }
        [Column("nr_ano_exercicio")]
        public int NumeroAnoExercicio { get; set; }
        [Column("ds_uso_autorizado_por")]
        public string DescricaoUsoAutorizadoPor { get; set; }
        
        [Column("[tb_natureza_tipo_id_natureza_tipo]")]
        public string CodigoNaturezaItem { get; set; }

        [Column("vl_caucao_caucionado")]
        public int ValorCaucionado { get; set; }
        [Column("cd_despesa")]
        public new string CodigoDespesa { get; set; }


        [Column("dt_realizado")]
        public new DateTime DataRealizado { get; set; }
        [Column("nr_medicao")]
        public string NumeroMedicao { get; set; }
        [Column("ds_prazo_pagamento")]
        public string DescricaoPrazoPagamento { get; set; }
        [Column("cd_tarefa")]
        public new string CodigoTarefa { get; set; }

        [Column("tarefa")]
        public string Tarefa { get; set; }

        [Column("nr_cnpj_cpf_fornecedor")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        [Column("nr_recibo")]
        public new string NumeroRecibo { get; set; }
        [Column("nr_caucao_guia")]
        public string NumeroGuia { get; set; }
        [Column("nm_caucao_quota_geral_autorizado_por")]
        public string QuotaGeralAutorizadaPor { get; set; }
        [Column("nm_dados_caucao")]
        public string DadosCaucao { get; set; }
        [Column("vr_subempenhar")]
        public int ValorSubempenhar { get; set; }
        [Column("cd_gestao_fornecedora")]
        public string CodigoGestaoFornecedora { get; set; }


        [Column("vl_anulado")]
        public string ValorAnulado { get; set; }
        [Column("vl_saldo_apos_anulacao")]
        public string ValorSaldoAposAnulacao { get; set; }
        [Column("vl_saldo_anterior_subempenho")]
        public string ValorSaldoAnteriorSubempenho { get; set; }

        [Column("nr_subempenho")]
        public string NumeroSubempenho { get; set; }
        
        [NotMapped]
        public virtual string TipoRap { get; }

        [Column("cedid")]
        public int CEDId { get; set; }

        [Column("nr_empenho")]
        public  string NumeroEmpenho { get; set; }

    }
}
