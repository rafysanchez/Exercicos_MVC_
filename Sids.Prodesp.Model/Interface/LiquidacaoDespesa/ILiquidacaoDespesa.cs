namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Base.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;

    public interface ILiquidacaoDespesa
    {
        int Id { get; set; }
        string CodigoTarefa { get; set; }
        string CodigoDespesa { get; set; }
        string NumeroRecibo { get; set; }
        string PrazoPagamento { get; set; }
        DateTime DataRealizado { get; set; }
        DateTime DataCadastro { get; set; }
        short RegionalId { get; set; }
        string NumeroContrato { get; set; }
        bool TransmitirProdesp { get; set; }
        bool TransmitirSiafem { get; set; }
        bool TransmitirSiafisico { get; set; }
        string NumeroProdesp { get; set; }
        string NlReferencia { get; set; }
        string NumeroSiafemSiafisico { get; set; }
        int CenarioSiafemSiafisico { get; set; }
        int ValorRealizado { get; set; }
        int ValorAnular { get; set; }
        string NumeroOriginalProdesp { get; set; }
        string NumeroSubempenhoProdesp { get; set; }
        string CenarioProdesp { get; set; }
        string NumeroCT { get; set; }
        string NumeroOriginalSiafemSiafisico { get; set; }
        string CodigoUnidadeGestora { get; set; }
        string CodigoGestao { get; set; }
        int Valor { get; set; }
        string CodigoAplicacaoObra { get; set; }
        string  CodigoTipoDeObra { get; set; }
        
        DateTime DataEmissao { get; set; }
        int TipoEventoId { get; set; }
        int CodigoEvento { get; set; }
        string NumeroCNPJCPFCredor { get; set; }
        string CodigoGestaoCredor { get; set; }
        string AnoMedicao { get; set; }
        string MesMedicao { get; set; }
        string Percentual { get; set; }
        int TipoObraId { get; set; }
        string CodigoUnidadeGestoraObra { get; set; }
        string NumeroObra { get; set; }
        string DescricaoObservacao1 { get; set; }
        string DescricaoObservacao2 { get; set; }
        string DescricaoObservacao3 { get; set; }
        string NumeroProcesso { get; set; }
        string Referencia { get; set; }
        string DescricaoAutorizadoSupraFolha { get; set; }
        string CodigoEspecificacaoDespesa { get; set; }
        string DescricaoEspecificacaoDespesa1 { get; set; }
        string DescricaoEspecificacaoDespesa2 { get; set; }
        string DescricaoEspecificacaoDespesa3 { get; set; }
        string DescricaoEspecificacaoDespesa4 { get; set; }
        string DescricaoEspecificacaoDespesa5 { get; set; }
        string DescricaoEspecificacaoDespesa6 { get; set; }
        string DescricaoEspecificacaoDespesa7 { get; set; }
        string DescricaoEspecificacaoDespesa8 { get; set; }
        string DescricaoEspecificacaoDespesa9 { get; set; }
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
        string StatusProdesp { get; set; }
        bool TransmitidoProdesp { get; set; }
        DateTime DataTransmitidoProdesp { get; set; }
        string MensagemProdesp { get; set; }
        string StatusSiafemSiafisico { get; set; }
        bool TransmitidoSiafem { get; set; }
        bool TransmitidoSiafisico { get; set; }
        DateTime DataTransmitidoSiafemSiafisico { get; set; }
        string MensagemSiafemSiafisico { get; set; }
        bool CadastroCompleto { get; set; }
        bool StatusDocumento { get; set; }
        string NaturezaSubempenhoId { get; set; }
        IEnumerable<LiquidacaoDespesaEvento> Eventos { get; set; }
        IEnumerable<LiquidacaoDespesaNota> Notas { get; set; }
        IEnumerable<LiquidacaoDespesaItem> Itens { get; set; }
        string Normal { get; }
        string Estorno { get; }

        DateTime DataVencimento { get; set; }
        string NlRetencaoInss { get; set; }

        string Lista { get; set; }

        bool ReferenciaDigitada { get; set; }
        int ProgramaId { get; set; }
    }
}