using Sids.Prodesp.Model.Interface;
using Sids.Prodesp.Model.Interface.Empenho;

namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Enum;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PesquisaCTViewModel
    {
        [Display(Name = "Nº do CT")]
        public string NumeroCT { get; set; }

        /*[Display(Name = "Nº do CT Original")]*/
        public string NumeroOriginalCT { get; set; }

        public EnumTipoOperacaoEmpenho TipoEmpenho { get; set; }

        public string TituloComponente { get; set; }
        public bool OcultarTituloComponente { get; set; }
        public string TituloCampo { get; set; }
        public bool OcultarBotaoConsultar { get; set; }


        [Obsolete("Utilize a assinatura que recebe o tipo")]
        public PesquisaCTViewModel CreateInstance(IEmpenho objModel)
        {
            return new PesquisaCTViewModel()
            {
                NumeroCT = objModel.NumeroCT,
                NumeroOriginalCT = objModel.NumeroOriginalCT,
                TituloCampo = "Nº do CT Original"
            };
        }

        public PesquisaCTViewModel CreateInstance(IEmpenho objModel, EnumTipoOperacaoEmpenho tipo)
        {
            var obj = new PesquisaCTViewModel()
            {
                NumeroCT = objModel.NumeroCT,
                NumeroOriginalCT = objModel.NumeroOriginalCT,
                TipoEmpenho = tipo,
                TituloCampo = "Nº do CT Original"
            };

            switch (tipo)
            {
                case EnumTipoOperacaoEmpenho.Reforco:
                    obj.TituloComponente = "Pesquisar Empenho por CT";
                    break;
                case EnumTipoOperacaoEmpenho.Cancelamento:
                    obj.TituloComponente = "Pesquisar Empenho por CT";
                    break;
                case EnumTipoOperacaoEmpenho.Subempenho:
                    break;
                case EnumTipoOperacaoEmpenho.Empenho:
                default:
                    obj.TituloComponente = "Pesquisar CT para Contabilizar Empenho";
                    break;
            }

            return obj;
        }

        public PesquisaCTViewModel CreateInstance(ILiquidacaoDespesa objModel, EnumTipoOperacaoEmpenho tipo)
        {
            return new PesquisaCTViewModel()
            {
                NumeroCT = objModel.NumeroCT,
                TipoEmpenho = tipo,
                TituloCampo = "Nº do CT",
                OcultarTituloComponente = true,
                OcultarBotaoConsultar = true
            };
        }
    }
}