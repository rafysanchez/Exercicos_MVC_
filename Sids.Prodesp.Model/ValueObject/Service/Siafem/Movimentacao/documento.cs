using System;
using System.Collections.Generic;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao
{
    public class documento
    {
        private string UgPagadora;


       
        public string DataEmissao { get; set; }
        public string UnidadeGestora { get; set; }
        public string Gestao { get; set; }

        public string UgFavorecida { get; set; }
        public string GestaoFavorecida { get; set; }
        public string FonteTesouro { get; set; }
        public string FonteNaoTesouro { get; set; }

        public string UGEmitente { get; set; }
        public string GestaoEmitente { get; set; }

        public string CategoriaGasto { get; set; }

        //public string Mes1 { get; set; }
        //public string Mes2 { get; set; }
        //public string Mes3 { get; set; }
        //public string Mes4 { get; set; }
        //public string Mes5 { get; set; }
        //public string Mes6 { get; set; }
        //public string Mes7 { get; set; }
        //public string Mes8 { get; set; }
        //public string Mes9 { get; set; }


        //public string Valor1 { get; set; }
        //public string Valor2 { get; set; }
        //public string Valor3 { get; set; }
        //public string Valor4 { get; set; }
        //public string Valor5 { get; set; }
        //public string Valor6 { get; set; }
        //public string Valor7 { get; set; }
        //public string Valor8 { get; set; }
        //public string Valor9 { get; set; }






        public string PTRes { get; set; }
        public string Processo { get; set; }
        public string UO { get; set; }
        public string PT { get; set; }
        public string Fonte { get; set; }
        public string NaturezaDespesa { get; set; }
        public string UGO { get; set; }
        public string UG { get; set; }
        public string Ug { get; set; }
        public string NumOC { get; set; }
        public string PlanoInterno { get; set; }

        public string Valor { get; set; }

     
        public string PrefixoOC { get; set; }

        public string NumeroNR { get; set; }

        public string NumeroNe { get; set; }

        public string NumeroNL { get; set; }

        public string NumeroNC { get; set; }

        public string Evento { get; set; }


        public string Credor { get; set; }


        public string GestaoCredor { get; set; }
        public string Municipio { get; set; }


        public string Acordo { get; set; }

        public string Modalidade { get; set; }

        public string Licitacao { get; set; }

        public string ReferenciaLegal { get; set; }

        public string OrigemMaterial { get; set; }

        public string LocalEntrega { get; set; }

        public string DataEntrega { get; set; }

        public string TipoEmpenho { get; set; }

        public string TipoObra { get; set; }

        public string UGObra { get; set; }

        public string AnoContrato { get; set; }

        public string MesContrato { get; set; }

        public string NumeroObra { get; set; }





        public string CnpjCpfFornecedor { get; set; }

        public string CgcCpfUgCredor { get; set; }
        public string UGFornecedora { get; set; }

        public string GestaoFornecedora { get; set; }

        public string PTRES { get; set; }

        public string Natureza { get; set; }

        public string TipoAquisicao { get; set; }

        public string TipoLicitacao { get; set; }

        public string RefLegal { get; set; }

        public string NumeroProcesso { get; set; }

        public string NumeroContratoFornec { get; set; }

        public string NumeroEdital { get; set; }

        public string ValorEmpenhar { get; set; }



        public string NumeroCT { get; set; }
        public string Item { get; set; }
        public string UnidadeForn { get; set; }
        public string Quantidade { get; set; }
        public string QuantidadeDec { get; set; }
        public string ValorUnitario { get; set; }
        public string ValorUnitarioDec { get; set; }


        public string Seq { get; set; }
        public string UnidadeFornecimento { get; set; }
        public string ValorParteInteira { get; set; }
        public string ValorParteDecimal { get; set; }
        public string ConfirmaPreco { get; set; }
        public string JustificativaPreco1 { get; set; }
        public string JustificativaPreco2 { get; set; }
        public string JustificativaItemNao { get; set; }
        public string JustificativaValorLancado1 { get; set; }
        public string JustificativaValorLancado2 { get; set; }


        public string Mes01 { get; set; }
        public string Mes02 { get; set; }
        public string Mes03 { get; set; }
        public string Mes04 { get; set; }
        public string Mes05 { get; set; }
        public string Mes06 { get; set; }
        public string Mes07 { get; set; }
        public string Mes08 { get; set; }
        public string Mes09 { get; set; }
        public string Mes10 { get; set; }
        public string Mes11 { get; set; }
        public string Mes12 { get; set; }
        public string Valor01 { get; set; }
        public string Valor02 { get; set; }
        public string Valor03 { get; set; }
        public string Valor04 { get; set; }
        public string Valor05 { get; set; }
        public string Valor06 { get; set; }
        public string Valor07 { get; set; }
        public string Valor08 { get; set; }
        public string Valor09 { get; set; }
        public string Valor10 { get; set; }
        public string Valor11 { get; set; }
        public string Valor12 { get; set; }
        public string Observacao01 { get; set; }
        public string Observacao02 { get; set; }
        public string Observacao03 { get; set; }
        public string ValorEmpenhar1 { get; set; }
        public string ValorEmpenhar2 { get; set; }
        public string ValorEmpenhar3 { get; set; }
        public string ValorEmpenhar4 { get; set; }
        public string ValorEmpenhar5 { get; set; }
        public string ValorEmpenhar6 { get; set; }
        public string ValorEmpenhar7 { get; set; }
        public string ValorEmpenhar8 { get; set; }
        public string ValorEmpenhar9 { get; set; }
        public string ValorEmpenhar10 { get; set; }
        public string ValorEmpenhar11 { get; set; }
        public string ValorEmpenhar12 { get; set; }
        public string ValorEmpenhar13 { get; set; }
        public string ValorEmpenhar14 { get; set; }
        public string ValorEmpenhar15 { get; set; }
        public string ValorEmpenhar16 { get; set; }
        public string ValorEmpenhar17 { get; set; }


        public string EmpenhoOriginal { get; set; }
        public string NumeroEmpenho { get; set; }

        public string Validademinimadoprodutonaentregade50porcento { get; set; }
        public string Validademinimadoprodutonaentregade60porcento { get; set; }
        public string Validademinimadoprodutonaentregade75porcento { get; set; }
        public string Validademinimadoprodutonaentregade80porcento { get; set; }
        public string Validademinimadoprodutonaentregavideedital { get; set; }
        public string Validademinimadoprodutonaentregaconformelegislacaovigentemedicamentomanipulado { get; set; }

        public string PrefixoCT { get; set; }
        public string NumCT { get; set; }

        public string CnpjCpf { get; set; }
        public string UGFornecedor { get; set; }
        public string GestaoFornecedor { get; set; }
        public string Contrato { get; set; }
        public string Evento52 { get; set; }
        public string Evento92 { get; set; }
        public string CgcCpf { get; set; }
        public string Data { get; set; }
        public string ModalidadeEmpenho { get; set; }
        public string Prefixo { get; set; }
        public string CodigoMunicipio { get; set; }
        public string CPFCredor { get; set; }
        public string DataPrevista { get; set; }
        public string NaturezaDespsesa { get; set; }
        public string Origem { get; set; }
        public string Tipo { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string InformacoesAdicion { get; set; }
        public string Logradouro { get; set; }
        public string NumEdital { get; set; }

        public string NumNE { get; set; }



        #region Movimentacao


        //public static implicit operator documento(SIAFDOC v)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion






    }
}




