using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Entity.Seguranca;
using System.Linq;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Extension;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    public class MovimentacaoFiltroViewModel
    {
        #region Propriedades
        public int Id { get; set; }


        [Display(Name = "Unidade Gestora")]
        public string UnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string idGestao { get; set; }

        [Display(Name = "N° Agrupamento")]
        public int? numAgrupamento { get; set; }

        [Display(Name = "N° Documento")]
        public string numDocumento { get; set; }

        [Display(Name = "Tipo Documento")]
        public IEnumerable<SelectListItem> lstDocumentos { get; set; }

        [Display(Name = "Tipo Movimentação")]
        public IEnumerable<SelectListItem> lstMovimentacao { get; set; }

        [Display(Name = "UG Favorecido")]
        public string ugFavorecido { get; set; }

        [Display(Name = "Gestão Favorecida")]
        public string idGestaoFavorecida { get; set; }

        [Display(Name = "C.F.P. (Programa de Trabalho)")]
        public long? idCFP { get; set; }

        [Display(Name = "C.E.D. (Natureza)")]
        public int? idCED { get; set; }


        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }

        [Display(Name = "Status Siafem")]
        public string StatusSiafem { get; set; }


        public string NumSiafem { get; set; }


        public IEnumerable<SelectListItem> DocumentoListItems { get; set; }

        public IEnumerable<SelectListItem> MovimentacaoListItems { get; set; }



        public IEnumerable<SelectListItem> lstStatusProdesp { get; set; }

        public IEnumerable<SelectListItem> lstStatusSiafem { get; set; }


        [Display(Name = "Data Cadastramento De")]
        public DateTime? DataCadastroDe { get; set; }

        [Display(Name = "Data Cadastramento Até")]
        public DateTime? DataCadastroAte { get; set; }


        [Display(Name = "Valor")]
        public int? Valor { get; set; }

        public int? tipoDocumento { get; set; }

        public int? tipoMovimentacao { get; set; }

        [Display(Name = "Tipo Documento")]
        public int? DocumentoId { get; set; }

        [Display(Name = "Tipo Movimentação")]
        public int? MovimentacaoId { get; set; }



        #endregion

        #region Singleton
        public MovimentacaoFiltroViewModel CreateInstance()
        {
            MovimentacaoFiltroViewModel filtro = new MovimentacaoFiltroViewModel();
            return filtro;
        }

        //internal MovimentacaoOrcamentariaFiltroViewModel CreateInstance(MovimentacaoOrcamentaria entity, IEnumerable<Regional> regional, DateTime de, DateTime ate)
        internal MovimentacaoFiltroViewModel CreateInstance(MovimentacaoOrcamentaria entity, IEnumerable<MovimentacaoTipo> movimentacao, IEnumerable<MovimentacaoDocumentoTipo> documento, DateTime de, DateTime ate)
        {

            MovimentacaoFiltroViewModel filtro = new MovimentacaoFiltroViewModel();

            filtro.Id = entity.Id;               
            
            filtro.DataCadastroDe = null;
            filtro.DataCadastroAte = null;
            

            filtro.StatusProdesp = entity.StatusProdesp;
            filtro.lstStatusProdesp = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            filtro.StatusSiafem = entity.StatusSiafem;
            filtro.lstStatusSiafem = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            filtro.DocumentoListItems = (Enum.GetValues(typeof(EnumTipoDocumentoMovimentacaoCompleto))
                .Cast<int>()
                .Select(e => new SelectListItem()
                {
                    Text = EnumExtension.GetEnumDescription<EnumTipoDocumentoMovimentacaoCompleto>((EnumTipoDocumentoMovimentacaoCompleto)e),
                    Value = e.ToString()
                })).ToList();

            // Fixar o documento selecionado no combox
            foreach (var dl in filtro.DocumentoListItems)
            {
                dl.Selected = true;

                if (dl.Value != entity.IdTipoDocumento.ToString())
                {
                    dl.Selected = false;
                }
            }

            filtro.MovimentacaoListItems = movimentacao.ToList()
            .Select(s => new SelectListItem
            {
                Text = s.Descricao,
                Value = s.Id.ToString(),
                Selected = s.Id == entity.IdTipoMovimentacao
            });

            return filtro;
        }
        #endregion

    }
}