using System.ComponentModel.DataAnnotations;

namespace Sids.Prodesp.Model.Enum
{
    public enum SimNao
    {
        /// <summary>
        /// Não.
        /// </summary>
        [Display(Name ="Não")]
        Nao = 0,

        /// <summary>
        /// Sim.
        /// </summary>
        [Display(Name = "Sim")]
        Sim = 1
    }
}
