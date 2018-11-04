using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    [Table("tb_Itens_obs_re")]
    public class ListaRE
    {
        [Key]
        [Column("id_tb_itens_obs_re")]
        public int Id { get; set; }

        [Display(Name = "Nº RE")]
        [Column("cd_relob_re")]
        public string NumeroRE { get; set; }

        [Display(Name = "Nº OB")]
        [Column("nr_ob_re")]
        public string NumeroOB { get; set; }

        [Display(Name = "Prioridade")]
        [Column("fg_prioridade")]
        public string FlagPrioridade { get; set; }

        [Display(Name = "Tipo OB")]
        [Column("cd_tipo_ob")]
        public int TipoOB { get; set; }

        [Display(Name = "Nome do Favorecido")]
        [Column("ds_nome_favorecido")]
        public string NomeFavorecido { get; set; }

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