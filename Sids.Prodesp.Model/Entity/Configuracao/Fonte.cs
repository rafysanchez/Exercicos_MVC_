using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Configuracao
{
    //public class ReservaFonte
    public class Fonte 
    {       
        /// <summary>
        /// id da fonte.
        /// </summary>
        [Column("id_fonte")]
        public int Id { get; set; }
        
        /// <summary>
        /// Código da fonte.
        /// </summary>
        [Display(Name ="Código")]
       // [Required]
        [Column("cd_fonte")]
        public string Codigo { get; set;}

        /// <summary>
        /// Número da fonte.
        /// </summary>
        [Display(Name ="Descrição")]
       // [Required]
        [Column("ds_fonte")]
        public string Descricao { get; set;}
    
      
    }
}
