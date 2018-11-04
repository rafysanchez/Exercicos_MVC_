namespace Sids.Prodesp.Core.Services.WebServie
{
    using Base;
    using Interface.Interface.Configuracao;
    using Interface.Interface.Log;
    using Interface.Interface.Reserva;
    using Interface.Interface.Seguranca;
    using Interface.Interface.Service;
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.ValueObject.Service.Reserva.Prodesp;
    using System.Collections.Generic;
    using System.Linq;

    public class ProdespService : BaseService
    {
        private readonly ICrudEstrutura _estutura;
        private readonly ICrudFonte _fonte;
        private readonly IProdesp _prodesp;
        private readonly ICrudPrograma _programa;
        private readonly IRegional _regional;

        public ProdespService(ILogError logError, IProdesp prodesp, ICrudPrograma programa, ICrudFonte fonte,
            ICrudEstrutura estutura, IRegional regional) : base(logError)
        {
            _estutura = estutura;
            _fonte = fonte;
            _prodesp = prodesp;
            _programa = programa;
            _regional = regional;
        }
        
        public string InserirReserva(Reserva reserva, List<IMes> mes, string chave, string senha)
        {
            var programa = _programa.Buscar(new Programa { Codigo = (int)reserva.Programa }).FirstOrDefault();
            var fonte = _fonte.Buscar(new Fonte { Id = (int)reserva.Fonte }).FirstOrDefault();
            var estrutura = _estutura.Buscar(new Estrutura { Codigo = (int)reserva.Estrutura }).FirstOrDefault();
            var regional = _regional.Buscar(new Regional { Id = (int)reserva.Regional }).FirstOrDefault();

            return _prodesp.InserirReserva(chave, senha, reserva, mes, programa, estrutura, fonte, regional);
        }

        public string InserirReservaReforco(ReservaReforco reforco, List<IMes> mes, string chave, string senha)
        {
            var regional = _regional.Buscar(new Regional { Id = (int)reforco.Regional }).FirstOrDefault();

            return _prodesp.InserirReservaReforco(chave, senha, reforco, mes, regional);
        }
        
        public string InserirReservaCancelamento(ReservaCancelamento cancelamento, List<IMes> mes, string chave, string senha)
        {
            return _prodesp.InserirReservaCancelamento(chave, senha, cancelamento, mes);
        }

        public bool InserirDoc(IReserva reserva, string chave, string senha, string tipo)
        {
            return _prodesp.InserirDoc(chave, senha, reserva, tipo);
        }

        public ConsultaContrato ConsultaContrato(string contrato, string chave, string senha)
        {
            return _prodesp.ConsultaContrato(chave, senha, contrato);
        }

        public ConsultaReservaEstrutura ConsultaReservaEstrutura(
            int anoExercicio, short regionalId, string cfp, string natureza, int programa,string origemRecurso, string processo, string chave, string senha)
        {

            var regional = _regional.Buscar(new Regional { Id = regionalId }).FirstOrDefault();

            return _prodesp.ConsultaReservaEstrutura(
                anoExercicio, regional.Descricao, cfp, natureza, programa,
                origemRecurso, processo, chave, senha);
        }

        public ConsultaReserva ConsultaReserva(string reserva, string chave, string senha)
        {
            return _prodesp.ConsultaReserva(chave, senha, reserva);
        }

        public ConsultaEmpenho ConsultaEmpenho(string empenho, string chave, string senha)
        {
            return _prodesp.ConsultaEmpenho(chave, senha, empenho);
        }

        public ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string especificacao, string chave, string senha)
        {
            return _prodesp.ConsultaEspecificacaoDespesa(chave, senha, especificacao);
        }

        public ConsultaAssinatura ConsultaAssinatura(string assinatura, int tipo, string chave, string senha)
        {
            return _prodesp.ConsultaAssinatura(chave, senha, assinatura, tipo);
        }
    }
}
