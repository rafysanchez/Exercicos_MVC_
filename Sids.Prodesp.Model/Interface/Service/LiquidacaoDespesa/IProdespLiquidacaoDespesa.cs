using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.Configuracao;
using Sids.Prodesp.Model.Interface.Seguranca;

namespace Sids.Prodesp.Model.Interface.Service.LiquidacaoDespesa
{
    using Entity.Configuracao;
    using Entity.LiquidacaoDespesa;
    using Interface.LiquidacaoDespesa;
    using ValueObject.Service.Prodesp.Common;

    public interface IProdespLiquidacaoDespesa
    {
        string InserirSubEmpenho(string chave, string senha, Subempenho subempenho, Estrutura estrutura);
        string InserirSubEmpenhoCancelamento(string chave, string senha, SubempenhoCancelamento entity);
        bool InserirDoc(string chave, string senha, ILiquidacaoDespesa objModel, string tipo);
        object InserirSubEmpenhoApoio(string chave, string senha, Subempenho entity);
        object ConsultarSubEmpenhoApoio(string chave, string senha, SubempenhoCancelamento entity);


        object ConsultaInclusaoEmpenhoCredor(string chave, string senha, Subempenho objModel);
        string InserirRapInscricao(string key, string password, IRap entity);

        string InserirRapRequisicao(string key, string password, IRap entity);

        object RapRequisicaoApoio(string key, string password, IRap entity, ICrudPrograma programa, ICrudEstrutura estrutura, IRegional regional);
        object RapAnulacaoApoio(string key, string password, string numRequisicaoRap, ICrudPrograma programa, ICrudEstrutura estrutura, IRegional regional);

        object ConsultaEmpenhoRap(string key, string password, IRap entity);


        ConsultaSubempenho ConsultaSubempenho(string chave, string senha, string subempenho);

        object ConsultaEmpenhoSaldo(string key, string password, IRap entity, Regional regional);

        string InserirRapAnulacao(string key, string password, RapAnulacao entity);
    }
}
