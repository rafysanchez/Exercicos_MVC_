namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Subempenho : LiquidacaoDespesa
    {
        [Column("id_subempenho")]
        public override int Id { get; set; }

        [Display(Name = "Ano")]
        [Column("nr_ano_exercicio")]
        public int NumeroAnoExercicio { get; set; }

        [Column("cd_nota_fiscal_prodesp")]
        public string CodigoNotaFiscalProdesp { get; set; }

        [Column("nr_medicao")]
        public string NumeroMedicao { get; set; }
        
        [Column("nr_empenho_prodesp")]
        public override string NumeroOriginalProdesp { get; set; }
               
        [Column("tb_programa_id_programa")]
        public int ProgramaId { get; set; }

        [Column("tb_estrutura_id_estrutura")]
        public int NaturezaId { get; set; }

        [Column("tb_fonte_id_fonte")]
        public int FonteId { get; set; }

        [Column("cd_credor_organizacao")]
        public int CodigoCredorOrganizacao { get; set; }

        [Column("nr_cnpj_cpf_fornecedor")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        [Column("nr_caucao_guia")]
        public string NumeroGuia { get; set; }

        [Column("nm_caucao_quota_geral_autorizado_por")]
        public string QuotaGeralAutorizadaPor { get; set; }

        [Column("vl_caucao_caucionado")]
        public int ValorCaucionado { get; set; }

        [Column("nm_credor")]
        public string NomeCredor { get; set; }

       
        public override string Normal => "X";
        public override string Estorno => default(string);
    }
}