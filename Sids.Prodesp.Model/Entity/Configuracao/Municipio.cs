using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Configuracao
{
    public class Municipio
    {
        [Display(Name = "Código")]
        [Column("cd_municipio")]
        public string Codigo { get; set; }

        [Display(Name = "Descrição")]
        [Column("ds_municipio")]
        public string Descricao { get; set; }
    }
}
