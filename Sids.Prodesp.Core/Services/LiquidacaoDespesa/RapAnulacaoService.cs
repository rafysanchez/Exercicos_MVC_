using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Base;
    using Infrastructure;
    using Model.Base.LiquidacaoDespesa;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface.Configuracao;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using Model.Interface.Service;
    using Model.Interface.Service.LiquidacaoDespesa;
    using Reserva;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using WebService;
    using WebService.LiquidacaoDespesa;

    public class RapAnulacaoService : CommonService
    {
        private readonly ICrudRapAnulacao _repository;
        private readonly ProdespLiquidacaoDespesaService _prodesp;
        private readonly SiafemLiquidacaoDespesaService _siafem;
        private readonly RapAnulacaoNotaService _notas;
        private readonly ChaveCicsmoService _chave;



        public RapAnulacaoService(ILogError log, ICommon common, IChaveCicsmo chave,
            ICrudRapAnulacao repository, ICrudRapAnulacaoNota notas,
            ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estrutura,
            IProdespLiquidacaoDespesa prodesp, ISiafemLiquidacaoDespesa siafem)
            : base(log, common, chave)
        {
            _prodesp = new ProdespLiquidacaoDespesaService(log, prodesp, estrutura);
            _siafem = new SiafemLiquidacaoDespesaService(log, siafem, programa, fonte, estrutura);
            _notas = new RapAnulacaoNotaService(log, notas);
            _chave = new ChaveCicsmoService(log, chave);

            _repository = repository;
        }


        public AcaoEfetuada Excluir(RapAnulacao entity, int recursoId, short action)
        {
            try
            {
                //a procedure se encarrega de excluir os itens, eventos e notas fiscais deste subemepnho
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"Subempenho : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        public int SalvarOuAlterar(RapAnulacao entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (entity.Notas != null)
                    entity.Notas = SalvarOuAlterarNotas(entity, recursoId, action);

                if (recursoId > 0) LogSucesso(action, recursoId, $@"
                    Nº do subempenho Prodesp {entity.NumeroProdesp}, 
                    Nº do subempenho SIAFEM/SIAFISICO {entity.NumeroSiafemSiafisico}.");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        private IEnumerable<LiquidacaoDespesaNota> SalvarOuAlterarNotas(RapAnulacao entity, int recursoId, short action)
        {

            var salvos = _notas.Buscar(new LiquidacaoDespesaNota { SubempenhoId = entity.Id });
            var deleta = salvos?.Where(w => entity.Notas.All(a => a.Id != w.Id)) ?? new List<LiquidacaoDespesaNota>();

            if (deleta.Any())
                _notas.Excluir(deleta, recursoId, action);

            var notas = new List<LiquidacaoDespesaNota>();
            foreach (LiquidacaoDespesaNota nota in entity.Notas)
            {
                nota.SubempenhoId = entity.Id;
                nota.Id = _notas.SalvarOuAlterar(nota, recursoId, action);
                notas.Add(nota);
            }

            return notas;
        }

        public IEnumerable<RapAnulacao> Listar(RapAnulacao entity)
        {
            return _repository.Fetch(entity);
        }

        public IEnumerable<RapAnulacao> BuscarGrid(RapAnulacao entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            return _repository.FetchForGrid(entity, de, ate);
        }



        public RapAnulacao Selecionar(int Id)
        {
            var entity = _repository.Get(Id);
            entity.Notas = _notas.Buscar(new RapAnulacaoNota { SubempenhoId = entity.Id });

            return entity;
        }


        public RapAnulacao Assinaturas(RapAnulacao entity)
        {
            return _repository.GetLastSignatures(entity);
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
                if (entity.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
            }
        }

        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var empenhos = new List<RapAnulacao>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                Transmitir(empenhoId, user, recursoId);
            }
            var empenhosErros =
                empenhos.Where(x => (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "E")) ||
                        (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "N" )) ||
                        (x.StatusProdesp == "N" && (x.StatusSiafemSiafisico == "E" ))).ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault().MensagemSiafemSiafisico + Environment.NewLine + Environment.NewLine + " | " + empenhos.FirstOrDefault().MensagemProdesp;
                else
                    result += Environment.NewLine + "; / Algumas Anulações de Rap não puderam ser transmitidos";

            return result;
        }

        private void Transmissao(Usuario user, RapAnulacao entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId);

                if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId);

                if (entity.TransmitidoSiafem && entity.TransmitidoProdesp)
                {
                    cicsmo = _chave.ObterChave(recursoId);
                    //entity.StatusDocumento = _prodesp.InserirDoc(entity, cicsmo.Chave, cicsmo.Senha, "03");
                    _chave.LiberarChave(cicsmo.Codigo);
                }
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

        private Error Retransmissao(Usuario user, RapAnulacao entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                { if (!entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId); }
                catch (Exception ex)
                { error.Prodesp = ex.Message; }

                try
                { if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId); }
                catch (Exception ex)
                { error.SiafemSiafisico = ex.Message; }

                if (entity.TransmitidoSiafem && entity.TransmitidoProdesp)
                {
                    cicsmo = _chave.ObterChave(recursoId);
                    //entity.StatusDocumento = _prodesp.InserirDoc(entity, cicsmo.Chave, cicsmo.Senha, "03");
                    _chave.LiberarChave(cicsmo.Codigo);
                }

                return error;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        private void TransmitirProdesp(RapAnulacao entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                var result = _prodesp.InserirRapAnulacao(entity, cicsmo.Chave, cicsmo.Senha);
                _chave.LiberarChave(cicsmo.Codigo);

                entity.NumeroProdesp = result.Replace(" ", "");
                entity.TransmitidoProdesp = true;
                entity.StatusProdesp = "S";
                entity.DataTransmitidoProdesp = DateTime.Now;
                entity.MensagemProdesp = null;

                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                _chave.LiberarChave(cicsmo.Codigo);
                entity.StatusProdesp = "E";
                entity.MensagemProdesp = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirSiafem(RapAnulacao entity, Usuario user, int recursoId)
        {
            try
            {
                var result = default(string);
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                result = _siafem.InserirRapRequisicaoApoioSiafem(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);

                entity.NumeroSiafemSiafisico = result;
                entity.TransmitidoSiafem = true;
                entity.StatusSiafemSiafisico = "S";
                entity.DataTransmitidoSiafemSiafisico = DateTime.Now;
                entity.MensagemSiafemSiafisico = null;

                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                entity.StatusSiafemSiafisico = "E";
                entity.MensagemSiafemSiafisico = ex.Message;
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