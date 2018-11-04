namespace Sids.Prodesp.Test.Services.WebServie.Reserva
{
    using Application;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sids.Prodesp.Infrastructure;
    [TestClass()]
    public class SiafemReservaServiceTests
    {
        private Usuario _usuario;

        public SiafemReservaServiceTests()
        {
            _usuario = CriarInstanciaUsuario();
        }

        [TestMethod()]
        public void LoginTest()
        {
            var teste = App.SiafemSegurancaService.Login(AppConfig.WsSiafemUser, AppConfig.WsPassword, "162101");
            Assert.AreEqual(teste, "true");
        }

        [TestMethod()]
        public void AlterarSenhaTest()
        {
            var teste = App.SiafemSegurancaService.AlterarSenha(AppConfig.WsSiafemUser, AppConfig.WsPassword, "12NOVEMBRO", "162101");
            Assert.AreEqual(teste, true);
        }

        [TestMethod()]
        public void InserirReservaSiafemTest()
        {

            var reserva = new Model.Entity.Reserva.Reserva
            {
                DataEmissao = DateTime.Now,
                Uo = "16055",
                Processo = "processo1",
                Ugo = "162101",
                Observacao = "obras e demais servicos"
            };

            var mes = App.ReservaMesService.Buscar(new ReservaMes {Id = reserva.Codigo}).Cast<IMes>().ToList();
            var result = App.SiafemReservaService.InserirReservaSiafem(AppConfig.WsSiafemUser, AppConfig.WsPassword, reserva, mes, "162101");

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirReservaSiafisicoTest()
        {

            var reserva = new Model.Entity.Reserva.Reserva
            {
                DataEmissao = DateTime.Now,
                Uo = "16055",
                Processo = "processo1",
                Ugo = "162101",
                Oc = "00259",
                Observacao =
                    "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23"
            };

            var mes = App.ReservaMesService.Buscar(new ReservaMes {Id = reserva.Codigo}).Cast<IMes>().ToList();
            var result = App.SiafemReservaService.InserirReservaSiafisico(AppConfig.WsSiafisicoUser, AppConfig.WsPassword, reserva, mes, "16055");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ConsultaOCTest()
        {
            var result = App.SiafemReservaService.ConsultaOC(AppConfig.WsSiafisicoUser, AppConfig.WsPassword, "2016OC00001", "162101", "16055");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ConsultaNrTest()
        {
            var reserva = new Model.Entity.Reserva.Reserva
            {
                DataEmissao = DateTime.Now,
                Uo = "16055",
                Processo = "processo1",
                Ugo = "162101",
                Oc = "00259",
                Observacao =
                    "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23",
                NumSiafemSiafisico = "2016NR00012"
            };

            var result = App.SiafemReservaService.ConsultaNr(AppConfig.WsSiafemUser /*AppConfig.WsSiafisicoUser*/, AppConfig.WsPassword, reserva, "16055");

            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void InserirReservaCancelamentoTest()
        {
            var reserva = CriarInstanciaReservaCancelamento();
            var mes = CriarInstanciaCancelamentoMes();

            var result = App.SiafemReservaService.InserirReservaCancelamento(_usuario.CPF, _usuario.SenhaSiafem, reserva, mes, "16055");

            Assert.IsNotNull(result);
        }

        static Usuario CriarInstanciaUsuario()
        {
            return new Usuario {CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword};
        }

        static ReservaCancelamento CriarInstanciaReservaCancelamento()
        {
            return new ReservaCancelamento
            {
                Estrutura = 1,
                Fonte = 1,
                Programa = 1,
                Contrato = "1217777991",
                AnoExercicio = 2016,
                Regional = 16,
                AutorizadoAssinatura = "11111",
                AutorizadoGrupo = "1",
                AutorizadoOrgao = "99",
                AutorizadoSupraFolha = "fl01",
                DestinoRecurso = "04",
                ExaminadoAssinatura = "12345",
                ExaminadoGrupo = "1",
                ExaminadoOrgao = "99",
                Processo = "processo1",
                OrigemRecurso = "004001004",
                ResponsavelAssinatura = "88888",
                ResponsavelGrupo = "1",
                ResponsavelOrgao = "00",
                EspecificacaoDespesa = "001",
                DescEspecificacaoDespesa = "TESTE",
                DataEmissao = DateTime.Parse("15/02/2016"),
                Uo = "16055",
                Ugo = "122101",
                Oc = "00259",
                Observacao =
                    "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7",
                TransmitirProdesp = true,
                TransmitirSiafem = true
            };
        }

        static List<ReservaCancelamentoMes> CriarInstanciaCancelamentoMes()
        {
            return new List<ReservaCancelamentoMes>
            {
                new ReservaCancelamentoMes {ValorMes = (decimal) 1, Descricao = "01"},
                new ReservaCancelamentoMes {ValorMes = (decimal) 2, Descricao = "04"},
                new ReservaCancelamentoMes {ValorMes = (decimal) 3, Descricao = "07"},
                new ReservaCancelamentoMes {ValorMes = (decimal) 4, Descricao = "10"}
            };
        }
    }
}
