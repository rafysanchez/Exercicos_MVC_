using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.ValueObject.PagamentoContaUnica
{
    public class ImpressaoRelacaoReRtConsultaVo : ImpressaoRelacaoRERT
    {
        #region Lista RE e RT Base

        [Display(Name = "Banco Favorecido")]
        [Column("ds_banco_favorecido")]
        public string BancoFavorecido { get; set; }

        [Display(Name = "Agência Favorecida")]
        [Column("cd_agencia_favorecido")]
        public string AgenciaFavorecida { get; set; }

        [Display(Name = "Conta Favorecida")]
        [Column("ds_conta_favorecido")]
        public string ContaFavorecida { get; set; }

        [Display(Name = "Valor OB")]
        [Column("vl_ob")]
        public decimal ValorOB { get; set; }

        [Display(Name = "Nº OB")]
        [Column("nr_ob")]
        public string NumeroOB { get; set; }

        #endregion

        #region Lista RE

        [Display(Name = "Nº RE")]
        [Column("cd_relob_re")]
        public string NumeroRE { get; set; }

        [Display(Name = "Prioridade")]
        [Column("fg_prioridade")]
        public string FlagPrioridade { get; set; }

        [Display(Name = "Tipo OB")]
        [Column("cd_tipo_ob")]
        public int TipoOB { get; set; }

        [Display(Name = "Nome do Favorecido")]
        [Column("ds_nome_favorecido")]
        public string NomeFavorecido { get; set; }

        #endregion

        #region Lista RT

        [Display(Name = "Nº RT")]
        [Column("cd_relob_rt")]
        public string NumeroRT { get; set; }

        [Display(Name = "Conta Bancária Emitente")]
        [Column("cd_conta_bancaria_emitente")]
        public string ContaBancariaEmitente { get; set; }

        [Display(Name = "Unidade Gestora Favorecida")]
        [Column("cd_unidade_gestora_favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Gestão Favorecida")]
        [Column("cd_gestao_favorecida")]
        public string GestaoFavorecida { get; set; }

        [Display(Name = "Mnemonico UF Favorecida")]
        [Column("ds_mnemonico_ug_favorecida")]
        public string MnemonicoUfFavorecida { get; set; }

        #endregion
    }
}
