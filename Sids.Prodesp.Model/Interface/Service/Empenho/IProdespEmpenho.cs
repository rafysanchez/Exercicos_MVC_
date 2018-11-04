namespace Sids.Prodesp.Model.Interface.Service.Empenho
{
    using Base;
    using Entity.Configuracao;
    using Entity.Empenho;
    using Entity.Seguranca;
    using Interface.Empenho;
    using System.Collections.Generic;
    using ValueObject.Service.Prodesp.Common;

    public interface IProdespEmpenho
    {
        string InserirEmpenho(string key, string password, Empenho entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional);

        string InserirEmpenhoReforco(string key, string password, EmpenhoReforco entity, IEnumerable<IMes> months, Fonte source);

        string InserirEmpenhoCancelamento(string key, string password, EmpenhoCancelamento entity, IEnumerable<IMes> months, Fonte source);

        bool InserirDoc(string key, string password, IEmpenho entity, string type);

        ConsultaEmpenhoEstrutura ConsultaEmpenhoEstrutura(string key, string password, int year, string regional, string cfp, string nature, int program, string resourceSource, string process);
    }
}
