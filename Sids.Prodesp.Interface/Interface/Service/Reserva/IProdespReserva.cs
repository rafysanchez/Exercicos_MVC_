using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Reserva;

namespace Sids.Prodesp.Interface.Interface.Service.Reserva
{
    public interface IProdespReserva
    {
        string InserirReserva(
            string chave, string senha, Model.Entity.Reserva.Reserva reserva, List<IMes> reservaMes, 
            Programa programa, Estrutura estrutura, Fonte fonte, Regional regional);

        string InserirReservaReforco(
            string chave, string senha, ReservaReforco reserva, 
            List<IMes> reforcoMes, Regional regional);

        string InserirReservaCancelamento(
            string chave, string senha, ReservaCancelamento cancelamento, 
            List<IMes> cancelamentoMeses);

        bool InserirDoc(string chave, string senha, IReserva reserva, string tipo);

        ConsultaContrato ConsultaContrato(string chave, string senha, string contrato);

        ConsultaReservaEstrutura ConsultaReservaEstrutura(int anoExercicio, string regional, string cfp, string natureza, int programa,string origemRecurso, string processo, string chave, string senha);

        ConsultaReserva ConsultaReserva(string chave, string senha, string reserva);

        ConsultaEmpenho ConsultaEmpenho(string chave, string senha, string reserva);

        ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string chave, string senha, string despesa);

        ConsultaAssinatura ConsultaAssinatura(string chave, string senha, string assinatura, int tipo);
    }
}
