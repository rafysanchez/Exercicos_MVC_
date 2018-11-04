using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Infrastructure.DataBase;
using Sids.Prodesp.Infrastructure.DataBase.Configuracao;
using Sids.Prodesp.Infrastructure.DataBase.Reserva;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Interface.Interface.Log;
using Sids.Prodesp.Interface.Interface.Reserva;
using Sids.Prodesp.Interface.Interface.Seguranca;
using Sids.Prodesp.Interface.Interface.Service;
using Sids.Prodesp.Model.Interface;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.ValueObject;
using Sids.Prodesp.Model.ValueObject.Service.Reserva.Prodesp;
using Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem;

namespace Sids.Prodesp.Core.Services.WebServie
{
    public class CommonService : Base.BaseService
    {

        private readonly ICrudReserva _reserva;
        private readonly ChaveCicsmoService _chave;
        private readonly ProdespService _prodesp;
        private readonly SiafemService _siafem;
        private readonly ICommon _common;
        private ILogError l;
        internal readonly RegionalService _regional;

        private int Recurso { get; set; }

        public CommonService(ILogError l, ICommon c, ICrudReserva p, IProdesp prodesp, ISiafem siafem, IChaveCicsmo chave) : base(l)
        {
            _prodesp = new ProdespService(l, prodesp, new ProgramaDal(), new FonteDal(), new EstruturaDal(), new RegionalDal());
            _siafem = new SiafemService(l, siafem, new ProgramaDal(), new FonteDal(), new EstruturaDal());
            _regional = new RegionalService(l, new RegionalDal());
            _reserva = p;
            _chave = new ChaveCicsmoService(l, chave);
            _common = c;
        }


        public ConsultaAssinatura ConsultarAssinatura(string assinatura, int tipo)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                var result = _prodesp.ConsultaAssinatura(assinatura, tipo, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
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
                return _common.GetEnderecoByCep(param);
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
            }
        }

        public ConsultaContrato ConsultarContrato(string contrato)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                var result = _prodesp.ConsultaContrato(contrato, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }

        }


        public ConsultaReservaEstrutura ConsultaReservaEstrutura(
                    int anoExercicio,
                    short regionalId,
                    string cfp,
                    string natureza,
                    int programa,
                    string origemRecurso,
                    string processo)

        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                var result = _prodesp.ConsultaReservaEstrutura(
                    anoExercicio,
                    regionalId,
                    cfp,
                    natureza,
                    programa,
                    origemRecurso,
                    processo,
                    chave.Chave,
                    chave.Senha);
                _chave.LiberarChave(chave.Codigo);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }


        public ConsultaReserva ConsultarReserva(string reserva)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                var result = _prodesp.ConsultaReserva(reserva, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }

        }

        public ConsultaEmpenho ConsultarEmpenho(string reserva)
        {

            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave();
                var result = _prodesp.ConsultaEmpenho(reserva, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
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
                var result = _prodesp.ConsultaEspecificacaoDespesa(especificacao, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
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
                if (ConfigurationManager.AppSettings["WSURL"] != "siafemProd")
                    usuario = new Usuario { CPF = "PSIAFISIC17", SenhaSiafem = Encrypt("13NOVEMBRO") , RegionalId = 1};
                
                return _siafem.ConsultaOC(usuario.CPF, Decrypt(usuario.SenhaSiafem), oc, unidadegestora, gestao);
            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
            }
        }


        public ConsultaNr ConsultaNr(IReserva reserva, Usuario usuario)
        {
            try
            {
                if (ConfigurationManager.AppSettings["WSURL"] != "siafemProd")
                    usuario = new Usuario {CPF = "PSIAFEM2017", SenhaSiafem = Encrypt("13NOVEMBRO"), RegionalId = 1};

                var ug = _regional.Buscar(new Regional {Id = Convert.ToInt16(usuario.RegionalId)}).First().Uge;

                ConsultaNr result = _siafem.ConsultaNr(usuario.CPF, Decrypt(usuario.SenhaSiafem), reserva, ug);

                return result;

            }
            catch (Exception ex)
            {
                throw ErrorLog(ex);
            }
        }
    }
}
