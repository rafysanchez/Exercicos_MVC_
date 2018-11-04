using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoDer;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa;
using System.Collections.Specialized;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.ValueObject;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.FormaGerarNl;

namespace Sids.Prodesp.Core.Services.PagamentoDer
{
    public class NlParametrizacaoService : Base.BaseService
    {
        private NlParametrizacaoDal _parametrizacaoDal;
        private NlParametrizacaoEventoDal _parametrizacaoEventoDal;
        private NlParametrizacaoDespesaDal _parametrizacaoDespesaDal;
        private NlParametrizacaoDespesaTipoDal _parametrizacaoDespesaTipoDal;
        private NlParametrizacaoNlTipoDal _nlParametrizacaoNlTipoDal;
        private ParaRestoPagarDal _paraRestoPagarDal;
        private NlParametrizacaoFormaGerarNlDal _parametrizacaoFormaGerarNlDal;
        private readonly ICrudDocumentoTipo _tipoDocumentoDal;

        public NlParametrizacaoService(ILogError log,
            NlParametrizacaoDal parametrizacaoDal,
            NlParametrizacaoDespesaDal parametrizacaoDespesaDal,
            NlParametrizacaoDespesaTipoDal parametrizacaoDespesaTipoDal,
            NlParametrizacaoEventoDal parametrizacaoEventoDal,
            NlParametrizacaoNlTipoDal nlParametrizacaoNlTipoDal,
            ParaRestoPagarDal paraRestoPagarDal,
            NlParametrizacaoFormaGerarNlDal parametrizacaoFormaGerarNlDal,
            ICrudDocumentoTipo tipoDocumentoDal) : base(log)
        {
            this._parametrizacaoDal = parametrizacaoDal;
            this._parametrizacaoDespesaDal = parametrizacaoDespesaDal;
            this._parametrizacaoDespesaTipoDal = parametrizacaoDespesaTipoDal;
            this._parametrizacaoEventoDal = parametrizacaoEventoDal;
            this._nlParametrizacaoNlTipoDal = nlParametrizacaoNlTipoDal;
            this._paraRestoPagarDal = paraRestoPagarDal;
            this._tipoDocumentoDal = tipoDocumentoDal;
            this._parametrizacaoFormaGerarNlDal = parametrizacaoFormaGerarNlDal;
        }

        public AcaoEfetuada Salvar(NlParametrizacao entity, int? recursoId)
        {
            try
            {
                if (entity.Id == 0)
                {
                    var id = _parametrizacaoDal.Add(entity);
                    entity.Id = id;
                }
                else
                {
                    ExcluirEventosNaoListados(entity);
                    ExcluirDespesasNaoListadas(entity);

                    _parametrizacaoDal.Edit(entity);
                }

                foreach (var evento in entity.Eventos)
                {
                    evento.IdNlParametrizacao = entity.Id;
                    SalvarEvento(evento);
                }

                foreach (var despesa in entity.Despesas)
                {
                    despesa.IdNlParametrizacao = entity.Id;
                    SalvarDespesa(despesa);
                }

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, 1, recursoId);
            }
        }

        public NlParametrizacao Selecionar(NlParametrizacao filtro)
        {

            var entity = this._parametrizacaoDal.Fetch(filtro).FirstOrDefault();

            if (entity == null)
                return null;
            var eventos = this._parametrizacaoEventoDal.Fetch(new NlParametrizacaoEvento() { IdNlParametrizacao = entity.Id });
            entity.Eventos = eventos;

            var despesas = this._parametrizacaoDespesaDal.Fetch(new NlParametrizacaoDespesa() { IdNlParametrizacao = entity.Id });
            entity.Despesas = despesas;

            return entity;
        }
        public List<NlParametrizacao> ObterTodas()
        {

            List<NlParametrizacao> entities = this._parametrizacaoDal.Fetch(new NlParametrizacao()).ToList();

            if (!entities.Any())
                return new List<NlParametrizacao>();


            foreach (NlParametrizacao entity in entities)
            {
                var eventos = this._parametrizacaoEventoDal.Fetch(new NlParametrizacaoEvento() { IdNlParametrizacao = entity.Id });
                entity.Eventos = eventos;

                var despesas = this._parametrizacaoDespesaDal.Fetch(new NlParametrizacaoDespesa() { IdNlParametrizacao = entity.Id });
                entity.Despesas = despesas;

            }

            return entities;
        }

