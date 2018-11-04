using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoService : CommonService
    {
        private readonly ICrudProgramacaoDesembolso _repository;
        private readonly SiafemPagamentoContaUnicaService _siafem;
        private readonly ProdespPagamentoContaUnicaService _prodesp;
        private readonly ProgramacaoDesembolsoAgrupamentoService _agrupamento;
        private readonly ProgramacaoDesembolsoEventoService _eventos;
        private readonly ChaveCicsmoService _chave;
        private readonly DocumentoTipoService _docRepository;
        private readonly ProgramacaoDesembolsoTipoService _tipoRepository;

        public ProgramacaoDesembolsoService(ILogError log, ICommon common, IChaveCicsmo chave,
            ICrudProgramacaoDesembolso repository, ICrudProgramacaoDesembolsoAgrupamento agrupamento,
            ICrudPagamentoContaUnicaEvento<ProgramacaoDesembolsoEvento> eventos, ISiafemPagamentoContaUnica siafem, IProdespPagamentoContaUnica prodesp)
            : base(log, common, chave)
        {
            _prodesp = new ProdespPagamentoContaUnicaService(log, prodesp);
            _siafem = new SiafemPagamentoContaUnicaService(log, siafem);
            _agrupamento = new ProgramacaoDesembolsoAgrupamentoService(log, agrupamento);
            _eventos = new ProgramacaoDesembolsoEventoService(log, eventos);
            _chave = new ChaveCicsmoService(log, chave);
            _repository = repository;
            _docRepository = new DocumentoTipoService(new DocumentoTipoDal());
            _tipoRepository = new ProgramacaoDesembolsoTipoService(new ProgramacaoDesembolsoTipoDal());
        }

        public AcaoEfetuada Excluir(ProgramacaoDesembolso entity, int recursoId, short action)
        {
            try
            {
                if (entity.Agrupamentos.Count(x => x.TransmitidoSiafem) > 0)
                    throw new SidsException("Não é permitido excluir lista de PD, existem PD’s já transmitidas");
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"ProgramacaoDesembolso : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        public int SalvarOuAlterar(ProgramacaoDesembolso entity, int recursoId, short action)
        {
            try
            {
                entity.NumeroAgrupamento = entity.ProgramacaoDesembolsoTipoId == 2
                    ? _agrupamento.BuscarUltimoAgupamento() + 1
                    : 0;
                entity.Id = _repository.Save(entity);

                if (entity.Eventos != null && entity.ProgramacaoDesembolsoTipoId == 1)
                    entity.Eventos = SalvarOuAlterarEventos(entity, recursoId, action);

                if (entity.Agrupamentos != null && entity.ProgramacaoDesembolsoTipoId == 2)
                    entity.Agrupamentos = SalvarOuAlterarAgrupamentos(entity, recursoId, action);

                if (recursoId > 0) LogSucesso(action, recursoId, $@"
                    Nº do Programação de Desembolso SIAFEM {entity.NumeroSiafem}.");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        private IEnumerable<ProgramacaoDesembolsoAgrupamento> SalvarOuAlterarAgrupamentos(ProgramacaoDesembolso entity, int recursoId, short action)
        {
            var salvos = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = entity.Id });
            var deleta = salvos?.Where(w => entity.Agrupamentos.All(a => a.Id != w.Id));

            _agrupamento.Excluir(deleta, recursoId, action);

            var agrupamentos = new List<ProgramacaoDesembolsoAgrupamento>();
            foreach (var agrupamento in entity.Agrupamentos)
            {
                agrupamento.PagamentoContaUnicaId = entity.Id;
                agrupamento.NumeroAgrupamento = entity.NumeroAgrupamento;
                //agrupamento.CodigoDespesa = entity.CodigoDespesa;
                agrupamento.Valor = Convert.ToDecimal( agrupamento.Valor.ToString().Replace(",",""));
                agrupamento.RegionalId =  agrupamento.Regional != null ?  Convert.ToInt32( agrupamento.Regional): agrupamento.RegionalId;
                agrupamento.Id = _agrupamento.SalvarOuAlterar(agrupamento, recursoId, action);
                agrupamentos.Add(agrupamento);
            }

            return agrupamentos;
        }

        private IEnumerable<ProgramacaoDesembolsoEvento> SalvarOuAlterarEventos(ProgramacaoDesembolso entity,
            int recursoId, short action)
        {
            var salvos = _eventos.Buscar(new ProgramacaoDesembolsoEvento { PagamentoContaUnicaId = entity.Id });
            var deleta = salvos?.Where(w => entity.Eventos.All(a => a.Id != w.Id));

            if (deleta.Any())
                _eventos.Excluir(deleta, recursoId, action);

            var eventos = new List<ProgramacaoDesembolsoEvento>();
            foreach (ProgramacaoDesembolsoEvento evento in entity.Eventos)
            {
                evento.PagamentoContaUnicaId = entity.Id;
                evento.Id = _eventos.SalvarOuAlterar(evento, recursoId, action);
                eventos.Add(evento);
            }

            return eventos;
        }

        public IEnumerable<ProgramacaoDesembolso> Listar(ProgramacaoDesembolso entity)
        {
            return _repository.Fetch(entity);
        }

        public IEnumerable<ProgramacaoDesembolso> BuscarGrid(ProgramacaoDesembolso entity,
            DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var entities = _repository.FetchForGrid(entity, de, ate).ToList();
            entities.ForEach(x => x.Eventos = _eventos.Buscar(new ProgramacaoDesembolsoEvento { PagamentoContaUnicaId = x.Id }));

            foreach (var pd in entities)
            {
               if (pd.ProgramacaoDesembolsoTipoId == 2)
                {
                    pd.Agrupamentos = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = pd.Id,NumeroSiafem = entity.NumeroSiafem ,NumeroProcesso = entity.NumeroProcesso,
                    DocumentoTipoId = entity.DocumentoTipoId,NumeroDocumento= entity.NumeroDocumento,NumeroAgrupamento= entity.NumeroAgrupamento,  Bloquear= entity.Bloquear});
                }
            }


            return entities.ToList();
        }

        public ProgramacaoDesembolso Selecionar(int Id)
        {
            var entity = _repository.Get(Id) ?? new ProgramacaoDesembolso();

            if (entity.Id == 0)
                return null;

            entity.Agrupamentos = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = entity.Id });
            entity.Eventos = _eventos.Buscar(new ProgramacaoDesembolsoEvento { PagamentoContaUnicaId = entity.Id });
            entity.DocumentoTipo = _docRepository.Listar(new DocumentoTipo { Id = entity.DocumentoTipoId }).FirstOrDefault();
            entity.ProgramacaoDesembolsoTipo = _tipoRepository.Listar(new ProgramacaoDesembolsoTipo { Id = entity.ProgramacaoDesembolsoTipoId }).FirstOrDefault();
            entity.Regional = _regional.Buscar(new Regional { Id = entity.RegionalId }).FirstOrDefault();
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

        public void TransmitirCancelamento(ProgramacaoDesembolso entity, Usuario user, int recursoId)
        {
            try
            {
                TransmitirSiafemCancelamento(entity, user, recursoId);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
        }

        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var empenhos = new List<IProgramacaoDesembolso>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                try
                {
                    var entity = (IProgramacaoDesembolso)Selecionar(empenhoId) ?? _agrupamento.Selecionar(empenhoId);

                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                    }

                    Retransmissao(user, entity, recursoId);
                    entity = (IProgramacaoDesembolso)Selecionar(empenhoId) ?? _agrupamento.Selecionar(empenhoId);
                    empenhos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                //finally
                //{
                //    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
                //}
            }

            var empenhosErros = empenhos.Where(x => x.StatusSiafem == "E").ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault()?.MensagemServicoSiafem;
                else
                    result += Environment.NewLine + "; / Algumas Programações de Desembolso não puderam ser retransmitidas";

            return result;
        }

        private void Transmissao(Usuario user, ProgramacaoDesembolso entity, int recursoId)
        {
            try
            {
                if (!entity.TransmitidoSiafem) TransmitirSiafemSiafisico(entity, user, recursoId);
            }
            catch
            {
                throw;
            }
        }

        private Error Retransmissao(Usuario user, IProgramacaoDesembolso entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                {
                    var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                    if (entity.TransmitirSiafem && !entity.TransmitidoSiafem)
                        if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                            TransmitirNomal((ProgramacaoDesembolso)entity, user, ug);
                        else
                            TransmitirSiafemRobo(user, ug, (ProgramacaoDesembolsoAgrupamento)entity, false);
                }
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

        private void TransmitirSiafemSiafisico(ProgramacaoDesembolso entity, Usuario user, int recursoId)
        {
            try
            {
                var result = default(string);
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                /*
                1	Manual
                2	Pagamentos a Preparar (Robô)
                3	Manual - PDBEC
                */

                if (entity.ProgramacaoDesembolsoTipoId == 1)
                    TransmitirNomal(entity, user, ug);
                else if (entity.ProgramacaoDesembolsoTipoId == 2)
                    TransmitirRobo(entity, user, ug);
                else if (entity.ProgramacaoDesembolsoTipoId == 3)
                    TransmitirPDBEC(entity, user, ug);
            }
            catch (Exception ex)
            {
                entity.StatusSiafem = "E";
                entity.MensagemServicoSiafem = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirPDBEC(ProgramacaoDesembolso entity, Usuario user, string ug)
        {
            var result = _siafem.InserirProgramacaoDesembolsoSiafisico(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);
            entity.NumeroSiafem = result;
            entity.TransmitidoSiafem = true;
            entity.StatusSiafem = "S";
            entity.DataTransmitidoSiafem = DateTime.Now;
            entity.MensagemServicoSiafem = null;
            entity.CausaCancelamento = null;
            entity.Cancelado = false;

            SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
        }

        public void TransmitirCancelamentoOp(IProgramacaoDesembolso entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            IProgramacaoDesembolso objModel;
            if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                objModel = Selecionar(entity.Id);
            else
                objModel = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = entity.Id }).FirstOrDefault();

            try
            {
                cicsmo = _chave.ObterChave(recursoId);
                objModel.TipoBloqueio = entity.TipoBloqueio;

                _prodesp.CancelamentoOp(cicsmo.Chave, cicsmo.Senha, objModel);

                if (entity.Bloqueio)
                {
                    TransmitirBoqueioOp(objModel, recursoId);
                    objModel.Bloqueio = true;
                }

                objModel.Cancelado = true;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

                if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                    SalvarOuAlterar((ProgramacaoDesembolso)objModel, 0, (short)EnumAcao.Transmitir);
                else
                    _agrupamento.SalvarOuAlterar((ProgramacaoDesembolsoAgrupamento)objModel, 0, (short)EnumAcao.Transmitir);
            }
        }

        public void TransmitirBoqueioOp(IProgramacaoDesembolso entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            IProgramacaoDesembolso objModel;
            IEnumerable<ProgramacaoDesembolsoAgrupamento>  pds;

            if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                objModel = Selecionar(entity.Id);
            else
                objModel = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = entity.Id }).FirstOrDefault();

            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                objModel.TipoBloqueio = entity.TipoBloqueio;

                _prodesp.BoqueioOp(cicsmo.Chave, cicsmo.Senha, objModel);

                objModel.Bloqueio = true;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

                if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                    SalvarOuAlterar((ProgramacaoDesembolso)objModel, 0, (short)EnumAcao.Transmitir);
                else
                {

                    pds = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumento = objModel.NumeroDocumento, NumeroAgrupamento = objModel.NumeroAgrupamento });

                    foreach (var programacao in pds)
                    {
                        programacao.TipoBloqueio = objModel.TipoBloqueio;
                        programacao.Bloqueio = true;

                        _agrupamento.SalvarOuAlterar(programacao, 0, (short)EnumAcao.Transmitir);

                    }

                }


            }
        }

        public void TransmitirDesbloqueioOp(IProgramacaoDesembolso entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            IProgramacaoDesembolso objModel;
            IEnumerable<ProgramacaoDesembolsoAgrupamento> pds;

            if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                objModel = Selecionar(entity.Id);
            else
                objModel = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = entity.Id }).FirstOrDefault();

            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                objModel.TipoBloqueio = entity.TipoBloqueio;

                _prodesp.DesbloqueioOp(cicsmo.Chave, cicsmo.Senha, objModel);

                objModel.Bloqueio = false;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

                if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                    SalvarOuAlterar((ProgramacaoDesembolso)objModel, 0, (short)EnumAcao.Transmitir);
                else
                {
                    pds = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumento = objModel.NumeroDocumento, NumeroAgrupamento = objModel.NumeroAgrupamento });

                    foreach (var programacao in pds)
                    {
                        programacao.TipoBloqueio = objModel.TipoBloqueio;
                        programacao.Bloqueio = false;

                        _agrupamento.SalvarOuAlterar(programacao, 0, (short)EnumAcao.Transmitir);
                    }
                }
            }
        }

        private void TransmitirSiafemCancelamento(ProgramacaoDesembolso entity, Usuario user, int recursoId)
        {
            try
            {
                var result = default(string);
                IEnumerable<ProgramacaoDesembolsoAgrupamento> pds;

                if (AppConfig.WsUrl != "siafemProd")
                {
                    user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                    user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                }
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                if (entity.ProgramacaoDesembolsoTipoId == 1 || entity.ProgramacaoDesembolsoTipoId == 3)
                {
                    var programacaoDesembolso = Selecionar(entity.Id);
                    programacaoDesembolso.CausaCancelamento = entity.CausaCancelamento;
                    result = _siafem.CancelarProgramacaoDesembolso(user.CPF, Decrypt(user.SenhaSiafem), ug, programacaoDesembolso);

                    programacaoDesembolso.NumeroSiafem = null;
                    programacaoDesembolso.MensagemServicoSiafem = null;
                    programacaoDesembolso.TransmitidoSiafem = false;
                    programacaoDesembolso.StatusSiafem = "N";
                    programacaoDesembolso.DataTransmitidoSiafem = default(DateTime);
                    programacaoDesembolso.Cancelado = true;
                    SalvarOuAlterar(programacaoDesembolso, recursoId, 1);
                }
                else
                {
                    var programacaoDesembolso = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { Id = entity.Id }).FirstOrDefault() ?? new ProgramacaoDesembolsoAgrupamento();
                    programacaoDesembolso.CausaCancelamento = entity.CausaCancelamento;
                    result = _siafem.CancelarProgramacaoDesembolso(user.CPF, Decrypt(user.SenhaSiafem), ug, programacaoDesembolso);

                    pds = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumento = programacaoDesembolso.NumeroDocumento, NumeroAgrupamento = programacaoDesembolso.NumeroAgrupamento });

                    foreach (var programacao in pds)
                    {

                        programacao.NumeroSiafem = null;
                        programacao.MensagemServicoSiafem = null;
                        programacao.TransmitidoSiafem = false;
                        programacao.StatusSiafem = "N";
                        programacao.DataTransmitidoSiafem = default(DateTime);
                        programacao.Cancelado = true;


                        _agrupamento.SalvarOuAlterar(programacao, recursoId, 1);

                    }



                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirRobo(ProgramacaoDesembolso entity, Usuario user, string ug)
        {
            try
            {
                var isHasError = false;


                if (entity.Agrupamentos.Any())
                {

                    foreach (var programacaoDesembolsoAgrupamento in entity.Agrupamentos.OrderBy(x => x.Sequencia))
                    {
                        if (string.IsNullOrEmpty(programacaoDesembolsoAgrupamento.NumeroSiafem))
                            isHasError = TransmitirSiafemRobo(user, ug, programacaoDesembolsoAgrupamento, isHasError);
                    }

                    if (isHasError)
                        throw new ApplicationException("Uma ou mais programações não puderam ser transmitidas");

                }
                else
                {
                    throw new ApplicationException("Não há documentos em condições de pagamento para o argumento informado.");
                }

            }
            finally
            {
                entity.Agrupamentos = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = entity.Id });
            }
        }

        private bool TransmitirSiafemRobo(Usuario user, string ug, ProgramacaoDesembolsoAgrupamento programacaoDesembolsoAgrupamento, bool isHasError)
        {
            try
            {
                var programacaoDesenbolso = _agrupamento.ProgramacaoDesembolsoFactory(programacaoDesembolsoAgrupamento);
                var result = _siafem.InserirProgramacaoDesembolso(user.CPF, Decrypt(user.SenhaSiafem), ug, programacaoDesenbolso);

                programacaoDesembolsoAgrupamento.NumeroSiafem = result;

                programacaoDesembolsoAgrupamento.MensagemServicoSiafem = null;
                programacaoDesembolsoAgrupamento.TransmitidoSiafem = true;
                programacaoDesembolsoAgrupamento.StatusSiafem = "S";
                programacaoDesembolsoAgrupamento.DataTransmitidoSiafem = DateTime.Now;
                programacaoDesembolsoAgrupamento.CausaCancelamento = null;
                programacaoDesembolsoAgrupamento.Cancelado = false;
            }
            catch (Exception ex)
            {
                programacaoDesembolsoAgrupamento.StatusSiafem = "E";
                programacaoDesembolsoAgrupamento.MensagemServicoSiafem = ex.Message;
                isHasError = true;
            }
            finally
            {
                _agrupamento.SalvarOuAlterar(programacaoDesembolsoAgrupamento, 0, 1);
            }
            return isHasError;
        }

        private void TransmitirNomal(ProgramacaoDesembolso entity, Usuario user, string ug)
        {
            var result = _siafem.InserirProgramacaoDesembolso(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);
            entity.NumeroSiafem = result;
            entity.TransmitidoSiafem = true;
            entity.StatusSiafem = "S";
            entity.DataTransmitidoSiafem = DateTime.Now;
            entity.MensagemServicoSiafem = null;
            entity.CausaCancelamento = null;
            entity.Cancelado = false;

            SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
        }

        internal class Error
        {
            public string Prodesp { get; set; }
            public string SiafemSiafisico { get; set; }
        }
    }
}