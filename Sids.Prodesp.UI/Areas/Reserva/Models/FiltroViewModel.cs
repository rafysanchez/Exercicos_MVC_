namespace Sids.Prodesp.UI.Areas.Reserva.Models
{
    using Application;
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface.Base;
    using Model.Interface.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FiltroViewModel : IFiltroViewModel
    {
        public string Ptres { get; set; }
        public string Cfp { get; set; }
        public int Programa { get; set; }
        public int Natureza { get; set; }
        public string NumProdesp { get; set; }
        public string Processo { get; set; }
        public int? Obra { get; set; }
        public string Contrato { get; set; }
        public string NumSiafemSiafisico { get; set; }
        public string Ugo { get; set; }
        public bool? TransmitidoSiafem { get; set; }
        public bool? TransmitidoSiafisico { get; set; }
        public string StatusSiafemSiafisico { get; set; }
        public DateTime? DataEmissaoDe { get; set; }
        public DateTime? DataEmissaoAte { get; set; }
        public int RegionalId { get; set; }
        public IList<SelectListItem> Anos { get; set; }
        public int? AnoExercicio { get; set; }
        public IList<Programa> Programas { get; set; }
        public IList<Estrutura> Estrutura { get; set; }
        public IList<TipoReserva> TipoReserva { get; set; }
        public int? Tipo { get; set; }
        public IList<Regional> Regional { get; set; }
        public IList<IMes> Mes { get; set; }
        public IList<SelectListItem> Fontes { get; set; }
        public IList<IReserva> Reservas { get; set; }
        public string StatusProdesp { get; set; }
        public IList<Fonte> Fonte { get; set; }


        public IList<SelectListItem> StatusProdespListItems { get; set; }
        public IList<SelectListItem> ProgramaListItems { get; set; }
        public IList<SelectListItem> NaturezaListItems { get; set; }
        public IList<SelectListItem> PtresListItems { get; set; }
        public bool ExibirTipo { get; set; }

        public IFiltroViewModel GerarFiltro(IReserva reserva)
        {
            var programas = App.ProgramaService.Listar(new Programa()).ToList();
            var programa = programas.FirstOrDefault(x => x.Codigo == reserva.Programa);
            var estruturas = App.EstruturaService.Listar(new Estrutura()).ToList();
            var sourceType = reserva.GetType();

            return new FiltroViewModel
            {
                Programas = programas,
                Estrutura = estruturas,
                TipoReserva = App.TipoReservaService.Buscar(new TipoReserva()).ToList(),
                RegionalId = RegionalId,
                Regional = App.RegionalService.Buscar(new Regional()).Where(x => x.Id > 1).ToList(),
                Tipo = reserva.Tipo,
                Ptres = programa?.Ptres,
                Cfp = programa?.Cfp,
                AnoExercicio = AnoExercicio,
                StatusProdespListItems = new List<SelectListItem>() {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não Transmitido", Value = "N" } },
                Anos = App.ReservaService.ObterAnos(),
                ExibirTipo = sourceType == typeof(Reserva)
            };
        }
    }
}