using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Movimentacao;
using System.ComponentModel.DataAnnotations;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{

    public abstract class DadoReducaoSuplementacaoViewModel
    {
        public string Id { get; set; }

        public int IdMovimentacao { get; set; }

        public int NrAgrupamento { get; set; }

        public int NrSequencia { get; set; }

        [Display(Name = "Órgão")]
        public string NrOrgao { get; set; }


        [Display(Name = "Total")]
        public decimal ValorTotal { get; set; }

        public string IdGestaoFavorecida { get; protected set; }
        public string EventoNC { get; protected set; }
        public decimal TotalQ1 { get; protected set; }
        public decimal TotalQ3 { get; protected set; }
        public decimal TotalQ2 { get; protected set; }
        public decimal TotalQ4 { get; protected set; }
        public int ProgramaId { get; protected set; }
        public int NaturezaId { get; protected set; }
        public int IdTipoDocumento { get; protected set; }
        public int IdTipoMovimentacao { get; protected set; }
        public string NrProcesso { get; protected set; }
        public string FlProc { get; protected set; }
        public string NrObra { get; protected set; }
        public string RedSup { get; protected set; }
        public string EspecDespesa { get; protected set; }
        public string DescEspecDespesa { get; protected set; }
        public string CodigoAutorizadoAssinatura { get; protected set; }
        public int CodigoAutorizadoGrupo { get; protected set; }
        public string CodigoAutorizadoOrgao { get; protected set; }
        public string DescricaoAutorizadoCargo { get; protected set; }
        public string NomeAutorizadoAssinatura { get; protected set; }
        public string CodigoExaminadoAssinatura { get; protected set; }
        public int CodigoExaminadoGrupo { get; protected set; }
        public string CodigoExaminadoOrgao { get; protected set; }
        public string DescricaoExaminadoCargo { get; protected set; }
        public string NomeExaminadoAssinatura { get; protected set; }
        public string CodigoResponsavelAssinatura { get; protected set; }
        public int CodigoResponsavelGrupo { get; protected set; }
        public string CodigoResponsavelOrgao { get; protected set; }
        public string DescricaoResponsavelCargo { get; protected set; }
        public string NomeResponsavelAssinatura { get; protected set; }
        public string MensagemProdesp { get; protected set; }
        public string MensagemSiafem { get; protected set; }
        public string TransmitidoProdesp { get; protected set; }
        public string TransmitidoSiafem { get; protected set; }
    }
}