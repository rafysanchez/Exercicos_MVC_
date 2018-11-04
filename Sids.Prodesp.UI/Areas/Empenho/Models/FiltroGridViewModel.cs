namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Extension;
    using Model.Interface.Base;
    using Model.Interface.Empenho;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FiltroGridViewModel
    {
        public string Id { get; set; }


        [Display(Name = "Nº Empenho Prodesp")]
        public string NumeroEmpenhoProdesp { get; set; }

        [Display(Name = "Nº Empenho SIAFEM")]
        public string NumeroEmpenhoSiafem { get; set; }

        [Display(Name = "Nº Empenho SIAFISICO")]
        public string NumeroEmpenhoSiafisico { get; set; }

        [Display(Name = "CFP")]
        public string ProgramaId { get; set; }

        [Display(Name = "CED")]
        public string NaturezaId { get; set; }

        [Display(Name = "Origem do Recurso")]
        public string FonteId { get; set; }

        [Display(Name = "Destino do Recurso")]
        public string DestinoId { get; set; }

        [Display(Name = "Licitação")]
        public string LicitacaoId { get; set; }

        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }
        


        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }


        public string MensagemServicoProdesp { get; set; }
        public string MensagemServicoSiafem { get; set; }
        public string MensagemServicoSiafisico { get; set; }


        public bool TransmitidoProdesp { get; set; }
        public bool TransmitidoSiafem { get; set; }
        public bool TransmitidoSiafisico { get; set; }
        public bool TransmitirProdesp { get; set; }
        public bool TransmitirSiafem { get; set; }
        public bool TransmitirSiafisico { get; set; }
        public bool CadastroCompleto { get; set; }


        public FiltroGridViewModel CreateInstance(IEmpenho objModel, IEnumerable<IMes> meses, IEnumerable<Programa> programa, IEnumerable<Estrutura> estrutura, IEnumerable<Fonte> fonte, IEnumerable<Licitacao> licitacao, IEnumerable<Destino> destino)
        {
            var _destino = destino.FirstOrDefault(w => w.Codigo == objModel.DestinoId)?.Descricao;
            var _programa = programa.FirstOrDefault(w => w.Codigo == objModel.ProgramaId)?.Cfp;
            var _natureza = estrutura.SingleOrDefault(w => w.Codigo == objModel.NaturezaId)?.Natureza;
            var _fonte = fonte.FirstOrDefault(w => w.Id == objModel.FonteId)?.Codigo;
            var _licitacao = licitacao.FirstOrDefault(w => w.Id == objModel.LicitacaoId)?.Descricao;
            var _meses = meses.Where(w => w.Id == objModel.Id).Sum(z => z.ValorMes / 100);

            
            return new FiltroGridViewModel()
            {
                Id = Convert.ToString(objModel.Id),

                NumeroEmpenhoProdesp = objModel.NumeroEmpenhoProdesp,
                NumeroEmpenhoSiafem = objModel.NumeroEmpenhoSiafem,
                NumeroEmpenhoSiafisico = objModel.NumeroEmpenhoSiafisico,

                DestinoId = Convert.ToString(_destino),
                ProgramaId = Convert.ToString(_programa.Formatar("00.000.0000.0000")),
                NaturezaId = Convert.ToString(_natureza.Formatar("0.0.00.00")),
                FonteId = Convert.ToString(_fonte),
                LicitacaoId = Convert.ToString(_licitacao),

                StatusProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso",
                //StatusSiafemSiafisico = string.IsNullOrEmpty(objModel.StatusSiafemSiafisico) || objModel.StatusSiafemSiafisico.Equals("N") ? "Não Transmitido" : objModel.StatusSiafemSiafisico.Equals("E") ? "Erro" : "Sucesso",
                //StatusSiafemSiafisico = string.IsNullOrEmpty(objModel.StatusSiafemSiafisico) || objModel.StatusSiafemSiafisico.Equals("N") ? "Não Transmitido" : objModel.StatusSiafemSiafisico.Equals("E") ? "Erro" : "Sucesso",
                StatusSiafemSiafisico = string.IsNullOrEmpty(objModel.StatusSiafemSiafisico) || objModel.StatusSiafemSiafisico == "N" ? "Não Transmitido" : objModel.StatusSiafemSiafisico == "E"? "Erro" : "Sucesso",
                TransmitidoProdesp = objModel.TransmitidoProdesp,
                TransmitidoSiafem = objModel.TransmitidoSiafem,
                TransmitidoSiafisico = objModel.TransmitidoSiafisico,
                TransmitirProdesp = objModel.TransmitirProdesp,
                TransmitirSiafem = objModel.TransmitirSiafem,
                TransmitirSiafisico = objModel.TransmitirSiafisico,
                CadastroCompleto = objModel.CadastroCompleto,

                MensagemServicoProdesp = objModel.MensagemServicoProdesp,
                MensagemSiafemSiafisico = objModel.MensagemSiafemSiafisico,

                ValorTotal = _meses
            };
        }

        public string MensagemSiafemSiafisico { get; set; }

        [Display(Name = "Status SIAFEM / SIAFISICO")]
        public string StatusSiafemSiafisico { get; set; }
    }
}