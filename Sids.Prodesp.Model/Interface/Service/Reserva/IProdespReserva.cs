namespace Sids.Prodesp.Model.Interface.Service.Reserva
{
    using Base;
    using Entity.Configuracao;
    using Entity.Reserva;
    using Entity.Seguranca;
    using Interface.Reserva;
    using System.Collections.Generic;
    using ValueObject.Service.Prodesp.Common;
    using ValueObject.Service.Prodesp.Reserva;

    public interface IProdespReserva
    {
        string InserirReserva(string key, string password, Reserva entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional);
        string InserirReservaReforco(string key, string password, ReservaReforco entity, IEnumerable<IMes> months, Regional regional);
        string InserirReservaCancelamento(string key, string password, ReservaCancelamento entity, IEnumerable<IMes> months);



        bool InserirDoc(string key, string password, IReserva entity, string type);
        ConsultaContrato ConsultaContrato(string key, string password, string contract, string type);
        ConsultaReservaEstrutura ConsultaReservaEstrutura(string key, string password, int year, string regional, string cfp, string nature, int program, string resourceSource, string process);
        ConsultaReserva ConsultaReserva(string key, string password, string reservaNumber);
        ConsultaEmpenho ConsultaEmpenho(string key, string password, string reservaNumber);
        ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string key, string password, string despesa);
        ConsultaAssinatura ConsultaAssinatura(string key, string password, string signatures, int type);
    }
}
