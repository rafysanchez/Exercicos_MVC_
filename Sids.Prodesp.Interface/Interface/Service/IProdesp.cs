namespace Sids.Prodesp.Interface.Interface.Service
{
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.ValueObject.Service.Reserva.Prodesp;
    using System.Collections.Generic;

    public interface IProdesp
    {
        string InserirReserva(
            string chave, string senha, Reserva reserva, List<IMes> reservaMes, 
            Programa programa, Estrutura estrutura, Fonte fonte, Regional regional);

        string InserirReservaReforco(
            string chave, string senha, ReservaReforco reserva, 
            List<IMes> reforcoMes, Regional regional);

        string InserirReservaCancelamento(
            string chave, string senha, ReservaCancelamento cancelamento, 
            List<IMes> cancelamentoMeses);

        bool InserirDoc(string chave, string senha, IReserva reserva, string tipo);

        ConsultaContrato ConsultaContrato(string chave, string senha, string contrato);

        ConsultaReservaEstrutura ConsultaReservaEstrutura(
            int anoExercicio, string regional, string cfp, string natureza, int programa,
            string origemRecurso, string processo, string chave, string senha);

        ConsultaReserva ConsultaReserva(string chave, string senha, string reserva);

        ConsultaEmpenho ConsultaEmpenho(string chave, string senha, string reserva);

        ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string chave, string senha, string despesa);

        ConsultaAssinatura ConsultaAssinatura(string chave, string senha, string assinatura, int tipo);
    }
}
