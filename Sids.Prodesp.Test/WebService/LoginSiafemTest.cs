using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Infrastructure;

namespace Sids.Prodesp.Test.WebService
{
    [TestClass()]
    public class WebServiceTest
    {
        [TestMethod()]
        public void TesteLoginComSucesso()
        {
            var teste = App.SiafemSegurancaService.Login(AppConfig.WsSiafemUser, AppConfig.WsPassword,"162101");
            Assert.AreEqual(teste, "true");
        }


        [TestMethod()]
        public void TesteLoginComSenhaInvalida()
        {
            var teste = App.SiafemSegurancaService.Login(AppConfig.WsSiafemUser, "13Outubro", "162101");
            Assert.AreEqual(teste, "SENHA INCORRETA DIGITE NOVAMENTE");
        }


        [TestMethod()]
        public void TesteLoginComUsuarioInvalido()
        {
            var teste = App.SiafemSegurancaService.Login(AppConfig.WsSiafemUser, AppConfig.WsPassword, "162101");
            Assert.AreEqual(teste, "- ACESSO NAO PERMITIDO");
        }

        [TestMethod()]
        public void TesteTocaSenhaComSucesso()
        {
            var teste = App.SiafemSegurancaService.AlterarSenha(AppConfig.WsSiafemUser, AppConfig.WsPassword, "12NOVEMBRO", "162101");
            Assert.AreEqual(teste, true);
        }

        [TestMethod()]
        public void TesteTocaSenhaComSenhaInvalida()
        {
            try
            {
                var teste = App.SiafemSegurancaService.AlterarSenha(AppConfig.WsSiafemUser, "13Outubro", "12NOVEMBRO", "162101");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "SENHA INCORRETA DIGITE NOVAMENTE");
            }
        }

        [TestMethod()]
        public void TesteTocaSenhaComUsuarioInvalido()
        {

            try
            {
                var teste = App.SiafemSegurancaService.AlterarSenha(AppConfig.WsSiafemUser, AppConfig.WsPassword, "12NOVEMBRO", "162101");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "- ACESSO NAO PERMITIDO");
            }
        }


    }
}

