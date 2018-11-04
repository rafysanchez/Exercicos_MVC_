using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sids.Prodesp.UI.Areas.Parametrizacao.Models
{
    public class NlParametrizacaoViewModel
    {
        public int IdParametrizacao { get; set; }

        //3.1	RDN1 – Consultar Parametrização
        //Para a consulta de parametrização é necessário escolher o campo “Tipo de NL”.
        //Ao selecionar um registro deve ser executada a consulta e preenchimento dos demais campos.
        //3.2	RDN2 – Campos Obrigatórios Apenas em Tela
        //Para salvar as informações, o campo “Tipo de NL” é obrigatório apenas na tela.
        [Required]
        [Display(Name = "Tipo de NL")]
        public int IdTipoNL { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Gerar Tipo Nl")]
        public int IdFormaGerarNl { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public bool Transmitir { get; set; }

        [Display(Name = "Unidade Gestora")]
        public int? UnidadeGestora { get; set; }

        [Display(Name = "CNPJ/CPF/UG Favorecida")]
        public string FavorecidaCnpjCpfUg { get; set; }

        [Display(Name = "Gestão Favorecida")]
        public int FavorecidaNumeroGestao { get; set; }


        public IEnumerable<NlParametrizacaoDespesaViewModel> Despesas { get; set; }

        public IEnumerable<NlParametrizacaoEventoViewModel> Eventos { get; set; }

        public NlParametrizacaoViewModel()
        {
            Despesas = new List<NlParametrizacaoDespesaViewModel>();
            Eventos = new List<NlParametrizacaoEventoViewModel>();
        }

        public NlParametrizacaoViewModel(NlParametrizacao entity)
        {
            this.IdParametrizacao = entity.Id;
            this.IdTipoNL = entity.Tipo;
            this.Observacao = entity.Observacao;
            this.Transmitir = entity.Transmitir;
            this.UnidadeGestora = entity.UnidadeGestora;
            this.FavorecidaCnpjCpfUg = entity.FavorecidaCnpjCpfUg;
            this.FavorecidaNumeroGestao = entity.FavorecidaNumeroGestao;
            this.IdFormaGerarNl = entity.IdFormaGerarNl;

            var eventos = new List<NlParametrizacaoEventoViewModel>();
            foreach (var item in entity.Eventos)
            {
                eventos.Add(new NlParametrizacaoEventoViewModel(item));
            }
            this.Eventos = eventos;

            var despesas = new List<NlParametrizacaoDespesaViewModel>();
            foreach (var item in entity.Despesas)
            {
                despesas.Add(new NlParametrizacaoDespesaViewModel(item));
            }
            this.Despesas = despesas;

        }

        public NlParametrizacao ToEntity()
        {
            var entity = new NlParametrizacao();
            var despesas = new List<NlParametrizacaoDespesa>();
            var eventos = new List<NlParametrizacaoEvento>();

            entity.Id = this.IdParametrizacao;
            entity.Tipo = this.IdTipoNL;
            entity.Observacao = this.Observacao;
            entity.Transmitir = this.Transmitir;
            entity.UnidadeGestora = this.UnidadeGestora;
            entity.FavorecidaCnpjCpfUg = this.FavorecidaCnpjCpfUg;
            entity.FavorecidaNumeroGestao = this.FavorecidaNumeroGestao;
            entity.IdFormaGerarNl = this.IdFormaGerarNl;

            foreach (var despesa in this.Despesas)
            {
                despesas.Add(despesa.ToEntity());
            }
            entity.Despesas = despesas;

            foreach (var evento in this.Eventos)
            {
                eventos.Add(evento.ToEntity());
            }
            entity.Eventos = eventos;

            return entity;
        }
    }
}