using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoImpressaoRelacaoReRtConsultaViewModel : IPagamentoContaUnica
    {
        public int Id { get; set; }

        [Display(Name = "Nº RE")]
        public string NumeroRE { get; set; }

        [Display(Name = "Nº RT")]
        public string NumeroRT { get; set; }

        [Display(Name = "Nº OB")]
        public string NumeroOB { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Transmissão")]
        public DateTime DataTransmissaoSiafem { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Banco")]
        public string CodigoBanco { get; set; }

        public bool FlagTransmitidoSiafem { get; set; }

        public bool FlagTransmitirSiafem { get; set; }

        [Display(Name = "Agência Favorecida")]
        public string AgenciaFavorecida { get; set; }

        [Display(Name = "Conta Favorecida")]
        public string ContaFavorecida { get; set; }

        [Display(Name = "Prioridade")]
        public string FlagPrioridade { get; set; }

        [Display(Name = "Tipo OB")]
        public int TipoOB { get; set; }

        [Display(Name = "Nome do Favorecido")]
        public string NomeFavorecido { get; set; }

        [Display(Name = "Banco Favorecido")]
        public string BancoFavorecido { get; set; }

        [Display(Name = "Valor OB")]
        public decimal ValorOB { get; set; }

        [Display(Name = "Conta Bancária Emitente")]
        public string ContaBancariaEmitente { get; set; }

        [Display(Name = "Unidade Gestora Favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Gestão Favorecida")]
        public string GestaoFavorecida { get; set; }

        [Display(Name = "Mnemonico UF Favorecida")]
        public string MnemonicoUfFavorecida { get; set; }

        public string MsgRetornoTransmissaoSiafem { get; set; }

        public IEnumerable<ImpressaoRelacaoReRtConsultaVo> Filhos { get; set; }

        public bool CadastroCompleto { get; set; }

        public string CodigoAplicacaoObra { get; set; }

        public DateTime DataEmissao { get; set; }

        public int RegionalId { get; set; }

        public string NumeroContrato { get; set; }

        public int DocumentoTipoId { get; set; }

        public string NumeroDocumento { get; set; }
        public DocumentoTipo DocumentoTipo { get; set; }

        internal DadoImpressaoRelacaoReRtConsultaViewModel CreateInstance(ImpressaoRelacaoReRtConsultaPaiVo entity)
        {
            var retorno = new DadoImpressaoRelacaoReRtConsultaViewModel();

            retorno.Id = entity.Id;
            retorno.CodigoUnidadeGestora = entity.CodigoUnidadeGestora;
            retorno.CodigoGestao = entity.CodigoGestao;
            retorno.CodigoBanco = entity.CodigoBanco;
            retorno.MsgRetornoTransmissaoSiafem = entity.MsgRetornoTransmissaoSiafem;
            retorno.FlagTransmitidoSiafem = entity.FlagTransmitidoSiafem;
            retorno.Filhos = entity.Filhos;

            if (entity.Filhos == null)
            {
                entity.Filhos = new List<ImpressaoRelacaoReRtConsultaVo>();
            }

            var objFilho = entity.Filhos.FirstOrDefault();
            if (objFilho != null)
            {
                retorno.NumeroRE = objFilho.CodigoRelacaoRERT?.Substring(4,2) == "RE" ? objFilho.CodigoRelacaoRERT : objFilho.NumeroRE;
                retorno.NumeroRT = objFilho.CodigoRelacaoRERT?.Substring(4, 2) == "RT" ? objFilho.CodigoRelacaoRERT : objFilho.NumeroRT;
                retorno.NumeroOB = objFilho.NumeroOB;
                retorno.AgenciaFavorecida = objFilho.AgenciaFavorecida;
                retorno.ContaFavorecida = objFilho.ContaFavorecida;
                retorno.FlagPrioridade = objFilho.FlagPrioridade;
                retorno.TipoOB = objFilho.TipoOB;
                retorno.NomeFavorecido = objFilho.NomeFavorecido;
                retorno.BancoFavorecido = objFilho.BancoFavorecido;
                retorno.ValorOB = objFilho.ValorOB;
                retorno.ContaBancariaEmitente = objFilho.ContaBancariaEmitente;
                retorno.UnidadeGestoraFavorecida = objFilho.UnidadeGestoraFavorecida;
                retorno.GestaoFavorecida = objFilho.GestaoFavorecida;
                retorno.MnemonicoUfFavorecida = objFilho.MnemonicoUfFavorecida;
            }

            return retorno;
        }
    }
}