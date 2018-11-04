using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Configuracao
{
    
    public class Estrutura: BaseModel
    {

        /// <summary>
        /// Código da Estrutura.
        /// </summary>
      
        [Display(Name = "Estrutura")]
        [Column("id_estrutura")]
        public int Codigo { get; set; }

        [Display(Name = "Programa")]
        [Column("id_programa")]
        public int? Programa { get; set; }

        [Display(Name = "Nomenclatura")]
        [Column("ds_nomenclatura")]
        public string Nomenclatura { get; set; }

        [Display(Name = "Natureza")]
        [Column("cd_natureza")]
        public string Natureza { get; set; }

        [Display(Name = "Macro")]
        [Column("cd_macro")]
        public string Macro { get; set; }

        [Display(Name = "Aplicacao")]
        [Column("cd_codigo_aplicacao")]
        public string Aplicacao { get; set; }
        
        [Display(Name = "Fonte")]
        [Column("id_fonte")]
        public string Fonte { get; set; }

    }
}

