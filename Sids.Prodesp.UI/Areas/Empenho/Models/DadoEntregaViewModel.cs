namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DadoEntregaViewModel
    {
        [Display(Name = "Data de Entrega")]
        public string DataEntregaMaterialSiafisico { get; set; }

        [Display(Name = "Logradouro")]
        public string DescricaoLogradouroEntrega { get; set; }

        [Display(Name = "Bairro")]
        public string DescricaoBairroEntrega { get; set; }

        [Display(Name = "Cidade")]
        public string DescricaoCidadeEntrega { get; set; }

        [Display(Name = "CEP")]
        public string NumeroCEPEntrega { get; set; }

        [Display(Name = "Informações Adicionais")]
        public string DescricaoInformacoesAdicionaisEntrega { get; set; }


        public DadoEntregaViewModel CreateInstance(DateTime entregaMaterial, string logradouro, string bairro, string cidade, string cep, string informacoesAdicionais)
        {
            return new DadoEntregaViewModel()
            {
                DataEntregaMaterialSiafisico = entregaMaterial > default(DateTime) ? entregaMaterial.ToShortDateString() : default(string),
                DescricaoLogradouroEntrega = logradouro,
                DescricaoBairroEntrega = bairro,
                DescricaoCidadeEntrega = cidade,
                NumeroCEPEntrega = cep,
                DescricaoInformacoesAdicionaisEntrega = informacoesAdicionais
            };
        }               
    }
}