using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Infrastructure;

namespace Sids.Prodesp.Test.Services.Reserva
{
    [TestClass()]
    public class ReservaCancelamentoServiceTests
    {
        private readonly ReservaCancelamento _cancelamento;
        private readonly List<ReservaCancelamentoMes> _cancelamentoMes;
        private readonly Usuario _usuario;

        public ReservaCancelamentoServiceTests()
        {
            _usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword };
            _cancelamentoMes = GerarNewCancelamento(out _cancelamento);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            var cancelamentos = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Contrato = "1600123455" });

            Assert.IsNotNull(cancelamentos);
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            var cancelamentos = App.ReservaCancelamentoService.BuscarGrid(new ReservaCancelamento { Contrato = "1600123455" });

            Assert.IsNotNull(cancelamentos);
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            var cancelamentos = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Contrato = "1600123455" }).FirstOrDefault();
            var result = App.ReservaCancelamentoService.Excluir(cancelamentos, 0, 2);

            Assert.IsNotNull(result);
        }




        [TestMethod()]
        public void TransmitirTest()
        {
            var id = App.ReservaCancelamentoService.Salvar(_cancelamento, _cancelamentoMes, 0, 1);

            App.ReservaCancelamentoService.Transmitir(id, _usuario, 0);

            Assert.Fail("False");
        }

        [TestMethod()]
        public void ReTransmitirTest()
        {
            List<int> Codigo = new List<int> { 1, 2, 3 };
            App.ReservaCancelamentoService.Transmitir(Codigo, _usuario, 0);

            Assert.Fail("False");
        }

        [TestMethod()]
        public void SalvarTest()
        {
            var id = App.ReservaCancelamentoService.Salvar(_cancelamento, _cancelamentoMes, 0, 1);
            Assert.IsTrue(id > 0);
        }

        static Usuario CriarInstanciaUsuario()
        {
            return new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = App.UsuarioService.Encrypt(AppConfig.WsPassword) };
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
                Regional = 3,
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
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23",
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
        private static List<ReservaCancelamentoMes> GerarNewCancelamento(out Model.Entity.Reserva.ReservaCancelamento cancelamento)
        {
            var cancelamentoMes = CriarInstanciaCancelamentoMes();
            cancelamento = new Model.Entity.Reserva.ReservaCancelamento
            {

                Estrutura = 1,
                Fonte = 1,
                Programa = 1,
                Contrato = "1217777991",
                AnoExercicio = 2016,
                Regional = 3,
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
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23",
                TransmitirProdesp = true,
                TransmitirSiafem = true
            };
            return cancelamentoMes;
        }

    }
}