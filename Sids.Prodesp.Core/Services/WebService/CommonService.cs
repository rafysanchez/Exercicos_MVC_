
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sids.Prodesp.Core.Services.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Infrastructure.Services;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.ProgramacaoDesembolso;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.WebService
{
    using Base;
    using Empenho;
    using Infrastructure;
    using Infrastructure.DataBase.Configuracao;
    using Infrastructure.DataBase.Seguranca;
    using Infrastructure.Services.Empenho;
    using Infrastructure.Services.LiquidacaoDespesa;
    using LiquidacaoDespesa;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Entity.Seguranca;
    using Model.Interface.Empenho;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using Model.Interface.Service;
    using Model.Interface.Service.Empenho;
    using Model.Interface.Service.Reserva;
    using Model.ValueObject.Service.Prodesp.Common;
    using Model.ValueObject.Service.Prodesp.Reserva;
    using Model.ValueObject.Service.Siafem.Empenho;
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using Model.ValueObject.Service.Siafem.Reserva;
    using Reserva;
    using Services.Reserva;
    using Services.Seguranca;
    using System;
    using System.Configuration;
    using System.Linq;
    using PagamentoContaUnica;
    using Infrastructure.Services.PagamentoContaUnica;
    using Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
    using Model.Base.Empenho;
    using Model.Extension;
    using Model.Exceptions;
    using System.Text;
    using Model.Enum;
    using Model.Entity.PagamentoContaUnica.PagamentoDer;
    using PagamentoContaDer;
    using Infrastructure.Services.PagamentoContaDer;

    public class CommonService : BaseService
    {

        private readonly ChaveCicsmoService _chave;
        private readonly ProdespReservaService _prodespReserva;
        private readonly SiafemReservaService _siafemReserva;
        private readonly ICommon _common;
        private readonly SiafemEmpenhoService _siafemEmpenho;
        internal readonly RegionalService _regional;
        private readonly ProdespEmpenhoService _prodespEmpenho;
        private readonly SiafemLiquidacaoDespesaService _siafemSubempenho;
        private readonly SiafemPagamentoContaUnicaService _siafemContaUnica;
        private readonly ProdespLiquidacaoDespesaService _prodespLiquidacaoDespesaService;
        private readonly ProdespPagamentoContaUnicaService _prodespPagamentoContaUnicaService;
        private readonly ProdespPagamentoContaDerService _prodespPagamentoContaDerService;
        private readonly ProgramacaoDesembolsoService _programacaoDesembolso;
        private readonly ProgramacaoDesembolsoAgrupamentoService _programacaoDesembolsoAgrupamento;


        private int Recurso { get; set; }

        public CommonService(ILogError l, ICommon c, IProdespReserva prodespReserva, ISiafemReserva siafemReserva, ISiafemEmpenho siafemEmpenho, IChaveCicsmo chave) : base(l)
        {
            _prodespReserva = new ProdespReservaService(l, prodespReserva, new ProgramaDal(), new FonteDal(), new EstruturaDal(), new RegionalDal());
            _prodespEmpenho = new ProdespEmpenhoService(l, new ProdespEmpenhoWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal(), new RegionalDal());
            _prodespLiquidacaoDespesaService = new ProdespLiquidacaoDespesaService(l, new ProdespLiquidacaoDespesaWs(), new EstruturaDal());
            _prodespPagamentoContaUnicaService = new ProdespPagamentoContaUnicaService(l, new ProdespPagamentoContaUnicaWs());
            _prodespPagamentoContaDerService = new ProdespPagamentoContaDerService(l, new ProdespPagamentoContaDerWs());
            _siafemReserva = new SiafemReservaService(l, siafemReserva, new ProgramaDal(), new FonteDal(), new EstruturaDal());
            _siafemEmpenho = new SiafemEmpenhoService(l, siafemEmpenho, new ProgramaDal(), new FonteDal(), new EstruturaDal());
            _siafemSubempenho = new SiafemLiquidacaoDespesaService(l, new SiafemLiquidacaoDespesaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal());
            _regional = new RegionalService(l, new RegionalDal());
            _chave = new ChaveCicsmoService(l, chave);
            _common = c;
            _programacaoDesembolso = new ProgramacaoDesembolsoService(l, c, chave, new ProgramacaoDesembolsoDal(), new ProgramacaoDesembolsoAgrupamentoDal(), new ProgramacaoDesembolsoEventoDal(), new SiafemPagamentoContaUnicaWs(), new ProdespPagamentoContaUnicaWs());
            _programacaoDesembolsoAgrupamento = new ProgramacaoDesembolsoAgrupamentoService(l, new ProgramacaoDesembolsoAgrupamentoDal());
            _siafemContaUnica = new SiafemPagamentoContaUnicaService(l, new SiafemPagamentoContaUnicaWs());
        }

        public CommonService(ILogError l, ICommon c, IChaveCicsmo chave) : base(l)
        {
            _siafemSubempenho = new SiafemLiquidacaoDespesaService(l, new SiafemLiquidacaoDespesaWs(), new ProgramaDal(), new FonteDal(), new EstruturaDal());
            _regional = new RegionalService(l, new RegionalDal());
            _chave = new ChaveCicsmoService(l, chave);
            _siafemContaUnica = new SiafemPagamentoContaUnicaService(l, new SiafemPagamentoContaUnicaWs());
            _common = c;
        }

        public ConsultaAssinatura ConsultarAssinatura(string assinatura, int tipo)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespReserva.ConsultaAssinatura(assinatura, tipo, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        public string GetEnderecoByCep(string param)
        {
            try
            {
                return _common.GetAddressByZipCode(param);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public ConsultaContrato ConsultarContrato(string contrato, string type = null)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                var result = _prodespReserva.ConsultaContrato(contrato, chave.Chave, chave.Senha, type);

                if (type == "2")
                    result.ListConsultaContrato = result.ListConsultaContrato.Where(x => x.OutEvento == "EMPENHO").ToList();

                return result;
            }
            finally

            {
                _chave.LiberarChave(chave.Codigo);
            }

        }



        public ConsultaReservaEstrutura ConsultaReservaEstrutura(int anoExercicio, short regionalId, string cfp, string natureza, int programa, string origemRecurso, string processo)
        {

            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespReserva.ConsultaReservaEstrutura(anoExercicio, regionalId, cfp, natureza, programa, origemRecurso, processo, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        public ConsultaEmpenhoEstrutura ConsultaEmpenhoEstrutura(int anoExercicio, short regionalId, string cfp, string natureza, int programa, string origemRecurso, string processo)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespEmpenho.ConsultaEmpenhoEstrutura(anoExercicio, regionalId, cfp, natureza, programa, origemRecurso, processo, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        public ConsultaReserva ConsultarReserva(string reserva)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespReserva.ConsultaReserva(reserva, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }

        }

        public ConsultaEmpenho ConsultarEmpenho(string empenho)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespReserva.ConsultaEmpenho(empenho, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }

        }


        public ConsultaSubempenho ConsultarSubempenho(string subempenho)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.ConsultaSubempenho(subempenho, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        public object ConsultaEmpenhoSaldo(RapInscricao entity)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.ConsultaEmpenhoSaldo(entity, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        public ConsultaEspecificacaoDespesa ConsultarEspecificacaoDespesa(string especificacao)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespReserva.ConsultaEspecificacaoDespesa(especificacao, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }

        }

        public ConsultaOc ConsultaOC(Usuario usuario, string oc, string unidadegestora, string gestao)
        {
            try
            {
                if (AppConfig.WsUrl != "siafemProd")
                    usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

                return _siafemReserva.ConsultaOC(usuario.CPF, Decrypt(usuario.SenhaSiafem), oc, unidadegestora, gestao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConsultaNr ConsultaNr(IReserva reserva, Usuario usuario)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            ConsultaNr result = _siafemReserva.ConsultaNr(usuario.CPF, Decrypt(usuario.SenhaSiafem), reserva, ug);

            return result;
        }

        public ConsultaNe ConsultaNe(IEmpenho empenho, Usuario usuario)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            ConsultaNe result = _siafemEmpenho.ConsultaNe(usuario.CPF, Decrypt(usuario.SenhaSiafem), empenho, ug);

            return result;
        }
        public ConsultaNe ConsultaNe(string numeroNe, Usuario usuario)
        {
            try
            {
                if (AppConfig.WsUrl != "siafemProd")
                    usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

                ConsultaNe result = _siafemEmpenho.ConsultaNe(usuario.CPF, Decrypt(usuario.SenhaSiafem), numeroNe, ug);

                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(0034) EMPENHO INEXISTENTE"))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public ConsultaCt ConsultaCt(Usuario usuario, string numCt, string unidadegestora)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var uge = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            ConsultaCt result = _siafemEmpenho.ConsultaCt(usuario.CPF, Decrypt(usuario.SenhaSiafem), numCt, uge, "16055");

            result.DataEmissao = DateTime.Today.ToString("ddMMMyyyy").ToUpper();

            return result;
        }

        public ConsultaPrecoNE ConsultaPrecoNE(Usuario usuario, string numNE, string unidadegestora)
        {
            if (!numNE.Contains("NE"))
                throw new SidsException("Numero NE Inválido");

            numNE = numNE.Split(new string[] { "NE" }, StringSplitOptions.None)[1];

            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var uge = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            ConsultaPrecoNE result;

            try
            {
                result = _siafemEmpenho.ConsultaPrecoNE(usuario.CPF, Decrypt(usuario.SenhaSiafem), numNE, uge, "16055");
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Siafisico - ConPrecoNE NUMERO DE EMPENHO (ORIGINAL) NAO ENCONTRADO")) 
                {
                    result = null;
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        public ConsultaEmpenhos ConsultaEmpenhos(Usuario usuario, string cgcCpf, string data, string fonte, string gestao, string unidadeGestora, string gestaoCredor, string licitacao, string modalidadeEmpenho, string natureza, string numeroNe, string processo)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            ConsultaEmpenhos result = _siafemEmpenho.ConsultaEmpenhos(usuario.CPF, Decrypt(usuario.SenhaSiafem), cgcCpf, data, fonte, gestao, unidadeGestora, gestaoCredor, licitacao, modalidadeEmpenho, natureza, numeroNe, processo, ug);

            return result;
        }


        public ConsultaNL ConsultaNL(Usuario usuario, string unidadegestora, string gestao, string numeroSiafemSiafisco)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var uge = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            if (string.IsNullOrWhiteSpace(unidadegestora))
            {
                unidadegestora = uge;
            }
            var nl = _siafemSubempenho.ConsultaNL(usuario.CPF, Decrypt(usuario.SenhaSiafem), unidadegestora, numeroSiafemSiafisco);

            return nl;
        }


        public ConsultaNL ConsultaNL(Usuario usuario, string unidadegestora, string gestao, ILiquidacaoDespesa entity)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var resultado = _siafemSubempenho.ConsultaNL(usuario.CPF, Decrypt(usuario.SenhaSiafem), unidadegestora, entity);

            if (string.IsNullOrEmpty(resultado.UG))
            {
                resultado.UG = entity.CodigoUnidadeGestora;
            }

            return resultado;
        }

        public ConsultaPd ConsultaPD(Usuario usuario, string unidadegestora, string gestao, string numeroSiafemSiafisco)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            return _siafemContaUnica.ConsultaPD(usuario.CPF, /*Decrypt(usuario.SenhaSiafem)*/AppConfig.WsPassword, unidadegestora, numeroSiafemSiafisco);
        }


        public object ConsultarSubEmpenhoApoio(Subempenho subempenho)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.InserirSubEmpenhoApoio(subempenho, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        public object ConsultarEmpenhoCredor(Subempenho subempenho)
        {
            var chave = new ChaveCicsmo();

            try
            {
                chave = _chave.ObterChave();

                var result = _prodespLiquidacaoDespesaService.InserirEmpenhoCredor(subempenho, chave.Chave, chave.Senha);

                return result;

            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }

        }

        public object ConsultarAnulacaoApoio(SubempenhoCancelamento cancelamento)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.AnularSubEmpenhoApoio(cancelamento, chave.Chave, chave.Senha);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }



        public object ConsultarRapRequisicaoApoio(RapRequisicao entity)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.RapRequisicaoApoio(entity, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }


        public object ConsultarEmpenhoRap(RapRequisicao entity)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.ConsultarEmpenhoRap(entity, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }



        public object ConsultarRapAnulacaoApoio(string numRequisicaoRap)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespLiquidacaoDespesaService.RapAnulacaoApoio(numRequisicaoRap, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }

        public object ConsultarDesdobramentoApoio(Desdobramento entity)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespPagamentoContaUnicaService.DesdobramentoApoio(entity, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }

        public object ConsultarPreparacaoPagtoDocGeradorApoio(PreparacaoPagamento entity)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespPagamentoContaUnicaService.PreparacaoPagamentoApoio(entity, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }



        public object ConsultarPreparacaoPgtoTipoDespesaDataVenc(PreparacaoPagamento entity)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespPagamentoContaUnicaService.PreparacaoPgtoTipoDespesaDataVenc(entity, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }

        public object ConsultarArquivoTipoDataVenc2(ArquivoRemessa entity)
        {
            var key = new ChaveCicsmo();
            try
            {
                key = _chave.ObterChave();
                return _prodespPagamentoContaDerService.ConsultarArquivoTipoDespesaDataVenc2(entity, key.Chave, key.Senha);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }



        public IEnumerable<ConsultaDesdobramento> ConsultaDesdobramento(Desdobramento desdobramento)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave();
                return _prodespPagamentoContaUnicaService.ConsultaDesdobramento(cicsmo.Chave, cicsmo.Senha, desdobramento.NumeroDocumento, desdobramento.DocumentoTipoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        public IEnumerable<object> CancelamentoOpApoio(ProgramacaoDesembolso programacaoDesembolso)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave();
                var result = _prodespPagamentoContaUnicaService.CancelamentoOpApoio(cicsmo.Chave, cicsmo.Senha, programacaoDesembolso);
                return result;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        public IEnumerable<object> BloqueioOpApoio(ProgramacaoDesembolso programacaoDesembolso)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave();
                var result = _prodespPagamentoContaUnicaService.BloqueioOpApoio(cicsmo.Chave, cicsmo.Senha, programacaoDesembolso);
                return result;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }
        public IEnumerable<ProgramacaoDesembolsoAgrupamento> ConsultaDocumentoGerador(ProgramacaoDesembolso programacaoDesembolso)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave();
                var result = _prodespPagamentoContaUnicaService.ConsultaDocumentoGerador(cicsmo.Chave, cicsmo.Senha, programacaoDesembolso);



                if (programacaoDesembolso.ProgramacaoDesembolsoTipoId == 2)// robo
                {
                    result = result.Where(x => _programacaoDesembolsoAgrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumentoGerador = x.NumeroDocumentoGerador }).Count() == 0);
                }
                else
                {

                    result = result.Where(x => _programacaoDesembolso.Listar(new ProgramacaoDesembolso { NumeroDocumentoGerador = x.NumeroDocumentoGerador }).Count(y => y.NumeroSiafem != null) == 0);
                    result = result.Where(x => _programacaoDesembolsoAgrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumentoGerador = x.NumeroDocumentoGerador }).Count(y => y.NumeroSiafem != null) == 0);
                }


                return result;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }


        public string MsgDesprezados(ProgramacaoDesembolso programacaoDesembolso)
        {
            var cicsmo = new ChaveCicsmo();
            StringBuilder sb = new StringBuilder();
            string mensagem = "";
            var numAgrupamento = new List<int>();


            try
            {
                cicsmo = _chave.ObterChave();
                var result = _prodespPagamentoContaUnicaService.ConsultaDocumentoGerador(cicsmo.Chave, cicsmo.Senha, programacaoDesembolso);

                //base do sids
                var PD_AUX = result.Where(x => _programacaoDesembolsoAgrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumentoGerador = x.NumeroDocumentoGerador }).Any());


                if (PD_AUX.Any())
                {

                    int total = PD_AUX.Count();

                    foreach (ProgramacaoDesembolsoAgrupamento obj in PD_AUX)
                    {
                        var pd = _programacaoDesembolsoAgrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { NumeroDocumentoGerador = obj.NumeroDocumentoGerador }).OrderBy(o => o.NumeroAgrupamento);

                        foreach (ProgramacaoDesembolsoAgrupamento obj2 in pd)
                        {
                            if (!numAgrupamento.Contains(obj2.NumeroAgrupamento))
                                numAgrupamento.Add(obj2.NumeroAgrupamento);
                        }

                    }

                    numAgrupamento.Sort();

                    foreach (int obj3 in numAgrupamento)
                    {

                        if (sb.Length > 0)
                        {
                            sb.Append(",");
                            sb.Append(obj3.ToString());
                        }
                        else
                        {
                            sb.Append(obj3.ToString());
                        }
                    }
                    mensagem = "Foram desprezados " + total.ToString() + " Pagamentos a Preparar que já estão cadastrados no SIDS, no(s) agrupamento(s) N° " + sb.ToString();

                }

                return mensagem;

            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        public ConsultaPdfEmpenho ObterPdfEmpenho(IEmpenho empenho, Usuario usuario)
        {
            EnumTipoServicoFazenda tipo = EnumTipoServicoFazenda.Siafem;

            if (!string.IsNullOrWhiteSpace(empenho.NumeroEmpenhoSiafem))
            {
                tipo = EnumTipoServicoFazenda.Siafem;
            }
            else
            if (!string.IsNullOrWhiteSpace(empenho.NumeroEmpenhoSiafisico))
            {
                tipo = EnumTipoServicoFazenda.Siafisico;
            }

            var user = tipo == EnumTipoServicoFazenda.Siafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;

            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = user, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            var s = Decrypt(usuario.SenhaSiafem);

            ConsultaPdfEmpenho result = _siafemEmpenho.ObterPdfEmpenho(usuario.CPF, s, empenho, tipo, ug);

            return result;
        }
    }
}
