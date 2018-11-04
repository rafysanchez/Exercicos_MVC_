
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento
{

    public class FiltroGridViewModel
    {

        #region gridDesdobramento properties

        public string Id { get; set; }

        [Display(Name = "Tipo Desdobramento")]
        public string DesdobramentoTipo { get; set; }

        [Display(Name = "Tipo Documento")]
        public string DocumentoTipo { get; set; }

        [Display(Name = "Total ISSQN")]
        public decimal ValorIssqn { get; set; }

        [Display(Name = "Total IR")]
        public decimal ValorIr { get; set; }

        [Display(Name = "Total INSS")]
        public decimal ValorInss { get; set; }

        [Display(Name = "Valor Documento")]
        public decimal ValorDocumento { get; set; }

        [Display(Name = "Status Transmissão Prodesp")]
        public string StatusProdesp { get; set; }

        [Display(Name = "Cancelado")]
        public string Cancelado { get; set; }

        public string MensagemProdesp { get; set; }

        public bool CadastroCompleto { get; set; }

        public bool TransmitidoProdesp { get; set; }
        #endregion



        #region gridReclassificacão properties

        [Display(Name = "N° Documento")]
        public string NumeroDocumento { get; set; }
        [Display(Name = "Nº Reclas. / Ret. SIAFEM")]
        public string NumeroSiafem { get; set; }

        [Display(Name = "Tipo de Reclassificação / Retenção")]
        public string ReclassificacaoRetencaoTipo { get; set; }

        [Display(Name = "Normal ou Estorno?")]
        public string NormalEstorno { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Status Trans. Siafem")]
        public string StatusSiafem { get; set; }
        public bool TransmitidoSiafem { get; set; }
        public string MensagemSiafem { get; set; }  //toolTip

        #endregion

        //comuns properties
        [Display(Name = "Data")]
        public string Data { get; set; }


        public FiltroGridViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento entity, IEnumerable<DesdobramentoTipo> desdobramentoTipos, IEnumerable<DocumentoTipo> documentoTipos)
        {
            return new FiltroGridViewModel
            {
                Id = Convert.ToString(entity.Id),
                DesdobramentoTipo = desdobramentoTipos.FirstOrDefault(w => w.Id == entity.DesdobramentoTipoId)?.Descricao,
                DocumentoTipo = documentoTipos.FirstOrDefault(w => w.Id == entity.DocumentoTipoId)?.Descricao,
                Data = entity.DataCadastro.ToShortDateString(),
                ValorInss = Convert.ToDecimal(entity.ValorInss),
                ValorIssqn = Convert.ToDecimal(entity.ValorIssqn) ,
                ValorIr = Convert.ToDecimal(entity.ValorIr) ,
                ValorDocumento = Convert.ToDecimal(entity.ValorDistribuido),
                StatusProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso",
                MensagemProdesp = entity.MensagemServicoProdesp,
                TransmitidoProdesp = entity.TransmitidoProdesp,
                CadastroCompleto = entity.CadastroCompleto,
                Cancelado = entity.SituacaoDesdobramento == "S" ? "Sim" : "Não",
                NumeroDocumento = entity.NumeroDocumento
            };
        }


        public FiltroGridViewModel CreateInstance(ReclassificacaoRetencao entity, IEnumerable<ReclassificacaoRetencaoTipo> reclassificacaoRetencaoTps, IEnumerable<ReclassificacaoRetencaoEvento> recRetEventos)
        {
            return new FiltroGridViewModel
            {
                Id = Convert.ToString(entity.Id),
                NumeroSiafem = entity.NumeroSiafem,
                ReclassificacaoRetencaoTipo = reclassificacaoRetencaoTps.FirstOrDefault(w => w.Id == entity.ReclassificacaoRetencaoTipoId)?.Descricao,
                NormalEstorno = entity.NormalEstorno.Equals("1") ? "Normal" : "Estorno",
                Total = entity.ReclassificacaoRetencaoTipoId == 2? Convert.ToDecimal(entity .Eventos.Sum(x => x.ValorUnitario)) / 100 : Convert.ToDecimal(entity.Valor) / 100,
                Data = entity.DataCadastro.ToShortDateString(),
                StatusSiafem = string.IsNullOrEmpty(entity.StatusSiafem) || entity.StatusSiafem.Equals("N") ? "Não Transmitido" : entity.StatusSiafem.Equals("E") ? "Erro" : "Sucesso",
                TransmitidoSiafem = entity.TransmitidoSiafem,
                MensagemSiafem = entity.MensagemServicoSiafem,
                CadastroCompleto = entity.CadastroCompleto
            };
        }

    }
}

