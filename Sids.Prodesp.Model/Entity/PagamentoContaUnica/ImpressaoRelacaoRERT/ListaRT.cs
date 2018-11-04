using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    [Table("tb_Itens_obs_rt")]
    public class ListaRT
    {
        [Key]
        [Column("id_tb_itens_obs_rt")]
        public int Id { get; set; }

        [Display(Name = "Nº RT")]
        [Column("cd_relob_rt")]
        public string NumeroRT { get; set; }

        [Display(Name = "Nº OB")]
        [Column("nr_ob_rt")]
        public string NumeroOB { get; set; }

        [Display(Name = "Conta Bancária Emitente")]
        [Column("cd_conta_bancaria_emitente")]
        public string ContaBancariaEmitente { get; set; }

        [Display(Name = "Unidade Gestora Favorecida")]
        [Column("cd_unidade_gestora")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Gestão Favorecida")]
        [Column("cd_gestao")]
        public string GestaoFavorecida { get; set; }

        [Display(Name = "Mnemonico UF Favorecida")]
        [Column("ds_mnemonico_ug_favorecida")]
        public string MnemonicoUfFavorecida { get; set; }

        [Display(Name = "Banco Favorecido")]
        [Column("ds_banco_favorecido")]
        public string BancoFavorecido { get; set; }

        [Display(Name = "Agência Favorecida")]
        [Column("cd_agencia_favorecida")]
        public string AgenciaFavorecida { get; set; }

        [Display(Name = "Conta Favorecida")]
        [Column("ds_conta_favorecida")]
        public string ContaFavorecida { get; set; }

        [Display(Name = "Valor OB")]
        [Column("vl_ob")]
        public decimal ValorOB { get; set; }
    }
}
