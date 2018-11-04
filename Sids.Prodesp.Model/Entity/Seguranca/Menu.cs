using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Menu : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public Menu() { }

        /// <summary>
        /// Construtor padrão. Personalizado.
        /// </summary>
        /// <param name="_codigo"> Código numérico do Item.</param>
        /// <param name="_recurso"> Recurso a qual o menu é vinculado.</param>
        /// <param name="_descricao"> Descrição.</param>
        /// <param name="_ordem"> Ordem em que deve aparecer (começando em 0).</param>
        public Menu(
            int _codigo,
            int? _recurso,
            string _descricao,
            int _ordem)
        {
            Codigo = _codigo;
            Recurso = _recurso;
            Descricao = _descricao;
            Ordem = _ordem;
        }

        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código numérico do Item.
        /// </summary>
        [Column("id_menu")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }
        
        /// <summary>
        /// Aplicação a qual o menu é vinculado (somente se TipoMenu = Aplicação).
        /// </summary>
        [Display(Name = "Recurso")]
        [Column("id_recurso")]
        public int? Recurso { get; set; }

        /// <summary>
        /// Descrição.
        /// </summary>
        [Display(Name = "Descrição"), Required]
        [Column("ds_menu")]
        public string Descricao { get; set; }

        /// <summary>
        /// Ordem em que deve aparecer (começando em 0).
        /// </summary>
        [Column("nr_ordem")]
        public int Ordem { get; set; }
        

        #endregion
    }
}