        private void ExcluirEventosNaoListados(NlParametrizacao entity)
        {
            var eventos = _parametrizacaoEventoDal.Fetch(new NlParametrizacaoEvento() { IdNlParametrizacao = entity.Id });
            foreach (var evento in eventos)
            {
                if (!entity.Eventos.Any(x => x.Id == evento.Id))
                {
                    _parametrizacaoEventoDal.Remove(evento.Id);
                }
            }
        }

        private void ExcluirDespesasNaoListadas(NlParametrizacao entity)
        {
            var despesas = _parametrizacaoDespesaDal.Fetch(new NlParametrizacaoDespesa() { IdNlParametrizacao = entity.Id });
            foreach (var despesa in despesas)
            {
                if (!entity.Despesas.Any(x => x.Id == despesa.Id))
                {
                    _parametrizacaoDespesaDal.Remove(despesa.Id);
                }
            }
        }

        private void SalvarEvento(NlParametrizacaoEvento evento)
        {
            if (evento.Id == 0)
            {
                _parametrizacaoEventoDal.Add(evento);
            }
            else
            {
                _parametrizacaoEventoDal.Edit(evento);
            }
        }

        private void SalvarDespesa(NlParametrizacaoDespesa despesa)
        {
            if (despesa.Id == 0)
            {
                _parametrizacaoDespesaDal.Add(despesa);
            }
            else
            {
                _parametrizacaoDespesaDal.Edit(despesa);
            }
        }

        public AcaoEfetuada Excluir(NlParametrizacao entity, int recursoId)
        {
            try
            {
                _parametrizacaoDal.Remove(entity.Id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, 1, recursoId);
            }

        }

        public ResultadoOperacaoObjetoVo<ConsultaDespesaNlVo> VerificarTipoDespesa(int tipoNl, int codigo, int? recursoId)
        {
            ResultadoOperacaoObjetoVo<ConsultaDespesaNlVo> resposta = new ResultadoOperacaoObjetoVo<ConsultaDespesaNlVo>();

            try
            {
                var retornoBd = _parametrizacaoDal.ObterTipoNlDaDespesa(codigo);

                if (retornoBd == null || retornoBd.IdTipoNl == tipoNl || retornoBd.IdTipoNl == 0)
                {
                    resposta.Sucesso = true;
                }
                else
                {
                    resposta.Sucesso = false;
                    resposta.Objeto = retornoBd;
                    resposta.Mensagem = String.Format("Tipo de Despesa utilizada no tipo de NL {0}", resposta.Objeto.TipoNlDescricao);
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, 1, recursoId);
            }

            return resposta;
        }

        public IEnumerable<NlParametrizacao> Consultar(NlParametrizacao filtro, int recursoId)
        {
            return _parametrizacaoDal.Fetch(filtro);
        }

        public IEnumerable<NlParametrizacaoEvento> Eventos(NlParametrizacaoEvento filtro, int recursoId)
        {
            return _parametrizacaoEventoDal.Fetch(filtro);
        }

        public IEnumerable<NlParametrizacaoDespesa> Despesas(NlParametrizacaoDespesa filtro, int recursoId)
        {
            return _parametrizacaoDespesaDal.Fetch(filtro);
        }

        public IEnumerable<NlParametrizacaoDespesaTipo> DespesaTipos(int recursoId)
        {
            var tipos = _parametrizacaoDespesaTipoDal.Fetch(new NlParametrizacaoDespesaTipo()).ToList();

            for (int i = 0; i < tipos.Count; i++)
            {
                tipos.ElementAt(i).Descricao = tipos.ElementAt(i).Codigo + " - " + tipos.ElementAt(i).Descricao;
            }

            return tipos;
        }

        public IEnumerable<NlTipo> TiposNl(int recursoId)
        {
            return _nlParametrizacaoNlTipoDal.Fetch(new NlTipo());
        }

        public IEnumerable<ParaRestoAPagar> TiposRap(int recursoId)
        {
            return _paraRestoPagarDal.Fatch(new ParaRestoAPagar());
        }

        public IEnumerable<DocumentoTipo> TiposDocumento(int recursoId)
        {
            return _tipoDocumentoDal.Fatch(new DocumentoTipo());
        }

        public IEnumerable<FormaGerarNl> TipoFormaGerarNl(int recursoId)
        {
            return _parametrizacaoFormaGerarNlDal.Fetch(new FormaGerarNl());
        }
    }
}
