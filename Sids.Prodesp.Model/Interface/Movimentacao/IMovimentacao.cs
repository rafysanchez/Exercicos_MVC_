namespace Sids.Prodesp.Model.Interface.Movimentacao
{
    using Entity.Movimentacao;
    using Model.Base.Movimentacao;
    using System;
    using System.Collections.Generic;
    public interface IMovimentacao
    {
        int NrAgrupamento { get; set; }

        int AnoExercicio { get; set; }
        
        string OrigemRecurso { get; set; }

        string DestinoRecurso { get; set; }

        string FlProc { get; set; }
        
        string UnidadeGestoraEmitente { get; set; }

        string GestaoEmitente { get; set; }

        string UnidadeGestoraFavorecida { get; set; }

        string IdGestaoFavorecida { get; set; }

        string NrOrgao { get; set; }

        string NrObra { get; set; }
        
        

        DateTime DataCadastro { get; set; }

        bool TransmitidoSiafem { get; set; }

        bool TransmitirSiafem { get; set; }

        string StatusSiafem { get; set; }

        DateTime DataSiafem { get; set; }

        bool TransmitidoProdesp { get; set; }

        bool TransmitirProdesp { get; set; }

        string StatusProdesp { get; set; }

        DateTime DataProdesp { get; set; }

        string MensagemProdesp { get; set; }

        string MensagemSiafem { get; set; }
        

        string AutorizadoSupraFolha { get; set; }

        int IdEstrutura { get; set; }

        string CodigoAutorizadoAssinatura { get; set; }
        int CodigoAutorizadoGrupo { get; set; }
        string CodigoAutorizadoOrgao { get; set; }
        string DescricaoAutorizadoCargo { get; set; }
        string NomeAutorizadoAssinatura { get; set; }
        string CodigoExaminadoAssinatura { get; set; }
        int CodigoExaminadoGrupo { get; set; }
        string CodigoExaminadoOrgao { get; set; }
        string DescricaoExaminadoCargo { get; set; }
        string NomeExaminadoAssinatura { get; set; }
        string CodigoResponsavelAssinatura { get; set; }
        int CodigoResponsavelGrupo { get; set; }
        string CodigoResponsavelOrgao { get; set; }
        string DescricaoResponsavelCargo { get; set; }
        string NomeResponsavelAssinatura { get; set; }
        
        int IdPrograma { get; set; }

        int IdFonte { get; set; }

        string NrNotaCredito { get; set; }
        
        

        string StatusProdespItem { get; set; }

        string StatusSiafemItem { get; set; }

        string MensagemProdespItem { get; set; }

        string MensagemSiafemItem { get; set; }

        List<MovimentacaoCancelamento> Cancelamento { get; set; }

        List<MovimentacaoNotaDeCredito> NotasCreditos { get; set; }

        List<MovimentacaoDistribuicao> Distribuicao { get; set; }

        List<MovimentacaoReducaoSuplementacao> ReducaoSuplementacao { get; set; }

        List<MovimentacaoMes> Meses { get; set; }
    }
}
