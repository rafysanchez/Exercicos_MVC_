using Sids.Prodesp.Model.Entity.LiquidacaoDespesa;
using Sids.Prodesp.Model.Interface.LiquidacaoDespesa;

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class FiltroGridViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nº Ap. Subempenho Prodesp")]
        public string NumeroSubempenhoProdesp { get; set; }

        [Display(Name = "Nº Ap. Subempenho SIAFEM/SIAFISICO")]
        public string NumeroSubempenhoSiafemSiafisico { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }
        public IEnumerable<SelectListItem> StatusProdespListItems { get; set; }

        [Display(Name = "Status SIAFEM/SIAFISICO")]
        public string StatusSiafemSiafisico { get; set; }
        public IEnumerable<SelectListItem> StatusSiafemSiafisicoListItems { get; set; }

        [Display(Name = "Tipo Apropriação/Subempenho")]
        public string CenarioSiafemSiafisico { get; set; }
        public IEnumerable<SelectListItem> CenarioTipoListItems { get; set; }


        [Display(Name = "Tipo de Serviço")]
        public string ServicoTipoId { get; set; }
        public IEnumerable<SelectListItem> ServicoTipoListItems { get; set; }

        [Display(Name = "Data")]
        public string DataCadastro { get; set; }


        public bool TransmitidoProdesp { get; set; }
        public bool TransmitidoSiafem { get; set; }
        public bool TransmitidoSiafisico { get; set; }
        public bool TransmitirProdesp { get; set; }
        public bool TransmitirSiafem { get; set; }
        public bool TransmitirSiafisico { get; set; }
        public bool CadastroCompleto { get; set; }
        public string MensagemProdesp { get; set; }
        public string MensagemSiafemSiafisico { get; set; }


        public FiltroGridViewModel CreateInstance(ILiquidacaoDespesa objModel, IEnumerable<CenarioTipo> cenarioList)
        {
            return new FiltroGridViewModel()
            {
                Id = Convert.ToString(objModel.Id),
                NumeroSubempenhoProdesp = objModel.NumeroProdesp,
                NumeroSubempenhoSiafemSiafisico = objModel.NumeroSiafemSiafisico,
                ValorTotal = objModel.Valor != 0 ? Convert.ToDecimal(objModel.Valor) / 100 : Convert.ToDecimal(objModel.ValorRealizado) / 100,
                StatusProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso",
                StatusSiafemSiafisico = string.IsNullOrEmpty(objModel.StatusSiafemSiafisico) || objModel.StatusSiafemSiafisico.Equals("N") ? "Não Transmitido" : objModel.StatusSiafemSiafisico.Equals("E") ? "Erro" : "Sucesso",
                CenarioSiafemSiafisico = cenarioList.FirstOrDefault(x => x.Id == objModel.CenarioSiafemSiafisico)?.Descricao,
                TransmitirSiafem = objModel.TransmitirSiafem,
                TransmitirSiafisico = objModel.TransmitirSiafisico,
                TransmitirProdesp = objModel.TransmitirProdesp,
                TransmitidoSiafem = objModel.TransmitidoSiafem,
                TransmitidoSiafisico = objModel.TransmitidoSiafisico,
                TransmitidoProdesp = objModel.TransmitidoProdesp,
                CadastroCompleto = objModel.CadastroCompleto,
                MensagemProdesp = objModel.MensagemProdesp,
                MensagemSiafemSiafisico = objModel.MensagemSiafemSiafisico
            };
        }

        public FiltroGridViewModel CreateInstance(IRap entity, IEnumerable<ServicoTipo> servico)
        {

            return new FiltroGridViewModel()
            {
                Id = Convert.ToString(entity.Id),
                NumeroSubempenhoProdesp = entity.NumeroProdesp,
                NumeroSubempenhoSiafemSiafisico = entity.NumeroSiafemSiafisico,
                ValorTotal = Convert.ToDecimal(entity.Valor) / 100,
                StatusProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso",
                StatusSiafemSiafisico = string.IsNullOrEmpty(entity.StatusSiafemSiafisico) || entity.StatusSiafemSiafisico.Equals("N") ? "Não Transmitido" : entity.StatusSiafemSiafisico.Equals("E") ? "Erro" : "Sucesso",

                MensagemProdesp = entity.MensagemProdesp,
                MensagemSiafemSiafisico = entity.MensagemSiafemSiafisico,

                ServicoTipoId = servico.FirstOrDefault(w => w.Id == entity.TipoServicoId)?.Descricao,

                TransmitirSiafem = entity.TransmitirSiafem,
                TransmitirSiafisico = entity.TransmitirSiafisico,
                TransmitirProdesp = entity.TransmitirProdesp,
                TransmitidoSiafem = entity.TransmitidoSiafem,
                TransmitidoSiafisico = entity.TransmitidoSiafisico,
                TransmitidoProdesp = entity.TransmitidoProdesp,
                CadastroCompleto = entity.CadastroCompleto,
                DataCadastro = entity.DataCadastro.ToShortDateString()
        };
        }

    }
}