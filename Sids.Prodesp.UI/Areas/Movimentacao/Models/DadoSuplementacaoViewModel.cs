using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Movimentacao;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    using Model.Interface.Movimentacao;
    using System.ComponentModel.DataAnnotations;

    public class DadoSuplementacaoViewModel : DadoReducaoSuplementacaoViewModel
    {
        [Display(Name = "N° Suplem.")]
        public string NrSuplementacao { get; set; }
        
                
        public DadoSuplementacaoViewModel CreateInstance(MovimentacaoReducaoSuplementacao objModel)
        {
            var dado = new DadoSuplementacaoViewModel();

            dado.Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string);
            dado.NrSuplementacao = objModel.NrSuplementacaoReducao;

            //campos do grid
            dado.NrOrgao = objModel.NrOrgao;
            dado.ValorTotal = objModel.Valor;
            //campos do grid

            dado.IdMovimentacao = objModel.IdMovimentacao;
            dado.NrAgrupamento = objModel.NrAgrupamento;
            dado.NrSequencia = objModel.NrSequencia;

            dado.IdGestaoFavorecida = objModel.GestaoFavorecido;
            //dado.EventoNC = objModel.EventoNC;

            dado.TotalQ1 = objModel.TotalQ1;
            dado.TotalQ2 = objModel.TotalQ2;
            dado.TotalQ3 = objModel.TotalQ3;
            dado.TotalQ4 = objModel.TotalQ4;

            dado.ProgramaId = objModel.IdPrograma;
            dado.NaturezaId = objModel.IdFonte;

            dado.IdTipoDocumento = 1;
            dado.IdTipoMovimentacao = objModel.IdTipoMovimentacao;

            dado.NrProcesso = objModel.NrProcesso;
            dado.FlProc = objModel.FlProc;
            dado.NrObra = objModel.NrObra;
            dado.RedSup = objModel.RedSup;

            dado.EspecDespesa = objModel.EspecDespesa;
            dado.DescEspecDespesa = objModel.DescEspecDespesa;

            dado.CodigoAutorizadoAssinatura = objModel.CodigoAutorizadoAssinatura;
            dado.CodigoAutorizadoGrupo = objModel.CodigoAutorizadoGrupo;
            dado.CodigoAutorizadoOrgao = objModel.CodigoAutorizadoOrgao;
            dado.DescricaoAutorizadoCargo = objModel.DescricaoAutorizadoCargo;
            dado.NomeAutorizadoAssinatura = objModel.NomeAutorizadoAssinatura;
            dado.CodigoExaminadoAssinatura = objModel.CodigoExaminadoAssinatura;
            dado.CodigoExaminadoGrupo = objModel.CodigoExaminadoGrupo;
            dado.CodigoExaminadoOrgao = objModel.CodigoExaminadoOrgao;
            dado.DescricaoExaminadoCargo = objModel.DescricaoExaminadoCargo;
            dado.NomeExaminadoAssinatura = objModel.NomeExaminadoAssinatura;
            dado.CodigoResponsavelAssinatura = objModel.CodigoResponsavelAssinatura;
            dado.CodigoResponsavelGrupo = objModel.CodigoResponsavelGrupo;
            dado.CodigoResponsavelOrgao = objModel.CodigoResponsavelOrgao;
            dado.DescricaoResponsavelCargo = objModel.DescricaoResponsavelCargo;
            dado.NomeResponsavelAssinatura = objModel.NomeResponsavelAssinatura;

            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;
            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";

            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            return dado;
        }
    }
}