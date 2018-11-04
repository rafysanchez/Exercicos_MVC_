using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Movimentacao;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    using Model.Interface.Movimentacao;
    using System.ComponentModel.DataAnnotations;

    public class DadoDistribuicaoSuplementacaoViewModel
    {

        public string IdDistribuicao { get; set; }

        public string IdSuplementacao { get; set; }

        public int IdMovimentacao { get; set; }

        public int NrAgrupamento { get; set; }
        
        public int NrSequencia { get; set; }
        

        [Display(Name = "N° Distr.")]
        public string NrNotaDistribuicao { get; set; }

        [Display(Name = "N° Suplem.")]
        public string NrSuplementacaoReducao { get; set; }
        

        [Display(Name = "UG Emitente")]
        public string UnidadeGestoraEmitente { get; set; }

        [Display(Name = "UG Favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Órgão")]
        public string NrOrgao { get; set; }

        [Display(Name = "Fonte")]
        public string Fonte { get; set; }

        [Display(Name = "Categoria de Gasto")]
        public string CategoriaGasto { get; set; }


        public string IdGestaoFavorecida { get; set; }

        public string EventoNC { get; set; }


        public int CenarioSiafemSiafisico { get; set; }
        public int ProgramaId { get; set; }

        public int NaturezaId { get; set; }

        public int IdTipoDocumento { get; set; }

        public int IdTipoMovimentacao { get; set; }

        public string NrProcesso { get; set; }


        public string FlProc { get; set; }

        public string NrObra { get; set; }

        public string FlagRedistribuicao { get; set; }

        public string NrCnpjCpf { get; set; }

        public string OrigemRecurso { get; set; }

        public string Uo { get; set; }

        public string FonteRecurso { get; set; }

        public string NatDespesa { get; set; }


        public string DestinoRecurso { get; set; }


        public string EspecDespesa { get; set; }

        public string DescEspecDespesa { get; set; }


        public string CodigoAutorizadoAssinatura { get; set; }
        public int CodigoAutorizadoGrupo { get; set; }
        public string CodigoAutorizadoOrgao { get; set; }
        public string DescricaoAutorizadoCargo { get; set; }
        public string NomeAutorizadoAssinatura { get; set; }

        public string CodigoExaminadoAssinatura { get; set; }
        public int CodigoExaminadoGrupo { get; set; }
        public string CodigoExaminadoOrgao { get; set; }
        public string DescricaoExaminadoCargo { get; set; }
        public string NomeExaminadoAssinatura { get; set; }

        public string CodigoResponsavelAssinatura { get; set; }
        public int CodigoResponsavelGrupo { get; set; }
        public string CodigoResponsavelOrgao { get; set; }
        public string DescricaoResponsavelCargo { get; set; }
        public string NomeResponsavelAssinatura { get; set; }


        [Display(Name = "Total")]
        public decimal Valor { get; set; }
        

        public decimal TotalQ1 { get; set; }

        public decimal TotalQ2 { get; set; }
        
        public decimal TotalQ3 { get; set; }

        public decimal TotalQ4 { get; set; }
        

        public string Observacao { get; set; }

        public string Observacao2 { get; set; }

        public string Observacao3 { get; set; }


        [Display(Name = "Status Prodesp")]
        public string TransmitidoProdesp { get; set; }

        public string MensagemProdesp { get; set; }

        [Display(Name = "Status Siafem")]
        public string TransmitidoSiafem { get; set; }

        public string MensagemSiafem { get; set; }


        public DadoDistribuicaoSuplementacaoViewModel CreateInstance(MovimentacaoDistribuicao objModel, string ugEmitente)
        {
            DadoDistribuicaoSuplementacaoViewModel dado = new DadoDistribuicaoSuplementacaoViewModel();
            dado.IdDistribuicao = objModel.Id > 0 ? objModel.Id.ToString() : default(string);
            dado.NrNotaDistribuicao = objModel.NumeroSiafem;
            dado.UnidadeGestoraEmitente = ugEmitente;
            dado.UnidadeGestoraFavorecida = objModel.UnidadeGestoraFavorecida;
            dado.Fonte = objModel.IdFonte?.ToString().PadLeft(3, '0');
            dado.CategoriaGasto = objModel.CategoriaGasto;
            dado.Valor = objModel.Valor;

            dado.IdMovimentacao = objModel.IdMovimentacao;
            dado.NrAgrupamento = objModel.NrAgrupamento;
            dado.NrSequencia = objModel.NrSequencia;
            dado.IdGestaoFavorecida = objModel.GestaoFavorecida;
            dado.EventoNC = objModel.EventoNC;

            dado.IdTipoDocumento = 2;
            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;
            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";

            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            return dado;
        }
        public DadoDistribuicaoSuplementacaoViewModel CreateInstance(MovimentacaoCancelamento objModel, string ugEmitente)
        {
            DadoDistribuicaoSuplementacaoViewModel dado = new DadoDistribuicaoSuplementacaoViewModel();
            dado.IdDistribuicao = objModel.Id > 0 ? objModel.Id.ToString() : default(string);
            dado.NrNotaDistribuicao = objModel.NumeroSiafem;
            dado.UnidadeGestoraEmitente = ugEmitente;
            dado.UnidadeGestoraFavorecida = objModel.UnidadeGestoraFavorecida;
            dado.Fonte = objModel.IdFonte.ToString().PadLeft(3, '0');
            dado.CategoriaGasto = objModel.CategoriaGasto;
            dado.Valor = objModel.Valor;

            dado.IdMovimentacao = objModel.IdMovimentacao;
            dado.NrAgrupamento = objModel.NrAgrupamento;
            dado.NrSequencia = objModel.NrSequencia;
            dado.IdGestaoFavorecida = objModel.GestaoFavorecida;
            dado.EventoNC = objModel.EventoNC;

            dado.IdTipoDocumento = 2;
            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;
            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";

            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            return dado;
        }
        public DadoDistribuicaoSuplementacaoViewModel CreateInstance(MovimentacaoReducaoSuplementacao objModel)
        {
            DadoDistribuicaoSuplementacaoViewModel dado = new DadoDistribuicaoSuplementacaoViewModel();
            dado.IdDistribuicao = objModel.IdDistribuicao > 0 ? objModel.IdDistribuicao.ToString() : default(string);
            dado.IdSuplementacao = objModel.Id > 0 ? objModel.Id.ToString() : default(string);
            dado.NrNotaDistribuicao = objModel.IdNotaCredito== 0 ? string.Empty : objModel.IdNotaCredito.ToString();
            dado.NrSuplementacaoReducao = objModel.NrSuplementacaoReducao;
            dado.UnidadeGestoraFavorecida = objModel.UnidadeGestora;
            dado.NrOrgao = objModel.NrOrgao;
            dado.Fonte = objModel.IdFonte.ToString().PadLeft(3, '0');
            dado.Valor = objModel.Valor;

            dado.IdMovimentacao = objModel.IdMovimentacao;
            dado.NrAgrupamento = objModel.NrAgrupamento;
            dado.NrSequencia = objModel.NrSequencia;
            dado.TotalQ1 = objModel.TotalQ1;
            dado.TotalQ2 = objModel.TotalQ2;
            dado.TotalQ3 = objModel.TotalQ3;
            dado.TotalQ4 = objModel.TotalQ4;

            dado.ProgramaId = objModel.IdPrograma;
            dado.NaturezaId = objModel.IdEstrutura;
            dado.IdTipoDocumento = 2;
            dado.IdTipoMovimentacao = objModel.IdTipoMovimentacao;
            dado.NrProcesso = objModel.NrProcesso;
            dado.FlProc = objModel.FlProc;
            dado.NrObra = objModel.NrObra;
            dado.NrCnpjCpf = objModel.NrCnpjCpf;
            dado.OrigemRecurso = objModel.OrigemRecurso;
            dado.DestinoRecurso = objModel.DestinoRecurso;
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