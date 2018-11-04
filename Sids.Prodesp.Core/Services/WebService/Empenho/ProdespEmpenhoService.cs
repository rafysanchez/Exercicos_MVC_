namespace Sids.Prodesp.Core.Services.WebService.Empenho
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.Interface.Base;
    using Model.Interface.Configuracao;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using Model.Interface.Service.Empenho;
    using Model.ValueObject.Service.Prodesp.Common;
    using System.Collections.Generic;
    using System.Linq;

    public class ProdespEmpenhoService : BaseService
    {
        private readonly ICrudEstrutura _estutura;
        private readonly ICrudFonte _fonte;
        private readonly IProdespEmpenho _prodesp;
        private readonly ICrudPrograma _programa;
        private readonly IRegional _regional;


        public ProdespEmpenhoService(ILogError logError, IProdespEmpenho prodesp, ICrudPrograma programa, ICrudFonte fonte,
            ICrudEstrutura estutura, IRegional regional) : base(logError)
        {
            _estutura = estutura;
            _fonte = fonte;
            _prodesp = prodesp;
            _programa = programa;
            _regional = regional;
        }


        public string InserirEmpenho(Empenho objModel, IEnumerable<IMes> mes, string chave, string senha)
        {
            var programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault();
            var fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault();
            var estrutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault();
            var regional = _regional.Fetch(new Regional { Id = objModel.RegionalId }).FirstOrDefault();

            return _prodesp.InserirEmpenho(chave, senha, objModel, mes, programa, estrutura, fonte, regional);
        }

        public string InserirEmpenhoReforco(EmpenhoReforco objModel, IEnumerable<IMes> mes, string chave, string senha)
        {
            var fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault();
            return _prodesp.InserirEmpenhoReforco(chave, senha, objModel, mes, fonte);
        }

        public string InserirEmpenhoCancelamento(EmpenhoCancelamento objModel, IEnumerable<IMes> mes, string chave, string senha)
        {
            return _prodesp.InserirEmpenhoCancelamento(chave, senha, objModel, mes, 
                _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault());
        }

        public bool InserirDoc(IEmpenho objModel, string chave, string senha, string tipo)
        {
            return _prodesp.InserirDoc(chave, senha, objModel, tipo);
        }
        

        public ConsultaEmpenhoEstrutura ConsultaEmpenhoEstrutura(int anoExercicio, short regionalId, string cfp, string natureza, int programa, string origemRecurso, string processo, string chave, string senha)
        {

            var regional = _regional.Fetch(new Regional { Id = regionalId }).FirstOrDefault();

            return _prodesp.ConsultaEmpenhoEstrutura(chave, senha, anoExercicio, regional.Descricao, cfp, natureza, programa, origemRecurso, processo);
        }
    }
}
