using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Configuracao
{
    public class Programa
    {
        /// <summary>
        /// Codigo do Programa
        /// </summary>
        [Display(Name = "Códico")]
        [Column("id_programa")] 
        public int Codigo { get; set; }
        
        /// <summary>
        /// Nome do Programa
        /// </summary>
        [Display(Name = "Descrição")]
        [Column("ds_programa")]
        public string Descricao { get; set; }

        /// <summary>
        /// Nome do Programa
        /// </summary>
        [Display(Name = "PTRES")]
        [Column("cd_ptres")]
        public string Ptres { get; set; }

        /// <summary>
        /// Nome do Programa
        /// </summary>
        [Display(Name = "CFP")]
        [Column("cd_cfp")]
        public string Cfp { get; set; }

        /// <summary>
        /// Nome do Programa
        /// </summary>
        [Display(Name = "Ano")]
        [Column("nr_ano_referencia")]
        public int Ano { get; set; }


    }
}
