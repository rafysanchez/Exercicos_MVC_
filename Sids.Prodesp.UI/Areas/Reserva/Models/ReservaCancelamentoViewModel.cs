using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.Interface.Reserva;

namespace Sids.Prodesp.UI.Areas.Reserva.Models
{
    public class ReservaCancelamentoViewModel : IReservaViewModel
    {
        public string Ptres { get; set; }
        public string Cfp { get; set; }
        public string Natureza { get; set; }
        public string NumRetornoProdesp { get; set; }
        public string NumProdesp { get; set; }
        public string NumSiafemSiafisco { get; set; }
        public string Processo { get; set; }
        public IList<SelectListItem> Anos { get; set; }
        public IList<Programa> Programas { get; set; }
        public IList<Estrutura> Estrutura { get; set; }
        public IList<Regional> Regional { get; set; }
        public IList<IMes> Mes { get; set; }
        public IList<SelectListItem> Fontes { get; set; }
        public IList<Fonte> Fonte { get; set; }
        public string Mes1 { get; set; }
        public string Mes2 { get; set; }
        public string Mes3 { get; set; }
        public string Mes4 { get; set; }
        public string Mes5 { get; set; }
        public string Mes6 { get; set; }
        public string Mes7 { get; set; }
        public string Mes8 { get; set; }
        public string Mes9 { get; set; }
        public string Mes10 { get; set; }
        public string Mes11 { get; set; }
        public string Mes12 { get; set; }

        public IList<TipoReserva> TipoReservas { get; set; }

        public IReservaViewModel GerarViewModel(IReserva entity)
        {
            var programas = App.ProgramaService.Listar(new Programa()).ToList();
            var programa = programas.FirstOrDefault(x => x.Codigo == entity.Programa) ?? new Programa();
            var estruturas = App.EstruturaService.Listar(new Estrutura()).ToList();
            var estrutura = estruturas.FirstOrDefault(x => x.Codigo == entity.Estrutura);
            var mes = App.CancelamentoMesService.Buscar(new ReservaCancelamentoMes { Id = entity.Codigo }).Cast<IMes>().ToList();

            IReservaViewModel viewModel = new ReservaCancelamentoViewModel();
            viewModel.Anos = App.ReservaCancelamentoService.ObterAnos();
            viewModel.Estrutura = estruturas;
            viewModel.Programas = programas;
            viewModel.Fonte = App.FonteService.Listar(new Fonte()).ToList();

            viewModel.Fontes = App.DestinoService.Buscar(new Destino()).Select(x => new SelectListItem
            {
                Value = x.Codigo,
                Text = x.Descricao
            }).ToList();

            viewModel.Regional = App.RegionalService.Buscar(new Regional()).Where(x => x.Id > 1).ToList();
            viewModel.Ptres = programa?.Ptres;
            viewModel.Cfp = programa?.Cfp;
            viewModel.Natureza = string.Concat(estrutura?.Natureza, " - ", estrutura?.Fonte);
            viewModel.Mes = mes;
            viewModel.Mes1 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("01"))?.ValorMes / 100));
            viewModel.Mes2 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("02"))?.ValorMes / 100));
            viewModel.Mes3 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("03"))?.ValorMes / 100));
            viewModel.Mes4 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("04"))?.ValorMes / 100));
            viewModel.Mes5 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("05"))?.ValorMes / 100));
            viewModel.Mes6 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("06"))?.ValorMes / 100));
            viewModel.Mes7 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("07"))?.ValorMes / 100));
            viewModel.Mes8 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("08"))?.ValorMes / 100));
            viewModel.Mes9 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("09"))?.ValorMes / 100));
            viewModel.Mes10 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("10"))?.ValorMes / 100));
            viewModel.Mes11 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("11"))?.ValorMes / 100));
            viewModel.Mes12 = Convert.ToString(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", mes.FirstOrDefault(f => f.Descricao.Equals("12"))?.ValorMes / 100));
            return viewModel;
        }

    }
}