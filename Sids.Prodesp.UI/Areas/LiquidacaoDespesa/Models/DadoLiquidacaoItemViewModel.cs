namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Base.Empenho;
    using Model.Enum;
    using Model.Extension;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class DadoLiquidacaoItemViewModel
    {
        public string Id { get; set; }
        public string SubempenhoId { get; set; }


        [Display(Name = "Item Material")]
        public string CodigoItemServico { get; set; }


        [Display(Name = "Unidade de Fornecimento")]
        public string CodigoUnidadeFornecimentoItem { get; set; }


        [Display(Name = "* Quantidade")]
        public string QuantidadeMaterialServico { get; set; }
        public string QuantidadeMaterialServicoDecimal { get; set; }

        [Display(Name = "* Valor")]
        public string Valor { get; set; }
        [Display(Name = "Quantidade a Liquidar")]
        public string QuantidadeLiquidar { get; set; }
        public string QuantidadeLiquidarDecimal { get; set; }

        public string StatusSiafisicoItem { get; set; }
        public string SequenciaItem { get; set; }
        public bool Transmitir { get; set; }

        public DadoLiquidacaoItemViewModel CreateInstance(ILiquidacaoDespesaItem objModel, bool siafem)
        {
            return
                /*siafem
                ? new DadoLiquidacaoItemViewModel()
                {
                    Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                    SubempenhoId = objModel.SubempenhoId > 0 ? objModel.SubempenhoId.ToString() : default(string),

                    QuantidadeMaterialServico = "1",
                }
                :*/
                new DadoLiquidacaoItemViewModel()
                {
                    Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                    SubempenhoId = objModel.SubempenhoId > 0 ? objModel.SubempenhoId.ToString() : default(string),

                    SequenciaItem = objModel.SequenciaItem.ToString(),
                    CodigoItemServico = objModel.CodigoItemServico,
                    CodigoUnidadeFornecimentoItem = objModel.CodigoUnidadeFornecimentoItem,

                    QuantidadeMaterialServicoDecimal = objModel.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1],

                    QuantidadeMaterialServico = objModel.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],

                    StatusSiafisicoItem = objModel.StatusSiafisicoItem,

                    Transmitir = objModel.Transmitir ?? false
                };
        }

        public DadoLiquidacaoItemViewModel CreateInstance(ILiquidacaoDespesaItem objModel)
        {
            return
                new DadoLiquidacaoItemViewModel()
                {
                    Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                    SubempenhoId = objModel.SubempenhoId > 0 ? objModel.SubempenhoId.ToString() : default(string),

                    SequenciaItem = objModel.SequenciaItem.ToString(),
                    CodigoItemServico = objModel.CodigoItemServico,
                    CodigoUnidadeFornecimentoItem = objModel.CodigoUnidadeFornecimentoItem,

                    //QuantidadeMaterialServicoDecimal = objModel.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1],
                    //QuantidadeMaterialServico = objModel.QuantidadeMaterialServico > 0 ? objModel.QuantidadeMaterialServico.ToString() : default(string),

                    QuantidadeLiquidarDecimal = objModel.QuantidadeMaterialServico > 0 ? objModel.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1] : default(string) ,
                    QuantidadeLiquidar = objModel.QuantidadeMaterialServico > 0 ? objModel.QuantidadeMaterialServico.ToString() : default(string),

                    StatusSiafisicoItem = objModel.StatusSiafisicoItem,

                    Transmitir = objModel.Transmitir ?? false,
                    Valor = objModel.Valor.ToString()
                };
        }
        public DadoLiquidacaoItemViewModel CriarInstancia(ILiquidacaoDespesaItem objModel, EnumCenarioSiafemSiafisico cenario)
        {
            var obj = new DadoLiquidacaoItemViewModel();

            var quantidadeMaterialServicoDecimal = objModel.QuantidadeMaterialServico > 0 ? objModel.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1] : "000";
            var quantidadeMaterialServico = objModel.QuantidadeMaterialServico > 0 ? objModel.QuantidadeMaterialServico.ToString() : "0,000";

            var quantidadeLiquidarDecimal = objModel.QuantidadeLiquidar > 0 ? objModel.QuantidadeLiquidar.ZeroParaNulo().Split(',')[1] : "000";
            var quantidadeLiquidar = objModel.QuantidadeLiquidar > 0 ? objModel.QuantidadeLiquidar.ToString() : "0,000";

            if (cenario == EnumCenarioSiafemSiafisico.SubempenhoBec && objModel.QuantidadeLiquidar == 0)
            {
                quantidadeLiquidarDecimal = quantidadeMaterialServicoDecimal;
                quantidadeLiquidar = quantidadeMaterialServico;
                quantidadeMaterialServicoDecimal = "000";
                quantidadeMaterialServico = "0,000";
            }


            obj.Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string);
            obj.SubempenhoId = objModel.SubempenhoId > 0 ? objModel.SubempenhoId.ToString() : default(string);
            obj.SequenciaItem = objModel.SequenciaItem.ToString();
            obj.CodigoItemServico = objModel.CodigoItemServico;
            obj.CodigoUnidadeFornecimentoItem = objModel.CodigoUnidadeFornecimentoItem;
            obj.QuantidadeMaterialServicoDecimal = quantidadeMaterialServicoDecimal;
            obj.QuantidadeMaterialServico = quantidadeMaterialServico;
            obj.QuantidadeLiquidarDecimal = quantidadeLiquidarDecimal;
            obj.QuantidadeLiquidar = quantidadeLiquidar;
            obj.StatusSiafisicoItem = objModel.StatusSiafisicoItem;
            obj.Transmitir = objModel.Transmitir ?? false;
            obj.Valor = objModel.Valor.ToString();

            return obj;
        }

    }
}