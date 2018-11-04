
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento
{
    public class DadoDesdobramentoIdentificacaoViewModel
    {
        [Display(Name = "Tipo Desdobramento")]
        public string Tipo { get; set; }
        public IEnumerable<SelectListItem> DesdobramentoTipoListItems { get; set; }

        [Display(Name = "Nome Reduzido")]
        public string ReduzidoCredor { get; set; }

        [Display(Name = "Não Reter")]
        public string ReterId { get; set; }

        public IEnumerable<SelectListItem> ReterListItems { get; set; }

        [Display(Name = "Desdobramento")]
        public string Desdobramento { get; set; }


        [Display(Name = "Percentual")]
        public string ValorPercentual { get; set; }


        [Display(Name = "% Base Calc")]
        public string BaseCalc { get; set; }

        [Display(Name = "Valor Desdobrado")]
        public string ValorDesdobrado { get; set; }

        [Display(Name = "Valor Distribuição")]
        public string ValorDistribuicao { get; set; }


        public string ReterDescicao { get; set; }

        public string DesdobramentoTipo { get; set; }
        public string TipoBloqueio { get; set; }

        [Display(Name = "Desdobramento")]
        public string Sequencia { get; set; }

        public bool TransmitidoProdesp { get; set; }


        public DadoDesdobramentoIdentificacaoViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento desdobramento,IdentificacaoDesdobramento entity, IEnumerable<DesdobramentoTipo> tipoDesdobramentos, IEnumerable<Reter> reters)
        {
            var desdobramentoTipos = tipoDesdobramentos as IList<DesdobramentoTipo> ?? tipoDesdobramentos.ToList();
            var reterList = reters as IList<Reter> ?? reters.ToList();

            return new DadoDesdobramentoIdentificacaoViewModel
            {

                Tipo = Convert.ToString(desdobramentoTipos.FirstOrDefault(x => x.Id == entity.DesdobramentoTipoId)?.Id),
                DesdobramentoTipo = desdobramentoTipos.Where(x => x.Id == entity.DesdobramentoTipoId).Select(x => x.Id + " - " + x.Descricao).FirstOrDefault(),//Convert.ToString(desdobramentoTipos.FirstOrDefault(x => x.Id == entity.DesdobramentoTipoId)?.Descricao),

                DesdobramentoTipoListItems = desdobramentoTipos.OrderBy(x => x.Id).
                Select(s => new SelectListItem
                {
                    Text = $"{s.Id} - {s.Descricao}" ,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.DesdobramentoTipoId
                }),


                ReterId = Convert.ToString(entity.ReterId),

                ReterDescicao = reterList.Where(x => x.Id == entity.ReterId).Select(x => x.Id +" - " + x.Descricao).FirstOrDefault(),

                ReterListItems = reterList.OrderBy(x => x.Id).
                Select(s => new SelectListItem
                {
                    Text = $"{s.Id} - {s.Descricao}",
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.ReterId
                }),


                ReduzidoCredor = entity.NomeReduzidoCredor,
                Desdobramento = entity.Desdobramento.ToString(),
                ValorPercentual = entity.ValorPercentual == 0 ? default(string) : (entity.ValorPercentual).ToString(),
                BaseCalc = entity.ValorPercentual == 0 ? default(string) : (entity.ValorPercentual).ToString(),
                ValorDesdobrado = entity.ValorDesdobrado == 0 ? default(string) : entity.ValorDesdobrado.ToString(),
                ValorDistribuicao = entity.ValorDistribuicao == 0 ? default(string) : entity.ValorDistribuicao.ToString(),
                Sequencia = entity.Sequencia.ToString("D2"),
                TransmitidoProdesp = desdobramento.TransmitidoProdesp
            };
        }

    }
}