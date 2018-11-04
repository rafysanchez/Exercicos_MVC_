using Sids.Prodesp.Infrastructure.DataBase.Configuracao;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.Seguranca;

namespace Sids.Prodesp.Core.Services.WebService.LiquidacaoDespesa
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.Configuracao;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using Model.Interface.Service.LiquidacaoDespesa;
    using Model.ValueObject.Service.Prodesp.Common;
    using System.Linq;

    public class ProdespLiquidacaoDespesaService : BaseService
    {
        private readonly ICrudEstrutura _estutura;
        private readonly IProdespLiquidacaoDespesa _prodesp;
        private readonly ICrudPrograma _programa;
        private readonly IRegional _regional;


        public ProdespLiquidacaoDespesaService(ILogError logError, IProdespLiquidacaoDespesa prodesp,ICrudEstrutura estutura) : base(logError)
        {
            _estutura = estutura;
            _prodesp = prodesp;
            _estutura = estutura;
            _prodesp = prodesp;
            _programa = new ProgramaDal();
            _regional = new RegionalDal();
        }


        public string InserirSubEmpenho(Subempenho objModel,string chave, string senha)
        {
            var estrutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault();

            return _prodesp.InserirSubEmpenho(chave, senha, objModel, estrutura);
        }
        public string InserirAnulacaoSubEmpenho(SubempenhoCancelamento objModel, string chave, string senha)
        {
            return _prodesp.InserirSubEmpenhoCancelamento(chave, senha, objModel);
        }
        public object InserirSubEmpenhoApoio(Subempenho objModel, string chave, string senha)
        {
            return _prodesp.InserirSubEmpenhoApoio(chave, senha, objModel);
        }


        public string InserirRapInscricao(IRap entity, string key, string password)
        {
            return _prodesp.InserirRapInscricao(key, password, entity);
        }

        public object RapRequisicaoApoio(IRap entity, string key, string password)
        {
            return _prodesp.RapRequisicaoApoio(key, password, entity, _programa, _estutura, _regional);
        }

        public object ConsultarEmpenhoRap(IRap entity, string key, string password)
        {
            return _prodesp.ConsultaEmpenhoRap(key, password, entity);
        }

        public object RapAnulacaoApoio(string numRequisicaoRap, string key, string password)
        {
            return _prodesp.RapAnulacaoApoio(key, password, numRequisicaoRap, _programa, _estutura, _regional);
        }

        public object AnularSubEmpenhoApoio(SubempenhoCancelamento objModel, string chave, string senha)
        {
            return _prodesp.ConsultarSubEmpenhoApoio(chave, senha, objModel);
        }

        public object InserirEmpenhoCredor(Subempenho objModel, string chave, string senha)
        {
            return _prodesp.ConsultaInclusaoEmpenhoCredor(chave, senha, objModel);
        }

        public ConsultaSubempenho ConsultaSubempenho(string subempenho, string chave, string senha)
        {
            return _prodesp.ConsultaSubempenho(chave, senha, subempenho);
        }
        public bool InserirDoc(ILiquidacaoDespesa objModel, string chave, string senha, string tipo)
        {
            return _prodesp.InserirDoc(chave, senha, objModel, tipo);
        }

        
        public object ConsultaEmpenhoSaldo(IRap entity, string key, string password)
        {
            Regional regional = _regional.Fetch(new Regional {Id = entity.RegionalId}).FirstOrDefault();

            return _prodesp.ConsultaEmpenhoSaldo(key, password, entity, regional);
        }

        public string InserirRapRequisicao(RapRequisicao entity, string key, string password)
        {
            return _prodesp.InserirRapRequisicao(key, password, entity);
        }

        public string InserirRapAnulacao(RapAnulacao entity, string key, string password)
        {
            return _prodesp.InserirRapAnulacao(key, password, entity);
        }
    }
}
