using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Core.Services.PagamentoDer;
using Sids.Prodesp.Model.Interface.Base;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ReclassificacaoRetencaoService : CommonService
    {

        private readonly ICrudReclassificacaoRetencao _repository;
        private readonly SiafemPagamentoContaUnicaService _siafem;
        private readonly ReclassificacaoRetencaoNotaService _notas;
        private readonly ReclassificacaoRetencaoEventoService _eventos;
        private readonly NlParametrizacaoService _parametrizacaoService;
        private readonly ChaveCicsmoService _chave;
        private readonly DocumentoTipoService _docRepository;



        public ReclassificacaoRetencaoService(ILogError log, ICommon common, IChaveCicsmo chave,
                ICrudReclassificacaoRetencao repository, ICrudPagamentoContaUnicaNota<ReclassificacaoRetencaoNota> notas, ICrudPagamentoContaUnicaEvento<ReclassificacaoRetencaoEvento> eventos, ISiafemPagamentoContaUnica siafem
            , NlParametrizacaoService parametrizacaoService)
                : base(log, common, chave)
        {

            _siafem = new SiafemPagamentoContaUnicaService(log, siafem);
            _notas = new ReclassificacaoRetencaoNotaService(log, notas);
            _eventos = new ReclassificacaoRetencaoEventoService(log, eventos);
            _parametrizacaoService = parametrizacaoService;
            _chave = new ChaveCicsmoService(log, chave);
            _repository = repository;
            _docRepository = new DocumentoTipoService(new DocumentoTipoDal());
        }


        public AcaoEfetuada Excluir(ReclassificacaoRetencao entity, int recursoId, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"ReclassificacaoRetencao : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }
        public int SalvarOuAlterar(ReclassificacaoRetencao entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (entity.Eventos.Any())
                    entity.Eventos = SalvarOuAlterarEventos(entity, recursoId, action);

                if (entity.Notas.Any())
                    entity.Notas = SalvarOuAlterarNotas(entity, recursoId, action);

                if (recursoId > 0) LogSucesso(action, recursoId, $@"
                    Nº do Reclassificacao / Retencao SIAFEM {entity.NumeroSiafem}.");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        private IEnumerable<ReclassificacaoRetencaoNota> SalvarOuAlterarNotas(ReclassificacaoRetencao entity, int recursoId, short action)
        {
            var salvos = _notas.Buscar(new ReclassificacaoRetencaoNota { IdReclassificacaoRetencao = entity.Id });
            var deleta = salvos?.Where(w => entity.Notas.All(a => a.Id != w.Id));

            _notas.Excluir(deleta, recursoId, action);

            var notas = new List<ReclassificacaoRetencaoNota>();
            foreach (ReclassificacaoRetencaoNota nota in entity.Notas)
            {
                nota.IdReclassificacaoRetencao = entity.Id;
                nota.Id = _notas.SalvarOuAlterar(nota, recursoId, action);
                notas.Add(nota);
            }

            return notas;
        }


        private IEnumerable<ReclassificacaoRetencaoEvento> SalvarOuAlterarEventos(ReclassificacaoRetencao entity, int recursoId, short action)
        {
            var salvos = _eventos.Buscar(new ReclassificacaoRetencaoEvento { PagamentoContaUnicaId = entity.Id });
            var deleta = salvos?.Where(w => entity.Eventos.All(a => a.Id != w.Id));

            if (deleta.Any())
                _eventos.Excluir(deleta, recursoId, action);

            var eventos = new List<ReclassificacaoRetencaoEvento>();
            foreach (ReclassificacaoRetencaoEvento evento in entity.Eventos)
            {
                evento.PagamentoContaUnicaId = entity.Id;
                evento.Id = _eventos.SalvarOuAlterar(evento, recursoId, action);
                eventos.Add(evento);
            }

            return eventos;
        }

        public IEnumerable<ReclassificacaoRetencao> Listar(ReclassificacaoRetencao entity)
        {
            return _repository.Fetch(entity);
        }

            public IEnumerable<ReclassificacaoRetencao> BuscarGrid(ReclassificacaoRetencao entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
            {
                var result = _repository.FetchForGrid(entity, de, ate).ToList();
                if(result.Any())
                    result.ForEach(x => x.Eventos = _eventos.Buscar(new ReclassificacaoRetencaoEvento { PagamentoContaUnicaId = x.Id }));
                return result;
            }



        public ReclassificacaoRetencao Selecionar(int Id)
        {
            var entity = _repository.Get(Id);

            entity.Notas = _notas.Buscar(new ReclassificacaoRetencaoNota { IdReclassificacaoRetencao = entity.Id });
            entity.Eventos = _eventos.Buscar(new ReclassificacaoRetencaoEvento { PagamentoContaUnicaId = entity.Id });
            entity.DocumentoTipo = _docRepository.Listar(new DocumentoTipo { Id = entity.DocumentoTipoId }).FirstOrDefault();
            return entity;
        }


        public void Transmitir(int entityId, Usuario user, int recursoId)
        {

            var entity = Selecionar(entityId);
            if (AppConfig.WsUrl != "siafemProd")
            {
                user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
            }

            try
            {
                Transmissao(user, entity, recursoId);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
            }
        }

        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var empenhos = new List<ReclassificacaoRetencao>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                var entity = new ReclassificacaoRetencao();
                try
                {
                    entity = Selecionar(empenhoId);
                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                    }

                    Retransmissao(user, entity, recursoId);
                    empenhos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
                }
            }

            var empenhosErros = empenhos.Where(x => x.StatusSiafem == "E").ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault()?.MensagemServicoSiafem;
                else
                    result += Environment.NewLine + "; / Alguns ReclassificacaoRetencaos não puderam ser retransmitidos";


            return result;
        }

        private void Transmissao(Usuario user, ReclassificacaoRetencao entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        private Error Retransmissao(Usuario user, ReclassificacaoRetencao entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                { if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId); }
                catch (Exception ex)
                { error.SiafemSiafisico = ex.Message; }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
            return error;
        }


        private void TransmitirSiafem(ReclassificacaoRetencao entity, Usuario user, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                entity.NumeroSiafem = _siafem.InserirReclassificacaoRetencao(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);
                entity.TransmitidoSiafem = true;
                entity.StatusSiafem = "S";
                entity.DataTransmitidoSiafem = DateTime.Now;
                entity.MensagemServicoSiafem = null;

                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                entity.StatusSiafem = "E";
                entity.MensagemServicoSiafem = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        internal class Error
        {
            public string Prodesp { get; set; }
            public string SiafemSiafisico { get; set; }
        }
    }
}
