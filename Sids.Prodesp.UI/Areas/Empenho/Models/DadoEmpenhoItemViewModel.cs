namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Interface.Empenho;
    using System.ComponentModel.DataAnnotations;

    public class DadoEmpenhoItemViewModel
    {
        public string Id { get; set; }
        public string EmpenhoId { get; set; }

        [Display(Name = "Unidade de Medida")]
        public string DescricaoUnidadeMedida { get; set; }

        [Display(Name = "Preço Total")]
        public string ValorTotal { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoItemSiafem { get; set; }


        [Display(Name = "Item Material ou Serviço")]
        public string CodigoItemServico { get; set; }

        [Display(Name = "Unidade de Fornecimento")]
        public string CodigoUnidadeFornecimentoItem { get; set; }

        [Display(Name = "Qtd. Material ou Serviço")]
        public string QuantidadeMaterialServico { get; set; }

        [Display(Name = "Preço Unitário")]
        public string ValorUnitario { get; set; }

        [Display(Name = "Justificativa")]
        public string DescricaoJustificativaPreco { get; set; }
        
        public string StatusSiafisicoItem { get; set; }
        public string SequenciaItem { get; set; }
        

        public DadoEmpenhoItemViewModel CreateInstance(IEmpenhoItem objModel, bool siafem)
        {
            var obj = siafem
                ? new DadoEmpenhoItemViewModel()
                {
                    Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                    EmpenhoId = objModel.EmpenhoId > 0 ? objModel.EmpenhoId.ToString() : default(string),

                    DescricaoUnidadeMedida = objModel.DescricaoUnidadeMedida,
                    QuantidadeMaterialServico = "1",
                    ValorUnitario = objModel.ValorTotal > 0 ? objModel.ValorTotal.ToString() : "0,00",
                    DescricaoItemSiafem = objModel.DescricaoItemSiafem,
                    ValorTotal = objModel.ValorTotal > 0 ? objModel.ValorTotal.ToString() : "0,00"
                }
                : new DadoEmpenhoItemViewModel()
                {
                    Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                    EmpenhoId = objModel.EmpenhoId > 0 ? objModel.EmpenhoId.ToString() : default(string),

                    CodigoItemServico = objModel.CodigoItemServico,
                    CodigoUnidadeFornecimentoItem = objModel.CodigoUnidadeFornecimentoItem,
                    DescricaoJustificativaPreco = objModel.DescricaoJustificativaPreco,
                    QuantidadeMaterialServico = objModel.QuantidadeMaterialServico > 0 ? objModel.QuantidadeMaterialServico.ToString("N3") : "0,000",
                    ValorUnitario = objModel.ValorUnitario > 0 ? objModel.ValorUnitario.ToString("N2") : "0,00",
                    ValorTotal = objModel.ValorTotal > 0 ? objModel.ValorTotal.ToString("N2") : "0,00",
                    StatusSiafisicoItem = objModel.StatusSiafisicoItem,
                    SequenciaItem = objModel.SequenciaItem.ToString()
                };


            return obj;
        }
    }
}