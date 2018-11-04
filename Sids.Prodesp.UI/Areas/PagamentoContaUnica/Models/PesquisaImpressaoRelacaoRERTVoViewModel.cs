using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using System;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class PesquisaImpressaoRelacaoRERTVoViewModel : ImpressaoRelacaoRERT
    {
        #region Lista RE e RT Base

        [Column("id_impressao_relacao_re_rt")]
        public override int Id { get; set; }

        [Display(Name = "Nº OB")]
        [Column("nr_ob")]
        public string NumeroOB { get; set; }

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

        public PesquisaImpressaoRelacaoRERTVoViewModel CreateInstance(ImpressaoRelacaoReRtConsultaVo entity)
        {
            return new PesquisaImpressaoRelacaoRERTVoViewModel
            {
                Id = entity.Id,

                NumeroRE = entity.CodigoRelacaoRERT?.Substring(4,2) == "RE" ? entity.CodigoRelacaoRERT : null,

                NumeroRT = entity.CodigoRelacaoRERT?.Substring(4, 2) == "RT" ? entity.CodigoRelacaoRERT : null,

                DataCadastramento = entity.DataCadastramento,

                DataTransmissaoSiafem = entity.DataTransmissaoSiafem,

                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,

                CodigoGestao = entity.CodigoGestao,

                CodigoBanco = entity.CodigoBanco,

                MsgRetornoTransmissaoSiafem = entity.MsgRetornoTransmissaoSiafem,

                NumeroOB = entity.CodigoOB,

                ContaBancariaEmitente = entity.ContaBancariaEmitente,

                UnidadeGestoraFavorecida = entity.UnidadeGestoraFavorecida,

                GestaoFavorecida = entity.GestaoFavorecida,

                MnemonicoUfFavorecida = entity.MnemonicoUfFavorecida,

                BancoFavorecido = entity.BancoFavorecido,

                AgenciaFavorecida = entity.AgenciaFavorecida,

                ContaFavorecida = entity.ContaFavorecida,

                ValorOB = entity.ValorOB,

                FlagPrioridade = entity.FlagPrioridade,

                TipoOB = entity.TipoOB,

                NomeFavorecido = entity.NomeFavorecido,
            };
        }
    }
}