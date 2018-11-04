namespace Sids.Prodesp.UI.Areas.Reserva.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface.Base;
    using Model.Interface.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IFiltroViewModel
    {
        string Ptres { get; set; }
        string Cfp { get; set; }
        int Programa { get; set; }
        int Natureza { get; set; }
        string NumProdesp { get; set; }
        string Processo { get; set; }
        int? Obra { get; set; }
        string Contrato { get; set; }
        string NumSiafemSiafisico { get; set; }
        string Ugo { get; set; }
        bool? TransmitidoSiafem { get; set; }
        bool? TransmitidoSiafisico { get; set; }
        string StatusSiafemSiafisico { get; set; }
        DateTime? DataEmissaoDe { get; set; }
        DateTime? DataEmissaoAte { get; set; }
        int RegionalId { get; set; }
        IList<SelectListItem> Anos { get; set; }
        int? AnoExercicio { get; set; }
        IList<Programa> Programas { get; set; }
        IList<Estrutura> Estrutura { get; set; }
        IList<TipoReserva> TipoReserva { get; set; }
        int? Tipo { get; set; }
        IList<Regional> Regional { get; set; }
        IList<IMes> Mes { get; set; }
        IList<SelectListItem> Fontes { get; set; }
        IList<IReserva> Reservas { get; set; }
        string StatusProdesp { get; set; }
        IList<Fonte> Fonte { get; set; }

        IList<SelectListItem> StatusProdespListItems { get; set; }
        bool ExibirTipo { get; set; }

        IFiltroViewModel GerarFiltro(IReserva reserva);        
    }
}
