namespace Sids.Prodesp.Model.Interface.Empenho
{
    using System;

    public interface IEmpenho
    {
        int Id { get; set; }
        short RegionalId { get; set; }
        int ProgramaId { get; set; }
        int NaturezaId { get; set; }
        int FonteId { get; set; }
        string LicitacaoId { get; set; }
        int ModalidadeId { get; set; }
        int TipoAquisicaoId { get; set; }
        int OrigemMaterialId { get; set; }
        string DestinoId { get; set; }
        int TipoObraId { get; set; }
        int NumeroAnoExercicio { get; set; }
        string NumeroProcesso { get; set; }
        string NumeroProcessoNE { get; set; }
        string NumeroProcessoSiafisico { get; set; }
        string NumeroContrato { get; set; }
        string NumeroCT { get; set; }
        string NumeroEmpenhoProdesp { get; set; }
        string NumeroEmpenhoSiafem { get; set; }
        string NumeroEmpenhoSiafisico { get; set; }
        DateTime DataCadastramento { get; set; }
        string NumeroCNPJCPFUGCredor { get; set; }
        string CodigoGestaoCredor { get; set; }
        int CodigoCredorOrganizacao { get; set; }
        string NumeroCNPJCPFFornecedor { get; set; }
        string CodigoUnidadeGestora { get; set; }
        string CodigoGestao { get; set; }
        int CodigoEvento { get; set; }
        DateTime DataEmissao { get; set; }
        string CodigoUnidadeGestoraFornecedora { get; set; }
        string CodigoGestaoFornecedora { get; set; }
        int CodigoUO { get; set; }
        string CodigoUnidadeFornecimento { get; set; }

        string DescricaoAutorizadoSupraFolha { get; set; }
        string CodigoEspecificacaoDespesa { get; set; }
        string DescricaoEspecificacaoDespesa { get; set; }

        string CodigoAutorizadoAssinatura { get; set; }
        int CodigoAutorizadoGrupo { get; set; }
        string CodigoAutorizadoOrgao { get; set; }
        string NomeAutorizadoAssinatura { get; set; }
        string DescricaoAutorizadoCargo { get; set; }
        string CodigoExaminadoAssinatura { get; set; }
        int CodigoExaminadoGrupo { get; set; }
        string CodigoExaminadoOrgao { get; set; }
        string NomeExaminadoAssinatura { get; set; }
        string DescricaoExaminadoCargo { get; set; }
        string CodigoResponsavelAssinatura { get; set; }
        int CodigoResponsavelGrupo { get; set; }
        string CodigoResponsavelOrgao { get; set; }
        string NomeResponsavelAssinatura { get; set; }
        string DescricaoResponsavelCargo { get; set; }

        bool TransmitirProdesp { get; set; }
        bool TransmitidoProdesp { get; set; }
        DateTime DataTransmitidoProdesp { get; set; }
        bool TransmitirSiafem { get; set; }
        bool TransmitidoSiafem { get; set; }
        DateTime DataTransmitidoSiafem { get; set; }
        bool TransmitirSiafisico { get; set; }
        bool TransmitidoSiafisico { get; set; }
        DateTime DataTransmitidoSiafisico { get; set; }
        string StatusProdesp { get; set; }
        string StatusSiafemSiafisico { get; set; }
        bool StatusDocumento { get; set; }
        bool CadastroCompleto { get; set; }
        string MensagemServicoProdesp { get; set; }
        string MensagemServicoSiafem { get; set; }
        string MensagemSiafemSiafisico { get; set; }
        string MensagemServicoSiafisico { get; set; }


        DateTime DataCadastramentoDe { get; set; }
        DateTime DataCadastramentoAte { get; set; }

        string CodigoNaturezaItem { get; set; }

        string CodigoAplicacaoObra { get; set; }

        string StatusSiafisicoNE { get; set; }
        string StatusSiafisicoCT { get; set; }
        string CodigoMunicipio { get; set; }
        string DescricaoAcordo { get; set; }
        string DescricaoLocalEntregaSiafem { get; set; }
        string NumeroOriginalCT { get; set; }

        bool ContBec { get; set; }
    }
}
