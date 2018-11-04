namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Interface;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PesquisaEmpenhoReforcoViewModel
    {
        [DisplayName("Nº Empenho Prodesp")]
        [StringLength(9)]
        public string CodigoEmpenho { get; private set; }


        public PesquisaEmpenhoReforcoViewModel CreateInstance(string codigoReforco)
        {
            return new PesquisaEmpenhoReforcoViewModel {
                CodigoEmpenho = codigoReforco
            };
           

           
        }
    }
}