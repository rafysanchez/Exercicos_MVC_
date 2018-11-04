namespace Sids.Prodesp.UI.Areas.Reserva.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface.Base;
    using Model.Interface.Reserva;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IReservaViewModel
    {
        string Ptres { get; set; }
        string Cfp { get; set; }
        string Natureza { get; set; }
        string NumRetornoProdesp { get; set; }
        string NumProdesp { get; set; }
        string NumSiafemSiafisco { get; set; }
        string Processo { get; set; }
        IList<SelectListItem> Anos { get; set; }
        IList<Programa> Programas { get; set; }
        IList<Estrutura> Estrutura { get; set; }
        IList<Regional> Regional { get; set; }
        IList<IMes> Mes { get; set; }
        IList<SelectListItem> Fontes { get; set; }
        IList<Fonte> Fonte { get; set; }
        IList<TipoReserva> TipoReservas { get; set; }

        string Mes1 { get; set; }
        string Mes2 { get; set; }
        string Mes3 { get; set; }
        string Mes4 { get; set; }
        string Mes5 { get; set; }
        string Mes6 { get; set; }
        string Mes7 { get; set; }
        string Mes8 { get; set; }
        string Mes9 { get; set; }
        string Mes10 { get; set; }
        string Mes11 { get; set; }
        string Mes12 { get; set; }

        IReservaViewModel GerarViewModel(IReserva entity);
    }
}
