using System;

namespace Sids.Prodesp.Model.Interface.Reserva
{
    public interface IReserva
    {
        int Codigo { get; set; }
        string NumProdesp { get; set; }
        string NumSiafemSiafisico { get; set; }
        int? Fonte { get; set; }
        int? Estrutura { get; set; }
        int? Programa { get; set; }
        short? Regional { get; set; }
        string Contrato { get; set; }
        string Processo { get; set; }
        int? Obra { get; set; }
        string Oc { get; set; }
        string Ugo { get; set; }
        string Uo { get; set; }
        int? Evento { get; set; }
        int? AnoExercicio { get; set; }
        string OrigemRecurso { get; set; }
        string DestinoRecurso { get; set; }
        string Observacao { get; set; }
        bool? TransmitidoProdesp { get; set; }
        bool? TransmitidoSiafem { get; set; }
        bool? TransmitidoSiafisico { get; set; }
        bool TransmitirProdesp { get; set; }
        bool TransmitirSiafem { get; set; }
        bool TransmitirSiafisico { get; set; }
        string AutorizadoSupraFolha { get; set; }
        string EspecificacaoDespesa { get; set; }
        string DescEspecificacaoDespesa { get; set; }
        string AutorizadoAssinatura { get; set; }
        string AutorizadoGrupo { get; set; }
        string AutorizadoOrgao { get; set; }
        string NomeAutorizadoAssinatura { get; set; }
        string AutorizadoCargo { get; set; }
        string ExaminadoAssinatura { get; set; }
        string ExaminadoGrupo { get; set; }
        string ExaminadoOrgao { get; set; }
        string NomeExaminadoAssinatura { get; set; }
        string ExaminadoCargo { get; set; }
        string ResponsavelAssinatura { get; set; }
        string ResponsavelGrupo { get; set; }
        string ResponsavelOrgao { get; set; }
        string NomeResponsavelAssinatura { get; set; }
        string ResponsavelCargo { get; set; }
        DateTime DataEmissao { get; set; }
        DateTime? DataEmissaoDe { get; set; }
        DateTime? DataEmissaoAte { get; set; }
        string StatusSiafemSiafisico { get; set; }
        string StatusProdesp { get; set; }
        bool StatusDoc { get; set; }
        DateTime? DataTransmissaoProdesp { get; set; }
        DateTime? DataTransmissaoSiafemSiafisico { get; set; }
        DateTime? DataCadastro { get; set; }
        bool CadastroCompleto { get; set; }
        string CfpPrograma { get; set; }
        string DescricaoPrograma { get; set; }
        string NaturezaEstrutura { get; set; }
        decimal ValorMes { get; set; }
        string MsgRetornoTransmissaoProdesp { get; set; }
        string MsgRetornoTransSiafemSiafisico { get; set; }
        int? Tipo { get; }
    }
}
