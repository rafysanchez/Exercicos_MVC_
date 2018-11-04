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

    public class RapInscricaoService : CommonService 
    {
        private readonly ICrudRapInscricao _repository;
        private readonly ProdespLiquidacaoDespesaService _prodesp;
        private readonly SiafemLiquidacaoDespesaService _siafem;
        private readonly RapInscricaoNotaService _notas;
        private readonly ChaveCicsmoService _chave;
        


        public RapInscricaoService(ILogError log, ICommon common, IChaveCicsmo chave,
            ICrudRapInscricao repository, ICrudRapInscricaoNota notas, 
            ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estrutura,
            IProdespLiquidacaoDespesa prodesp, ISiafemLiquidacaoDespesa siafem)
            : base(log, common, chave)
        {
            _prodesp = new ProdespLiquidacaoDespesaService(log, prodesp, estrutura);
            _siafem = new SiafemLiquidacaoDespesaService(log, siafem, programa, fonte, estrutura);
            _chave = new ChaveCicsmoService(log, chave);
            _notas = new RapInscricaoNotaService(log, notas);
            _repository = repository;
        }


        public AcaoEfetuada Excluir(RapInscricao entity, int recursoId, short action)
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

        public int SalvarOuAlterar(RapInscricao entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (entity.Notas != null)
                    entity.Notas = SalvarOuAlterarNotas(entity, recursoId, action);

                if (recursoId > 0) LogSucesso(action, recursoId, $@"
                    Nº  Prodesp {entity.NumeroProdesp}, 
                    Nº SIAFEM/SIAFISICO {entity.NumeroSiafemSiafisico}.");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        private IEnumerable<LiquidacaoDespesaNota> SalvarOuAlterarNotas(RapInscricao entity, int recursoId, short action)
        {

            var salvos = _notas.Buscar(new LiquidacaoDespesaNota { SubempenhoId = entity.Id });
            var deleta = salvos?.Where(w => entity.Notas.All(a => a.Id != w.Id));
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


        public IEnumerable<RapInscricao> Listar(RapInscricao entity)
        {
            return _repository.Fetch(entity);
        }

        public IEnumerable<RapInscricao> BuscarGrid(RapInscricao entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            return _repository.FetchForGrid(entity, de, ate);
        }



        public RapInscricao Selecionar(int Id)
        {
            var entity = _repository.Get(Id);

            entity.Notas = _notas.Buscar(new SubempenhoNota { SubempenhoId = entity.Id });

            return entity;
        }


        public RapInscricao Assinaturas(RapInscricao entity)
        {
            return _repository.GetLastSignatures(entity);
        }



        public void Transmitir(int entityId, Usuario user, int recursoId, int cedId)
        {

            var entity = Selecionar(entityId);

            entity.CEDId = cedId;

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
            var empenhos = new List<RapInscricao>();
            var result = "";

            foreach (var empenhoId in entityIdList)
            {
                var entity = new RapInscricao();
                try
                {
                    entity = Selecionar(empenhoId);
                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                    }

                    var error = Retransmissao(user, entity, recursoId);

                    if (entity.TransmitirProdesp && (entity.TransmitirSiafem || entity.TransmitirSiafisico))
                        if (entity.StatusProdesp == "E" && (entity.StatusSiafemSiafisico == "S"))
                        {
                            result += $";Erro {error.Prodesp}";
                            result += $" para transmissão Inscrição de Rap N° SIAFEM/SIAFISICO {entity.NumeroSiafemSiafisico}";
                            
                        }
                        else if ((entity.StatusSiafemSiafisico == "E") && entity.StatusProdesp == "S")
                            result += $";Erro {error.SiafemSiafisico} para transmissão Inscrição de Rap N° Prodesp {entity.NumeroProdesp}";

                    empenhos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
                    if (entity.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
                }
            }
            var empenhosErros =
                empenhos.Where(x => (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "E")) ||
                        (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "N")) ||
                        (x.StatusProdesp == "N" && (x.StatusSiafemSiafisico == "E"))).ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault().MensagemSiafemSiafisico + Environment.NewLine + Environment.NewLine + " | " + empenhos.FirstOrDefault().MensagemProdesp;
                else
                    result += Environment.NewLine + "; / Algumas Inscrições de Rap não puderam ser transmitidos";

            return result;
        }

        private void Transmissao(Usuario user, RapInscricao entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId);

                if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId);

                if (entity.TransmitidoSiafem && entity.TransmitidoProdesp)
                {
                    cicsmo = _chave.ObterChave(recursoId);
                    entity.StatusDocumento = _prodesp.InserirDoc(entity, cicsmo.Chave, cicsmo.Senha, "05");
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

        private Error Retransmissao(Usuario user, RapInscricao entity, int recursoId)
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
                    entity.StatusDocumento = _prodesp.InserirDoc(entity, cicsmo.Chave, cicsmo.Senha, "05");
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

        private void TransmitirProdesp(RapInscricao entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                var result = _prodesp.InserirRapInscricao(entity, cicsmo.Chave, cicsmo.Senha);
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

        private void TransmitirSiafem(RapInscricao entity, Usuario user, int recursoId)
        {
            try
            {
                var result = default(string);
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                entity.Notas = _notas.Buscar(new RapInscricaoNota() {SubempenhoId = entity.Id});

                result = _siafem.InserirRapInscricaoSiafem(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);

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