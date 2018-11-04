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
    public class ReservaServiceTests
    {
        private readonly Model.Entity.Reserva.Reserva _reserva;
        private readonly List<ReservaMes> _reservaMes;
        private readonly Usuario _usuario;
        public ReservaServiceTests()
        {
            _usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword };
            _reservaMes = GerarNewReserva(out _reserva);
        }

        [TestMethod()]
        public void SalvarTest()
        {

            int id = App.ReservaService.Salvar(_reserva, _reservaMes,0,1);

            Assert.IsTrue(id > 0);
        }


        [TestMethod()]
        public void BuscarTest()
        {
            var reservas = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva {Contrato = "1217777991" });

            Assert.IsNotNull(reservas);
        }


        [TestMethod()]
        public void BuscarGridTest()
        {
            var reservas = App.ReservaService.BuscarGrid(new Model.Entity.Reserva.Reserva { Contrato = "1217777991" });

            Assert.IsNotNull(reservas);
        }
        
        [TestMethod()]
        public void TransmitirTest()
        {
            var reservas = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva {Contrato = "1217777991"}).FirstOrDefault();
            App.ReservaService.Transmitir(reservas.Codigo, _usuario, 0);

            Assert.Fail("False");
        }

        [TestMethod()]
        public void TransmitirTest1()
        {
            List<int> id = new List<int>();

            var reservas = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva {Contrato = "1217777991"}).FirstOrDefault();

            id.Add(reservas.Codigo);

            var result = App.ReservaService.Transmitir(id, _usuario, 0);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ReTransmitirTest()
        {
            var reservas = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva { Contrato = "1600123455" }).FirstOrDefault();
            App.ReservaService.Transmitir(reservas.Codigo, _usuario, 0);

            Assert.Fail("False");
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            var reservas = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva { Contrato = "1217777991" }).FirstOrDefault();
            var result = App.ReservaService.Excluir(reservas, 0, 2);

            Assert.IsNotNull(result);
        }



        [TestMethod()]
        public void ImprimirTest()
        {
            var reserva = App.ReservaService.Buscar(new Model.Entity.Reserva.Reserva { NumSiafemSiafisico = "2016NR00012" }).FirstOrDefault();
            
            var result = App.ReservaService.ConsultaNr(reserva, _usuario);

            Assert.IsNotNull(result);
        }

        private static List<ReservaMes> GerarNewReserva(out Model.Entity.Reserva.Reserva reserva)
        {
            var reservaMes = new List<ReservaMes>
            {
                new ReservaMes {ValorMes = 1, Descricao = "01"},
                new ReservaMes {ValorMes = 2, Descricao = "04"},
                new ReservaMes {ValorMes = 3, Descricao = "07"},
                new ReservaMes {ValorMes = 4, Descricao = "10"}
            };

            reserva = new Model.Entity.Reserva.Reserva
            {
                Tipo = 1,
                Estrutura = 1,
                Fonte = 1,
                Programa = 1,
                Contrato = "1217777991",
                AnoExercicio = 2016,
                AnoReferencia = 2016,
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
            return reservaMes;
        }

    }
}