using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sids.Prodesp.UI.Areas.Parametrizacao.Models
{
    public class NlParametrizacaoEventoViewModel
    {
        public int Id { get; set; }

        public int IdParametrizacao { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int IdTipoDocumento { get; set; }

        [Display(Name = "Tipo RAP")]
        public string IdTipoRap { get; set; }

        [Display(Name = "Evento")]
        public string NumeroEvento { get; set; }

        [Display(Name = "Classificacao")]
        public string NumeroClassificacao { get; set; }

        [Display(Name = "Entrada/Saida")]
        public string EntradaSaidaDescricao { get; set; }

        [Display(Name = "Fonte")]
        public string NumeroFonte { get; set; }

        public NlParametrizacaoEventoViewModel()
        {

        }

        public NlParametrizacaoEventoViewModel(NlParametrizacaoEvento entity)
        {
            this.Id = entity.Id;
            this.IdParametrizacao = entity.IdNlParametrizacao;
            this.IdTipoDocumento = entity.IdDocumentoTipo;
            this.IdTipoRap = entity.IdRapTipo;
            this.NumeroEvento = entity.NumeroEvento;
            this.NumeroClassificacao = entity.NumeroClassificacao;
            this.EntradaSaidaDescricao = entity.EntradaSaidaDescricao;
            this.NumeroFonte = entity.NumeroFonte;
        }


        public NlParametrizacaoEvento ToEntity()
        {
            var entity = new NlParametrizacaoEvento();
            entity.Id = this.Id;
            entity.IdNlParametrizacao = this.IdParametrizacao;
            entity.IdDocumentoTipo = this.IdTipoDocumento;
            entity.IdRapTipo = this.IdTipoRap;
            entity.NumeroEvento = this.NumeroEvento;
            entity.NumeroClassificacao = this.NumeroClassificacao;
            entity.EntradaSaidaDescricao = this.EntradaSaidaDescricao;
            entity.NumeroFonte = this.NumeroFonte;
            return entity;
        }
    }
}