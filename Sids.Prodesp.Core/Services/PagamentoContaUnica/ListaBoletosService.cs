using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ListaBoletosService : CommonService
    {

        private readonly ICrudListaBoletos _repository;
        private readonly ChaveCicsmoService _chave;
        private readonly ListaCodigoBarrasService _codigoBarras;
        private readonly SiafemPagamentoContaUnicaService _siafem;
        private readonly ProdespPagamentoContaUnicaService _prodesp;


        public ListaBoletosService(ILogError log, ICommon common, IChaveCicsmo chave, ISiafemPagamentoContaUnica siafem, IProdespPagamentoContaUnica contaUnica,  ICrudListaBoletos repository) : base(log, common, chave)
        {
            _chave = new ChaveCicsmoService(log, chave);
            _codigoBarras = new ListaCodigoBarrasService(new ListaCodigoBarrasDal());
            _siafem = new SiafemPagamentoContaUnicaService(log, siafem);
            _prodesp = new ProdespPagamentoContaUnicaService(log, contaUnica);
            _repository = repository;
        }


        public AcaoEfetuada Excluir(ListaBoletos entity, int recursoId, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"Lista de boletos : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        public int SalvarOuAlterar(ListaBoletos entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (entity.ListaCodigoBarras != null)
                    SalvarOuAlterarIdentificacao(entity);

                if (recursoId > 0 && !string.IsNullOrEmpty(entity.NumeroSiafem)) LogSucesso(action, recursoId, $"Lista de boletos : Codigo {entity.NumeroSiafem}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        private void SalvarOuAlterarIdentificacao(ListaBoletos entity)
        {
            var salvos = _codigoBarras.Listar(new ListaCodigoBarras { ListaBoletosId = entity.Id });
            var deleta = salvos?.Where(w => entity.ListaCodigoBarras.All(a => a.Id != w.Id || (a.CodigoBarraBoleto?.NumeroConta1 == null && a.CodigoBarraTaxa?.NumeroConta1 == null))) ?? new List<ListaCodigoBarras>();

            foreach (var codigoBarras in deleta)
            {
                _codigoBarras.Excluir(codigoBarras);
            }

            entity.ListaCodigoBarras.ToList().ForEach(x => x.ListaBoletosId = entity.Id);
            _codigoBarras.SalvarOuAlterar(entity.ListaCodigoBarras.Where(a => a.CodigoBarraBoleto?.NumeroConta1 != null || a.CodigoBarraTaxa?.NumeroConta1 != null));
        }


        public IEnumerable<ListaBoletos> Listar(ListaBoletos entity)
        {
            return _repository.Fetch(entity);
        }


        public ListaBoletos Selecionar(int id)
        {
            var entity = _repository.Get(id);
            entity.ListaCodigoBarras = _codigoBarras.Listar(new ListaCodigoBarras { ListaBoletosId = id }).ToList();
            return entity;
        }


        public IEnumerable<ListaBoletos> BuscarGrid(ListaBoletos entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var result = _repository.FetchForGrid(entity, de, ate);
            return result;
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
            var empenhos = new List<ListaBoletos>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                var entity = new ListaBoletos();
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
                    result += Environment.NewLine + "; / Algumas Listas Boleto não puderam ser retransmitidos";


            return result;
        }

        private void Transmissao(Usuario user, ListaBoletos entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId);

                if (entity.TransmitidoSiafem)
                {
                    cicsmo = _chave.ObterChave(recursoId);
                    _prodesp.InserirDoc(entity: entity, chave: cicsmo.Chave, senha: cicsmo.Senha, tipo: entity.DocumentoTipoId == 5 ? "91" : "92");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        private Error Retransmissao(Usuario user, ListaBoletos entity, int recursoId)
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
                throw;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
            return error;
        }


        private void TransmitirSiafem(ListaBoletos entity, Usuario user, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional {Id = (int) user.RegionalId}).First().Uge;

                foreach (var entityListaCodigoBarra in entity.ListaCodigoBarras.Where(x => !x.TransmitidoSiafem && (x.CodigoBarraBoleto?.Id > 0 || x.CodigoBarraTaxa?.Id > 0)))
                {
                    var result = _siafem.InserirListaBoletos(user.CPF, Decrypt(user.SenhaSiafem), ug, entity, entityListaCodigoBarra);

                    if (string.IsNullOrWhiteSpace(entity.NumeroSiafem))
                    {
                        entity.NumeroSiafem = result.Split(';')[0];
                        entity.ValorTotalLista += Convert.ToDecimal(result.Split(';')[1]);
                    }
                    else
                        entity.ValorTotalLista += Convert.ToDecimal(result);

                    entity.TotalCredores += 1;
                    entityListaCodigoBarra.TransmitidoSiafem = true;
                }


                entity.TransmitidoSiafem = true;
                entity.StatusSiafem = "S";
                entity.DataTransmitidoSiafem = DateTime.Now;
                entity.MensagemServicoSiafem = null;
            }
            catch (Exception ex)
            {
                entity.StatusSiafem = "E";
                entity.MensagemServicoSiafem = ex.Message;
                throw;
            }
            finally
            {
                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
        }

        internal class Error
        {
            public string Prodesp { get; set; }
            public string SiafemSiafisico { get; set; }
        }
    }
}
