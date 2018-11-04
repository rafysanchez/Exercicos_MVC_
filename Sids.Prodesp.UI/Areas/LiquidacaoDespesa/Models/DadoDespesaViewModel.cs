namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;

    public class DadoDespesaViewModel
    {
        [Display(Name = "Nº do Processo")]
        public string NumeroProcesso { get; set; }
        [Display(Name = "Aut. proc. supra folhas")]
        public string DescricaoAutorizadoSupraFolha { get; set; }
        [Display(Name = "Cód. Esp. de Despesa")]
        public string CodigoEspecificacaoDespesa { get; set; }
        [Display(Name = "NL Retenção de INSS")]
        public string NlRetencaoInss { get; set; }
        [Display(Name = "Lista")]
        public string Lista { get; set; }

        public string DescricaoEspecificacaoDespesa1 { get; set; }
        public string DescricaoEspecificacaoDespesa2 { get; set; }
        public string DescricaoEspecificacaoDespesa3 { get; set; }
        public string DescricaoEspecificacaoDespesa4 { get; set; }
        public string DescricaoEspecificacaoDespesa5 { get; set; }
        public string DescricaoEspecificacaoDespesa6 { get; set; }
        public string DescricaoEspecificacaoDespesa7 { get; set; }
        public string DescricaoEspecificacaoDespesa8 { get; set; }
        public string DescricaoEspecificacaoDespesa9 { get; set; }



        public DadoDespesaViewModel CreateInstance(ILiquidacaoDespesa objModel)
        {
            return new DadoDespesaViewModel()
            {
                NumeroProcesso = objModel.NumeroProcesso,
                DescricaoAutorizadoSupraFolha = objModel.DescricaoAutorizadoSupraFolha,
                CodigoEspecificacaoDespesa = objModel.CodigoEspecificacaoDespesa,

                NlRetencaoInss = objModel.NlRetencaoInss,
                Lista = objModel.Lista,

                DescricaoEspecificacaoDespesa1 = objModel.DescricaoEspecificacaoDespesa1,
                DescricaoEspecificacaoDespesa2 = objModel.DescricaoEspecificacaoDespesa2,
                DescricaoEspecificacaoDespesa3 = objModel.DescricaoEspecificacaoDespesa3,
                DescricaoEspecificacaoDespesa4 = objModel.DescricaoEspecificacaoDespesa4,
                DescricaoEspecificacaoDespesa5 = objModel.DescricaoEspecificacaoDespesa5,
                DescricaoEspecificacaoDespesa6 = objModel.DescricaoEspecificacaoDespesa6,
                DescricaoEspecificacaoDespesa7 = objModel.DescricaoEspecificacaoDespesa7,
                DescricaoEspecificacaoDespesa8 = objModel.DescricaoEspecificacaoDespesa8,
                DescricaoEspecificacaoDespesa9 = objModel.DescricaoEspecificacaoDespesa9

            };
        }

        public DadoDespesaViewModel CreateInstance(Subempenho objModel)
        {
            return new DadoDespesaViewModel()
            {
                NumeroProcesso = objModel.NumeroProcesso,
                DescricaoAutorizadoSupraFolha = objModel.DescricaoAutorizadoSupraFolha,
                CodigoEspecificacaoDespesa = objModel.CodigoEspecificacaoDespesa,

                DescricaoEspecificacaoDespesa1 = objModel.DescricaoEspecificacaoDespesa1,
                DescricaoEspecificacaoDespesa2 = objModel.DescricaoEspecificacaoDespesa2,
                DescricaoEspecificacaoDespesa3 = objModel.DescricaoEspecificacaoDespesa3,
                DescricaoEspecificacaoDespesa4 = objModel.DescricaoEspecificacaoDespesa4,
                DescricaoEspecificacaoDespesa5 = objModel.DescricaoEspecificacaoDespesa5,
                DescricaoEspecificacaoDespesa6 = objModel.DescricaoEspecificacaoDespesa6,
                DescricaoEspecificacaoDespesa7 = objModel.DescricaoEspecificacaoDespesa7,
                DescricaoEspecificacaoDespesa8 = objModel.DescricaoEspecificacaoDespesa8,
                DescricaoEspecificacaoDespesa9 = objModel.DescricaoEspecificacaoDespesa9

            };
        }
    }
}