namespace Sids.Prodesp.Core.Services.Seguranca
{
    using WebService.Seguranca;
    using Base;
    using Infrastructure;
    using Model.Entity.Log;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using Model.Interface.Service.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Exceptions;

    public class UsuarioService : BaseService
    {
        private readonly ICrudUsuario _usuario;
        private readonly PerfilUsuarioService _perfilUsuarioService;
        private readonly ICrudPerfil _perfil;
        private readonly SiafemSegurancaService _siafemService;
        private readonly IRegional _regional;

        public UsuarioService(ILogError logError, ICrudUsuario user, ICrudPerfilUsuario perfilUser, ICrudPerfil per, ISiafemSeguranca siafemService, IRegional regional)
            : base(logError)
        {
            _perfil = per;
            _usuario = user;
            _perfilUsuarioService = new PerfilUsuarioService(logError, perfilUser);
            _regional = regional;
            _siafemService = new SiafemSegurancaService(logError, siafemService);
        }

        public AcaoEfetuada Salvar(Usuario obj, List<PerfilUsuario> perfis, int recursoId = 1, short actionId = 1)
        {
            try
            {
                var usuarios = Buscar(new Usuario { CPF = obj.CPF }).ToList();
                var logins = Buscar(new Usuario { ChaveDeAcesso = obj.ChaveDeAcesso }).ToList();

                if (obj.Codigo == 0)
                {

                    if (usuarios.Count(u => u.CPF == obj.CPF) > 0)
                        throw new SidsException("CPF já cadastrado");

                    if (logins.Count(u => u.ChaveDeAcesso == obj.ChaveDeAcesso) > 0)
                        throw new SidsException("Login já cadastrado");

                    PreInsertModel(obj);
                    obj.SenhaExpirada = true;
                    obj.AlterarSenha = true;
                    obj.DataExpiracaoSenha = DateTime.Today.AddDays(int.Parse("0" + AppConfig.DiasExpiracaoSenha));
                    obj.Codigo = _usuario.Add(obj);
                }
                else
                {
                    if (usuarios.Count(u => u.CPF == obj.CPF && u.Codigo != obj.Codigo) > 0)
                        throw new SidsException("CPF já cadastrado");

                    if (logins.Count(u => u.ChaveDeAcesso == obj.ChaveDeAcesso && u.Codigo != obj.Codigo) > 0)
                        throw new SidsException("Login já cadastrado");

                    var senha = _usuario.Fetch(new Usuario { Codigo = obj.Codigo }).FirstOrDefault().Senha;
                    if (GetUserIdLogado() == obj.Codigo && obj.Status == false)
                        throw new SidsException("O usuário logado não pode ser desativado.");

                    var idLogado = GetUserIdLogado();

                    if (senha != obj.Senha && idLogado != obj.Codigo)
                    {
                        obj.SenhaExpirada = true;
                        obj.AlterarSenha = true;
                    }

                    if (obj.DataExpiracaoSenha == new DateTime())
                    {
                        var user = _usuario.Fetch(new Usuario { Codigo = obj.Codigo }).FirstOrDefault();
                        obj.DataExpiracaoSenha = user.DataExpiracaoSenha;
                        obj.DataUltimoAcesso = user.DataUltimoAcesso;
                        obj.DataCriacao = user.DataCriacao;
                    }

                    _usuario.Edit(obj);
                }

                if (perfis != null)
                    SalvarPerfilUsuario(obj, perfis);

                var arg = string.Format("Login {0}, CPF {1}", obj.ChaveDeAcesso, obj.CPF);

                if (recursoId > 0)
                    return LogSucesso(actionId, recursoId, arg);

                return AcaoEfetuada.Sucesso;

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        public void SalvarPerfilUsuario(Usuario obj, List<PerfilUsuario> perfis)
        {
            try
            {
                var perifilSalvos = _perfilUsuarioService.ObterPerfilUsuarioPorUsuario(obj);

                foreach (var perfil in perifilSalvos)
                {
                    _perfilUsuarioService.Excluir(perfil);
                }

                foreach (PerfilUsuario item in perfis)
                {
                    item.Codigo = 0;
                    item.Usuario = obj.Codigo;
                    item.Status = true;
                    _perfilUsuarioService.Salvar(item);
                }
            }
            catch (Exception ex)
            {
                throw new SidsException(ex.Message);
            }
        }

        public AcaoEfetuada Excluir(int id, int recursoId, short actionId)
        {
            try
            {
                var obj = Buscar(new Usuario { Codigo = id }).FirstOrDefault();
                var usuariolog = _log.Fetch(new LogFilter { IdUsuario = id }).ToList();

                if (GetUserIdLogado() == id)
                    throw new SidsException("O usuário logado não pode ser excluído.");

                if (usuariolog.Count > 0)
                    throw new SidsException("O usuário não pode ser excluído, pois possui vinculo em ações no Sistema SIDS.");

                _usuario.Remove(obj.Codigo);

                var arg = String.Format("Login {0}, CPF {1}", obj.ChaveDeAcesso, obj.CPF);
                return LogSucesso(actionId, recursoId, arg);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }
        public List<Usuario> Buscar(Usuario obj)
        {
            try
            {
                var usuarios = _usuario.Fetch(obj).ToList();
                return usuarios;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public AcaoEfetuada AlterarSenha(Usuario objModel, string senhaAtual, string novasenha, int recursoId, short actionId)
        {
            var mensagem = "";
            var ugo = _regional.Fetch(new Regional { Id = (int)objModel.RegionalId }).FirstOrDefault().Uge;

            if (recursoId == 0 && !objModel.SenhaSiafemExpirada)
            {
                mensagem = objModel.AcessaSiafem ? _siafemService.Login(objModel.CPF, Decrypt(novasenha), ugo) : "true";

                ValidarMensagemSisfem(mensagem);
            }
            else if (objModel.AcessaSiafem && (senhaAtual != novasenha || objModel.SenhaSiafemExpirada))
                AtualizarSenhaSiafem(ref objModel, senhaAtual, novasenha, ugo);

            //else if (objModel.AcessaSiafem && senhaAtual != novasenha || (senhaAtual != novasenha && objModel.SenhaSiafemExpirada && recursoId == 0))


            AtualizarSenhaSids(ref objModel, novasenha);

            return Salvar(objModel, null, recursoId, actionId);
        }

        private static void AtualizarSenhaSids(ref Usuario objModel, string novasenha)
        {
            objModel.Senha = novasenha;
            objModel.SenhaSiafem = novasenha;
            objModel.DataExpiracaoSenha = DateTime.Today.AddDays(int.Parse("0" + AppConfig.DiasExpiracaoSenha));
            objModel.SenhaExpirada = false;
            objModel.AlterarSenha = false;
            objModel.Token = string.Empty;
        }

        private void AtualizarSenhaSiafem(ref Usuario objModel, string senhaAtual, string novasenha, string ugo)
        {
            _siafemService.AlterarSenha(objModel.CPF, Decrypt(senhaAtual), Decrypt(novasenha), "");

            objModel.SenhaSiafem = novasenha;
            objModel.SenhaSiafemExpirada = false;
        }

        public AcaoEfetuada AlterarSenha(Usuario objModel, string novasenha, string senhaatual, int recursoId)
        {
            try
            {
                Usuario usu = null;

                if (objModel.SenhaExpirada)
                {
                    usu = _usuario.Fetch(new Usuario { ChaveDeAcesso = objModel.ChaveDeAcesso, Senha = senhaatual }).FirstOrDefault();
                }
                else if (objModel.AcessaSiafem && recursoId == 0)
                {
                    var ugo = _regional.Fetch(new Regional { Id = (int)objModel.RegionalId }).FirstOrDefault().Uge;
                    usu = objModel;

                    var mensagem = _siafemService.Login(objModel.CPF, Decrypt(senhaatual), ugo);

                    ValidarMensagemSisfem(mensagem);
                }
                else if (objModel.AcessaSiafem && recursoId > 0)
                {
                    usu = objModel;
                }
                else
                    usu = _usuario.Fetch(new Usuario { ChaveDeAcesso = objModel.ChaveDeAcesso, Senha = senhaatual }).FirstOrDefault();

                if (usu != null)
                {
                    return AlterarSenha(objModel, senhaatual, novasenha, recursoId, (short)EnumAcao.Alterar);
                }
                else
                {
                    var msg = recursoId > 0 ? "Senha atual deve ser a mesma cadastrada." : "A senha atual não confere.";
                    throw new SidsException(msg);
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Alterar, recursoId);
            }
        }

        private static void ValidarMensagemSisfem(string mensagem)
        {
            if (mensagem.Contains("SENHA INCORRETA DIGITE NOVAMENTE") || mensagem.Contains("- ACESSO NAO PERMITIDO"))
                throw new SidsException("A senha atual informada não confere com a senha atual do SIAFEM");
            else if (mensagem != "true" && !mensagem.Contains("- POR FAVOR, TROQUE SUA SENHA"))
                throw new SidsException("SIAFEM " + mensagem);
        }

        public void AtualizarUsuarioAD(Usuario objModel)
        {
            try
            {
                var encryptSenha = Encrypt(objModel.Senha);
                var usu = _usuario.Fetch(new Usuario { ChaveDeAcesso = objModel.ChaveDeAcesso }).FirstOrDefault();

                if (usu != null)
                {
                    usu.Senha = encryptSenha;
                    _usuario.UpdateADUser(usu);
                }
                else
                {
                    throw new SidsException("Usuário ou senha inválido.");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public string ValidaLoginAcesso(ref Usuario objModel)
        {
            try
            {
                var mensagem = "";
                var encryptSenha = Encrypt(objModel.Senha);
                var u = Buscar(new Usuario { ChaveDeAcesso = objModel.ChaveDeAcesso/*, Status = true*/ }).FirstOrDefault();

                if (u == null)
                    throw new SidsException("Usuário ou senha atual não confere");
                else if (u.Senha != encryptSenha && objModel.TipoAutenticacao == TipoAutenticacao.SSO)
                    throw new SidsException("Usuário ou senha atual não confere");
                else if (u.Status == false)
                    throw new SidsException("Usuário desativado. Favor solicitar ao responsável! ");
                else if (u != null && u.AcessaSiafem == true && !string.IsNullOrEmpty(u.SenhaSiafem))
                {
                    var ugo = _regional.Fetch(new Regional { Id = (int)u.RegionalId }).FirstOrDefault().Uge;

                    mensagem = _siafemService.Login(u.CPF, Decrypt(u.SenhaSiafem), ugo);

                    if (mensagem.Contains("SENHA INCORRETA DIGITE NOVAMENTE") || mensagem.Contains("- ACESSO NAO PERMITIDO"))
                    {
                        mensagem = "A senha atual não confere com a senha atual do SIAFEM, por favor, atualize-a.";
                        u.AlterarSenhaSiafem = true;
                        Salvar(u, null, 0, 0);
                    }
                    else if (mensagem.Contains("- POR FAVOR, TROQUE SUA SENHA"))
                    {
                        mensagem = "true";
                        u.SenhaSiafemExpirada = true;
                        Salvar(u, null, 0, 0);
                    }
                    else if (mensagem == "true")
                    {
                        u.SenhaSiafemExpirada = false;
                        Salvar(u, null, 0, 0);
                    }

                }
                else
                {
                    mensagem = "true";
                }

                if (u.DataExpiracaoSenha <= DateTime.Now)
                {
                    u.SenhaExpirada = true;
                    u.AlterarSenha = true;
                    Salvar(u, null, 0, 0);
                }

                AtualizaUltimaDataAcesso(u);
                objModel = u;
                SetCurrentUser(u);
                return mensagem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AtualizaUltimaDataAcesso(Usuario objModel)
        {
            try
            {
                _usuario.UpdateLastAccess(objModel);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public string GerarNovaSenha(Usuario objModel)
        {
            var senha = RandomPassword(8);
            objModel.Senha = Encrypt(senha);
            objModel.SenhaExpirada = true;
            Salvar(objModel, null, 0, 0);
            return senha;
        }

        private List<Perfil> ObterPerfilPorUserId(int userId)
        {
            var perfis = _perfil.FetchByUser(new Usuario { Codigo = userId }).ToList();
            return perfis;
        }
    }
}
