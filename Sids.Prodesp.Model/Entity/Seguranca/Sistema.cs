using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Sistema
    {
        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public Sistema() { }

        public Sistema(
            int _id,
            string _descricao)
        {
            Id = _id;
            Descricao = _descricao;
        }

        [Column("id_sistema")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_sistema")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
