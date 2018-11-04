namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Seguranca;
    using Model.Extension;
    using Model.Interface;
    using Model.ValueObject;
    using Model.Interface.Empenho;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class PesquisaReservaEstruturaViewModel
    {
        [Display(Name = "Ano")]
        public string NumeroAnoExercicio { get; set; }
        public IEnumerable<SelectListItem> NumeroAnoExercicioListItems { get; set; }

        [Display(Name = "Órgão")]
        public string RegionalId { get; set; }
        public IEnumerable<SelectListItem> RegionalListItems { get; set; }

        [Display(Name = "CFP")]
        public string ProgramaId { get; set; }
        public IEnumerable<SelectListItem> ProgramaListItems { get; set; }

        [Display(Name = "CED")]
        public string NaturezaId { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }

        [Display(Name = "PTRES")]
        public string CodigoPtres { get; set; }
        public IEnumerable<SelectListItem> CodigoPtresListItems { get; set; }

        [Display(Name = "Origem do Recurso")]
        public string FonteId { get; set; }
        public IEnumerable<SelectListItem> FonteListItems { get; set; }

        [Display(Name = "Nº do Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Nº do Processo (Siafisico)")]
        public string NumeroProcessoSiafisico { get; set; }


        public PesquisaReservaEstruturaViewModel CreateInstance(IEmpenho objModel, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<Estrutura> estrutura, IEnumerable<Fonte> fonte, IEnumerable<int> anos)
        {
            var _programasAnoAtual = programa.Where(x => x.Ano == objModel.NumeroAnoExercicio);
            var _ptres = programa.Where(w => w.Codigo == objModel.ProgramaId).SingleOrDefault()?.Ptres;

            var cpfAnterior = programa.FirstOrDefault(x => x.Codigo == objModel.ProgramaId);
            var cpfAtual = _programasAnoAtual.FirstOrDefault(x => x.Cfp.Equals(cpfAnterior?.Cfp));

            var naturezaAnterior = estrutura.FirstOrDefault(x => x.Codigo == objModel.NaturezaId);
            
            var _naturezasDoProgramaAnterior = estrutura.Where(x => x.Programa == cpfAnterior?.Codigo || cpfAnterior?.Codigo == 0);
            var _naturezasDoProgramaAtual = estrutura.Where(x => x.Programa == cpfAtual?.Codigo || cpfAtual?.Codigo == 0);

            var naturezaAtual = _naturezasDoProgramaAtual.FirstOrDefault(x => x.Natureza.Equals(naturezaAnterior?.Natureza) && x.Fonte == naturezaAnterior?.Fonte);
            var _naturezaAtualId = _naturezasDoProgramaAtual.FirstOrDefault(w => w.Natureza == naturezaAnterior?.Natureza && w.Fonte == naturezaAnterior?.Fonte)?.Codigo;

            var retorno = new PesquisaReservaEstruturaViewModel();

            retorno.NumeroAnoExercicio = objModel.NumeroAnoExercicio > 0 ? objModel.NumeroAnoExercicio.ToString() : default(string);
            retorno.NumeroAnoExercicioListItems = anos
            .Select(s => new SelectListItem
            {
                Text = s.ToString(),
                Value = s.ToString(),
                Selected = s == objModel.NumeroAnoExercicio
            })
            .ToList();

            retorno.RegionalId = objModel.RegionalId > 0 ? objModel.RegionalId.ToString() : default(string);
            retorno.RegionalListItems = regional.Where(x => x.Id > 1)
            .Select(s => new SelectListItem
            {
                Text = s.Descricao,
                Value = s.Id.ToString(),
                Selected = s.Id == objModel.RegionalId
            })
            .ToList();

            retorno.CodigoPtres = _ptres;
            retorno.CodigoPtresListItems = _programasAnoAtual.OrderBy(x => x.Ptres)
            .Select(s => new SelectListItem
            {
                Text = s.Ptres,
                Value = s.Ptres,
                Selected = s.Ptres == _ptres
            })
            .ToList();

            retorno.ProgramaId = objModel.ProgramaId > 0 ? cpfAtual?.Codigo.ToString() : default(string);
            retorno.ProgramaListItems = _programasAnoAtual.OrderBy(x => x.Cfp)
            .Select(s => new SelectListItem
            {
                Text = CfpValueObject.Formatar(s.Cfp, s.Descricao),
                Value = s.Codigo.ToString(),
                Selected = s.Codigo == objModel.ProgramaId
            })
            .ToList();
            
            retorno.NaturezaId = Convert.ToString(_naturezaAtualId);
            retorno.NaturezaListItems = _naturezasDoProgramaAtual.OrderBy(x => x.Natureza)
            .Select(s => new SelectListItem
            {
                Text = NaturezaValueObject.Formatar(s.Natureza, s.Fonte),
                Value = s.Codigo.ToString(),
                Selected = s.Codigo == _naturezaAtualId
            })
            .ToList();

            retorno.FonteId = objModel.FonteId > 0 ? objModel.FonteId.ToString() : default(string);
            retorno.FonteListItems = fonte.OrderBy(x => x.Codigo)
            .Select(s => new SelectListItem
            {
                Text = s.Codigo,
                Value = s.Id.ToString(),
                Selected = s.Codigo == FonteId
            })
            .ToList();

            retorno.NumeroProcesso = objModel.NumeroProcesso;
            retorno.NumeroProcessoSiafisico = objModel.NumeroProcessoSiafisico;
            

            return retorno;
        }
    }
}