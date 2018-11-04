using System;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class RespostaImpressaoRelacaoReRt
    {
        public RespostaImpressaoRelacaoReRt() { }

        [XmlElement(ElementName = "DATA_REFERENCIA")]
        public string DataReferencia { get; set; }

        [XmlElement(ElementName = "CODIGO_RELATORIO")]
        public string CodigoRelatorio { get; set; }

        [XmlElement(ElementName = "RELOB")]
        public string Relob { get; set; }

        [XmlElement(ElementName = "UNIDADE_GESTORA")]
        public string UnidadeGestora { get; set; }

        [XmlElement(ElementName = "NOME_DA_UNIDADE_GESTORA")]
        public string NomeDaUnidadeGestoraRE { get; set; }

        [XmlElement(ElementName = "NOME_UNIDADE_GESTORA")]
        public string NomeDaUnidadeGestoraRT { get; set; }

        [XmlElement(ElementName = "GESTAO")]
        public string Gestao { get; set; }

        [XmlElement(ElementName = "NOME_DA_GESTAO")]
        public string NomeDaGestaoRE { get; set; }

        [XmlElement(ElementName = "NOME_GESTAO")]
        public string NomeDaGestaoRT { get; set; }

        [XmlElement(ElementName = "BANCO")]
        public string Banco { get; set; }

        [XmlElement(ElementName = "NOME_DO_BANCO")]
        public string NomeDoBancoRE { get; set; }

        [XmlElement(ElementName = "NOME_BANCO")]
        public string NomeDoBancoRT { get; set; }

        [XmlElement(ElementName = "AGENCIA")]
        public string Agencia { get; set; }

        [XmlElement(ElementName = "NOME_DA_AGENCIA")]
        public string NomeDaAgencia { get; set; }

        [XmlElement(ElementName = "CONTA_C")]
        public string ContaC { get; set; }

        [XmlElement(ElementName = "VALOR_TOTAL_DOCUMENTO")]
        public decimal ValorTotalDocumento { get; set; }

        [XmlElement(ElementName = "VALOR_POR_EXTENSO")]
        public string ValorPorExtenso { get; set; }

        [XmlElement(ElementName = "TEXTO_AUTORIZACAO")]
        public string TextoAutorizacao { get; set; }

        [XmlElement(ElementName = "DATA_EMISSAO")]
        public string DataEmissao { get; set; }

        [XmlElement(ElementName = "CIDADE")]
        public string Cidade { get; set; }

        [XmlElement(ElementName = "NOME_ORDENADOR_ASSINATURA")]
        public string NomeOrdenadorAssinatura { get; set; }

        [XmlElement(ElementName = "NOME_GESTOR_FINANCEIRO")]
        public string NomeGestorFinanceiro { get; set; }

        [XmlElement(ElementName = "REPETICOES")]
        public RepeticoesReRt RepeticoesReRt { get; set; }

        [XmlElement(ElementName = "MsgErro")]
        public string MsgErro { get; set; }

        [XmlElement(ElementName = "MsgRetorno")]
        public string MsgRetorno { get; set; }

        [XmlElement(ElementName = "StatusOperacao")]
        public bool StatusOperacao { get; set; }

        public string SemReRt { get; set; }
    }
}
