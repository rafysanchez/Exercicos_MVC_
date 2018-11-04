using Sids.Prodesp.Core.Services.WebServie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem;


namespace Sids.Prodesp.Test.Services.WebServie
{
    [TestClass()]
    public class SiafemServiceTests
    {
        private Usuario _usuario;

        public SiafemServiceTests()
        {
            _usuario = CriarInstanciaUsuario();
        }

        [TestMethod()]
        public void LoginTest()
        {
            var teste = App.SiafemService.Login("PSIAFEM2017", "13NOVEMBRO", "162101");
            Assert.AreEqual(teste, "true");
        }

        [TestMethod()]
        public void AlterarSenhaTest()
        {
            var teste = App.SiafemService.AlterarSenha("PSIAFEM2017", "13NOVEMBRO", "12NOVEMBRO", "162101");
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
            var result = App.SiafemService.InserirReservaSiafem("PSIAFEM2017", "13NOVEMBRO", reserva, mes, "162101");

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
            var result = App.SiafemService.InserirReservaSiafisico("PSIAFISIC16", "13NOVEMBRO", reserva, mes, "162101");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ConsultaOCTest()
        {
            var result = App.SiafemService.ConsultaOC("PSIAFISIC16", "13NOVEMBRO", "2016OC00001", "162101", "16055");
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

            var result = App.SiafemService.ConsultaNr("PSIAFEM2017" /*"PSIAFISIC16"*/, "13NOVEMBRO", reserva, "162101");

            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void InserirReservaCancelamentoTest()
        {
            var reserva = CriarInstanciaReservaCancelamento();
            var mes = CriarInstanciaCancelamentoMes();

            var result = App.SiafemService.InserirReservaCancelamento(_usuario.CPF, _usuario.SenhaSiafem, reserva, mes, "162101");

            Assert.IsNotNull(result);
        }

        static Usuario CriarInstanciaUsuario()
        {
            return new Usuario {CPF = "PSIAFEM2017", SenhaSiafem = "13NOVEMBRO"};
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
