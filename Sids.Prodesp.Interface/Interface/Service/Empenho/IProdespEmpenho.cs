namespace Sids.Prodesp.Interface.Interface.Service.Empenho
{
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.ValueObject.Service.Prodesp.Common;
    using Model.ValueObject.Service.Prodesp.Reserva;
    using System.Collections.Generic;

    public interface IProdespEmpenho
    {
        string InserirEmpenho(string chave, string senha, Empenho objModel, IEnumerable<IMes> mes, Programa programa, Estrutura estrutura, Fonte fonte, Regional regional);

        string InserirEmpenhoReforco(string chave, string senha, EmpenhoReforco objModel, IEnumerable<IMes> mes, Fonte fonte);

        string InserirEmpenhoCancelamento(string chave, string senha, EmpenhoCancelamento objModel, IEnumerable<IMes> mes, Fonte fonte);

        bool InserirDoc(string chave, string senha, IEmpenho objModel, string tipo);

        ConsultaContrato ConsultaContrato(string chave, string senha, string contrato);

        ConsultaEmpenhoEstrutura ConsultaEmpenhoEstrutura(int anoExercicio, string regional, string cfp, string natureza, int programa, string origemRecurso, string processo, string chave, string senha);

        ConsultaReserva ConsultaReserva(string chave, string senha, string reserva);

        ConsultaEmpenho ConsultaEmpenho(string chave, string senha, string reserva);

        ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string chave, string senha, string despesa);

        ConsultaAssinatura ConsultaAssinatura(string chave, string senha, string assinatura, int tipo);
    }
}
