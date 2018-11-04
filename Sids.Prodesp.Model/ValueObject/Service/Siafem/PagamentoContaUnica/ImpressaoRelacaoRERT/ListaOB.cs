using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "documento")]
    public class ListaOB
    {
        public ListaOB() { }

        [XmlElement(ElementName = "NUMERO")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "IND_PRIORIDADE")]
        public string IndPrioridade { get; set; }

        [XmlElement(ElementName = "TIPO_OB")]
        public string TipoOb { get; set; }

        [XmlElement(ElementName = "NOME_DO_FAVORECIDO")]
        public string NomeDoFavorecido { get; set; }

        [XmlElement(ElementName = "BANCO_FAVORECIDO")]
        public string BancoFavorecido { get; set; }

        [XmlElement(ElementName = "AGENCIA_FAVORECIDO")]
        public string AgenciaFavorecido { get; set; }

        [XmlElement(ElementName = "CONTA_FAVORECIDO")]
        public string ContaFavorecido { get; set; }

        [XmlElement(ElementName = "VALOR_OB")]
        public decimal ValorOb { get; set; }

        [XmlElement(ElementName = "CONTA_BANCARIA_EMITENTE")]
        public string ContaBancariaEmitente { get; set; }

        [XmlElement(ElementName = "UNIDADE_GESTORA_FAVORECIDA")]
        public string UnidadeGestoraFavorecida { get; set; }

        [XmlElement(ElementName = "GESTAO_FAVORECIDA")]
        public string GestaoFavorecida { get; set; }

        [XmlElement(ElementName = "MNEMONICO_UG_FAVORECIDA")]
        public string MnemonicoUgFavorecida { get; set; }
    }
}
