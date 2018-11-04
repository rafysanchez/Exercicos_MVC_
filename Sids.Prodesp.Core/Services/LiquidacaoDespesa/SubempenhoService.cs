using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Base;
    using Infrastructure;
    using Model.Base.LiquidacaoDespesa;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Extension;
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

    public class SubempenhoService : SubempenhoBaseService
    {
        private readonly ICrudSubempenho _repository;
        private readonly ProdespLiquidacaoDespesaService _prodesp;
        private readonly SiafemLiquidacaoDespesaService _siafem;
        private readonly SubempenhoNotaService _notas;
        private readonly SubempenhoItemService _itens;
        private readonly SubempenhoEventoService _eventos;
        private readonly ChaveCicsmoService _chave;



        public SubempenhoService(ILogError log, ICommon common, IChaveCicsmo chave,
            ICrudSubempenho repository, ICrudSubempenhoNota notas, ICrudSubempenhoItem itens, ICrudSubempenhoEvento eventos,
            ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estrutura,
            IProdespLiquidacaoDespesa prodesp, ISiafemLiquidacaoDespesa siafem)
            : base(log, common, chave)
        {
            _prodesp = new ProdespLiquidacaoDespesaService(log, prodesp, estrutura);
            _siafem = new SiafemLiquidacaoDespesaService(log, siafem, programa, fonte, estrutura);
            _notas = new SubempenhoNotaService(log, notas);
            _itens = new SubempenhoItemService(log, itens);
            _eventos = new SubempenhoEventoService(log, eventos);
            _chave = new ChaveCicsmoService(log, chave);

            _repository = repository;
        }


        public AcaoEfetuada Excluir(Subempenho entity, int recursoId, short action)
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

        public int SalvarOuAlterar(Subempenho entity, int recursoId, short action)
        {
            return SalvarOuAlterar(entity, recursoId, action, false);
        }
        public int SalvarOuAlterar(Subempenho entity, int recursoId, short action, bool salvamentoAposTransmitir)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (entity.Eventos != null)
                    entity.Eventos = SalvarOuAlterarEventos(entity, recursoId, action);

                if (entity.Itens != null)
                    entity.Itens = SalvarOuAlterarItens(entity, recursoId, action, salvamentoAposTransmitir);

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

        private IEnumerable<LiquidacaoDespesaNota> SalvarOuAlterarNotas(Subempenho entity, int recursoId, short action)
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

        private IEnumerable<SubempenhoItem> SalvarOuAlterarItens(Subempenho entity, int recursoId, short action, bool salvamentoAposTransmitir)
        {
            var salvos = _itens.Buscar(new SubempenhoItem { SubempenhoId = entity.Id });
            var deleta = salvos?.Where(w => entity.Itens.All(a => a.Id != w.Id));

            _itens.Excluir(deleta, recursoId, action);

            var itens = new List<SubempenhoItem>();
            var seq = 1;
            foreach (var item in entity.Itens)
            {
                var itemSalvo = salvos.FirstOrDefault(x => x.Id == item.Id);

                item.SequenciaItem = seq++;
                item.SubempenhoId = entity.Id;
                //item.QuantidadeMaterialServico = item.QuantidadeLiquidar == 0 ? item.QuantidadeMaterialServico : item.QuantidadeLiquidar;
                item.QuantidadeMaterialServico = salvamentoAposTransmitir ? item.QuantidadeMaterialServico : item.QuantidadeLiquidar;
                item.CodigoItemServico = item.CodigoItemServico.FormatarCodigoItem();
                item.Valor = item.Valor == 0 && itemSalvo != null && itemSalvo.Id != 0 ? itemSalvo.Valor : item.Valor;

                item.Id = _itens.SalvarOuAlterar(item, recursoId, action);
                itens.Add(item.ToSubempenhoItem());
            }

            return itens;
        }

        private IEnumerable<LiquidacaoDespesaEvento> SalvarOuAlterarEventos(Subempenho entity, int recursoId, short action)
        {
            var salvos = _eventos.Buscar(new LiquidacaoDespesaEvento { SubempenhoId = entity.Id });
            var deleta = salvos?.Where(w => entity.Eventos.All(a => a.Id != w.Id));

            if (deleta.Any())
                _eventos.Excluir(deleta, recursoId, action);

            var eventos = new List<LiquidacaoDespesaEvento>();
            foreach (LiquidacaoDespesaEvento evento in entity.Eventos)
            {
                evento.SubempenhoId = entity.Id;
                evento.Id = _eventos.SalvarOuAlterar(evento, recursoId, action);
                eventos.Add(evento);
            }

            return eventos;
        }

        public IEnumerable<Subempenho> Listar(Subempenho entity)
        {
            return _repository.Fetch(entity);
        }

        public IEnumerable<Subempenho> BuscarGrid(Subempenho entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var result = _repository.FetchForGrid(entity, de, ate);
            return result;
        }



        public Subempenho Selecionar(int Id)
        {
            var entity = _repository.Get(Id);

            //por conta da DataHelper não possuir recursividade ou ser capaz de trabalhar com objetos complexos
            //a população completa do objeto é feita aqui (e exclusivamente aqui)
            var itens = _itens.Buscar(new SubempenhoItem { SubempenhoId = entity.Id });
            entity.Itens = itens;
            entity.Notas = _notas.Buscar(new SubempenhoNota { SubempenhoId = entity.Id });
            entity.Eventos = _eventos.Buscar(new SubempenhoEvento { SubempenhoId = entity.Id });

            return entity;
        }


        public Subempenho Assinaturas(Subempenho entity)
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
                SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir, true);
                if (entity.TransmitidoProdesp)
                {
                    SetCurrentTerminal(string.Empty);
                }
            }
        }

        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var empenhos = new List<Subempenho>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                var entity = new Subempenho();
                try
                {
                    entity = Selecionar(empenhoId);
                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                    }


                    var dtoErro = Retransmissao(user, entity, recursoId);

                    if (entity.TransmitirProdesp && (entity.TransmitirSiafem || entity.TransmitirSiafisico))
                        if (entity.StatusProdesp == "E" && (entity.StatusSiafemSiafisico == "S"))
                        {
                            result += $";Erro {dtoErro.Prodesp}";
                            result += $" para transmissão Subempenho N° SIAFEM/SIAFISICO {entity.NumeroSiafemSiafisico}";

                        }
                        else if ((entity.StatusSiafemSiafisico == "E") && entity.StatusProdesp == "S")
                            result += $";Erro {dtoErro.SiafemSiafisico} para transmissão Subempenho N° Prodesp {entity.NumeroProdesp}";

                    empenhos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir, true);
                    if (entity.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
                }
            }

            var empenhosErros =
                empenhos.Where(x => (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "E")) ||
                        (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "N")) ||
                        (x.StatusProdesp == "N" && (x.StatusSiafemSiafisico == "E"))).ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault().MensagemSiafemSiafisico + Environment.NewLine + " | " + empenhos.FirstOrDefault().MensagemSiafemSiafisico + Environment.NewLine + " | " + empenhos.FirstOrDefault().MensagemProdesp;
                else
                    result += Environment.NewLine + "; / Alguns Subempenhos não puderam ser retransmitidos";


            return result;
        }

        private void Transmissao(Usuario user, Subempenho entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            var itensOriginais = entity.Itens;

            try
            {
                entity.Itens = this.VerificarItensParaTransmitir(entity);
                this.EliminarItensZerados(entity);

                if (entity.TransmitirSiafem && !entity.TransmitidoSiafem)
                {
                    TransmitirSiafem(entity, user, recursoId);
                }

                if (entity.TransmitirSiafisico && !entity.TransmitidoSiafisico)
                {
                    TransmitirSiafisico(entity, user, recursoId);
                }

                if (entity.TransmitirProdesp && !entity.TransmitidoProdesp)
                {
                    TransmitirProdesp(entity, recursoId);
                }

                if ((entity.TransmitidoSiafem || entity.TransmitidoSiafisico) && entity.TransmitidoProdesp)
                {
                    cicsmo = _chave.ObterChave(recursoId);
                    entity.StatusDocumento = _prodesp.InserirDoc(entity, cicsmo.Chave, cicsmo.Senha, "05");

                }
            }
            catch (Exception ex)
            {
                entity.Itens = itensOriginais;
                throw ex;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        private Error Retransmissao(Usuario user, Subempenho entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                { if (entity.TransmitirSiafem && !entity.TransmitidoSiafem) TransmitirSiafem(entity, user, recursoId); }
                catch (Exception ex)
                { error.SiafemSiafisico = ex.Message; }

                try
                { if (entity.TransmitirSiafisico && !entity.TransmitidoSiafisico) TransmitirSiafisico(entity, user, recursoId); }
                catch (Exception ex)
                { error.SiafemSiafisico = ex.Message; }

                try
                { if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId); }
                catch (Exception ex)
                { error.Prodesp = ex.Message; }

                if ((entity.TransmitidoSiafisico || entity.TransmitidoSiafem) && entity.TransmitidoProdesp)
                {
                    cicsmo = _chave.ObterChave(recursoId);
                    entity.StatusDocumento = _prodesp.InserirDoc(entity, cicsmo.Chave, cicsmo.Senha, "05");
                    _chave.LiberarChave(cicsmo.Codigo);
                }
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

        private void TransmitirProdesp(Subempenho entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                var result = _prodesp.InserirSubEmpenho(entity, cicsmo.Chave, cicsmo.Senha);
                _chave.LiberarChave(cicsmo.Codigo);

                string[] delimiter = result.Split(';');

                entity.NumeroProdesp = delimiter[0].Replace(" ", "");
                entity.Referencia = delimiter[1];
                entity.TransmitidoProdesp = true;
                entity.StatusProdesp = "S";
                entity.DataTransmitidoProdesp = DateTime.Now;
                entity.MensagemProdesp = null;

                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir, true);
            }
            catch (Exception ex)
            {
                _chave.LiberarChave(cicsmo.Codigo);
                entity.StatusProdesp = "E";
                entity.MensagemProdesp = ex.Message;
                entity.Itens = null;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirSiafem(Subempenho entity, Usuario user, int recursoId)
        {
            try
            {
                var result = default(string);
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                result = _siafem.InserirSubempenhoSiafem(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);

                entity.NumeroSiafemSiafisico = result;
                entity.TransmitidoSiafem = true;
                entity.StatusSiafemSiafisico = "S";
                entity.DataTransmitidoSiafemSiafisico = DateTime.Now;
                entity.MensagemSiafemSiafisico = null;

                base.SomarEventos(entity);

                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir, true);
            }
            catch (Exception ex)
            {
                entity.StatusSiafemSiafisico = "E";
                entity.MensagemSiafemSiafisico = ex.Message;
                entity.Itens = null;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirSiafisico(Subempenho entity, Usuario user, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)user.RegionalId }).First().Uge;

                var cenarioComItens = new int[6] { 1, 2, 3, 9, 10, 11 };
                var validarItens = cenarioComItens.Contains(entity.CenarioSiafemSiafisico);

                if (validarItens && (entity.Itens == null || !entity.Itens.Any()))
                {
                    throw new SidsException("Sem itens para transmitir");
                }

                string numeroNl = _siafem.InserirSubempenhoSiafisico(user.CPF, Decrypt(user.SenhaSiafem), ug, entity);

                entity.NumeroSiafemSiafisico = numeroNl;
                entity.TransmitidoSiafisico = true;
                entity.StatusSiafemSiafisico = "S";
                entity.DataTransmitidoSiafemSiafisico = DateTime.Now;
                entity.MensagemSiafemSiafisico = null;

                base.CalcularValoresNl(entity, user, ug);

                if (entity.Itens.Count() > 0)
                {
                    entity.ValorRealizado = entity.Valor;
                }

                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir, true);
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