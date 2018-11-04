using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Regional
    {

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public Regional() { }

        public Regional(
            int _id,
            string _descricao)
        {
            Id = _id;
            Descricao = _descricao;
        }

        [Column("id_regional")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_regional")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Column("cd_uge")]
        [Display(Name = "Unidade Gestora")]
        public string Uge { get; set; }

        public string Orgao { get; set; }
    }
}
