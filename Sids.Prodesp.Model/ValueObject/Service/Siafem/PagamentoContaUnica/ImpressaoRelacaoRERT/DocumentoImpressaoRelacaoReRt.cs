namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    public class DocumentoImpressaoRelacaoReRt
    {
        public string UnidadeGestora { get; set; }

        public string Gestao { get; set; }

        public string Banco { get; set; }

        public string DataSolicitacao { get; set; }

        public string NumeroRelatorio { get; set; }

        public string PrefixoREouRT { get; set; }

        public string NumREouRT { get; set; }
    }
}