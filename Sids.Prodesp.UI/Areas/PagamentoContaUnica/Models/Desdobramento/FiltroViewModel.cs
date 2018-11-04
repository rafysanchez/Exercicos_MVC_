using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento
{
    public class FiltroViewModel
    {

        [Display(Name = "Tipo de Desdobramento")]
        public string DesdobramentoTipoId { get; set; }
        public IEnumerable<SelectListItem> DesdobramentoTipoListItems { get; set; }



        [Display(Name = "Tipo Documento")]
        public string DocumentoTipoId { get; set; }
        public IEnumerable<SelectListItem> DocumentoTipoListItems { get; set; }


        [Display(Name = "Nº Documento")]
        public string NumeroDocumento { get; set; }



        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }
        public IEnumerable<SelectListItem> StatusProdespListItems { get; set; }


        [Display(Name = "Data de Cadastro De")]
        public string DataCadastramentoDe { get; set; }

        [Display(Name = "Data de Cadastro Até")]
        public string DataCadastramentoAte { get; set; }


        [Display(Name = "Cancelado")]
        public string Cancelado { get; set; }
        public IEnumerable<SelectListItem> StatusCanceladoListItems { get; set; }

        #region Reclassificação properties

        [Display(Name = "Nº Reclassificação/Ret. SIAFEM")]
        public string NumeroSiafem { get; set; }

        [Display(Name = "Nº Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Nº do Empenho SIAFEM/SIAFISICO")]
        public string NumeroOriginalSiafemSiafisico { get; set; }

        [Display(Name = "Tipo de Reclassificação / Retenção")]
        public string ReclassificacaoRetencaoTipo { get; set; }
        public IEnumerable<SelectListItem> ReclassificacaoRetencaoTipoListItems { get; set; }

        [Display(Name = "Normal ou Estorno?")]
        public string NormalEstorno { get; set; }
        public IEnumerable<SelectListItem> NormalEstornoListItems { get; set; }

        [Display(Name = "Status Siafem")]
        public string StatusSiafem { get; set; }
        public IEnumerable<SelectListItem> StatusSiafemListItems { get; set; }

        [Display(Name = "Nº Contrato")]
        public string NumeroContrato { get; set; }

        #endregion

        public FiltroViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento objModel, IEnumerable<DesdobramentoTipo> tipoDesdobramento, IEnumerable<DocumentoTipo> documento,   DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();


            filtro.DesdobramentoTipoId = Convert.ToString(tipoDesdobramento?.FirstOrDefault(x => x.Id == objModel.DesdobramentoTipoId));

            filtro.DesdobramentoTipoListItems = tipoDesdobramento?.Where(x => x.Id <= 2 ).
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.DesdobramentoTipoId
                });


            filtro.DocumentoTipoId = Convert.ToString(documento?.FirstOrDefault(x => x.Id == objModel.DesdobramentoTipoId));

            filtro.DocumentoTipoListItems = documento?.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.DocumentoTipoId
                });


            filtro.StatusProdesp = Convert.ToString(objModel.TransmitidoProdesp && objModel.DataTransmitidoProdesp > default(DateTime)).ToLower();
            filtro.StatusProdespListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };


            filtro.Cancelado = Convert.ToString(objModel.SituacaoDesdobramento == "S").ToLower();
            filtro.StatusCanceladoListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Cancelado", Value = "S" },
                    new SelectListItem { Text = "Ativo", Value = "N" }
                };

            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;

           return filtro;
        }

        public FiltroViewModel CreateInstance(ReclassificacaoRetencao objModel, IEnumerable<ReclassificacaoRetencaoTipo> tipoReclassificacao, DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();

            filtro.NumeroSiafem = objModel.NumeroSiafem;
            filtro.NumeroProcesso = objModel.NumeroProcesso;
            filtro.CodigoAplicacaoObra = objModel.CodigoAplicacaoObra;
            filtro.NumeroOriginalSiafemSiafisico = objModel.NumeroOriginalSiafemSiafisico;


            filtro.ReclassificacaoRetencaoTipoListItems = tipoReclassificacao
                .Select(x => new SelectListItem
                {
                    Text = x.Descricao,
                    Value = x.Id.ToString(),
                    Selected = x.Id == objModel.ReclassificacaoRetencaoTipoId
                });
            filtro.ReclassificacaoRetencaoTipo = objModel.ReclassificacaoRetencaoTipoId.ToString();


            filtro.NormalEstornoListItems = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Normal", Value = "1", Selected = objModel.NormalEstorno == "1"},
                    new SelectListItem {Text = "Estorno", Value = "2", Selected = objModel.NormalEstorno == "2"}
                };
            filtro.NormalEstorno = objModel.NormalEstorno;


            filtro.StatusSiafem = objModel.StatusSiafem;
            filtro.StatusSiafemListItems = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
            };


            filtro.NumeroContrato = objModel.NumeroContrato;
            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;

            return filtro;
        }


    }
}
