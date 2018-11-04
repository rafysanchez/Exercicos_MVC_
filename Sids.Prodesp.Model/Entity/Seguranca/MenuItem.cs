using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class MenuItem : BaseModel
    {

        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public MenuItem() { }

        /// <summary>
        /// Construtor padrão. Personalizado.
        /// </summary>
        /// <param name="_codigo"> Código do Menu Item.</param>
        /// <param name="_menu"> Código do Menu Pai.</param>
        /// <param name="_recurso"> Código do Recurso para o qual o item aponta.</param>
        /// <param name="_label"> Texto que aparece no menu.</param>
        /// <param name="_target"> URL Target (_blank, _self, _top, etc).</param>
        /// <param name="_ordem"> Ordem do item no menu (0, 1, 2, etc).</param>
        public MenuItem(
            int _codigo,
            int _menu,
            int _recurso,
            string _label,
            string _target,
            int _ordem)
        {
            Codigo = _codigo;
            Menu = _menu;
            Recurso = _recurso;
            Rotulo = _label;
            AbrirEm = _target;
            Ordem = _ordem;
        }

        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código do Menu Item.
        /// </summary>
        [Column("id_menu_item")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        /// <summary>
        /// Código do Menu Pai.
        /// </summary>
        [Required]
        [Column("id_menu")]
        public int Menu { get; set; }

        /// <summary>
        /// Código do Recurso para o qual o item aponta.
        /// </summary>
        [Required]
        [Column("id_recurso")]
        public int Recurso { get; set; }

        /// <summary>
        /// Texto que aparece no menu.
        /// </summary>
        [Display(Name = "Rótulo"), Required]
        [Column("ds_rotulo")]
        public string Rotulo { get; set; }

        /// <summary>
        /// URL Target (_blank, _self, _top, etc).
        /// </summary>
        [Display(Name = "Abrir em")]
        [Column("ds_abrir_em")]
        public string AbrirEm { get; set; }

        /// <summary>
        /// Ordem do item no menu (0, 1, 2, etc).
        /// </summary>
        [Column("ordem_item")]
        public int? Ordem { get; set; }

        [Column("ds_menu")]
        public string DescMenu { get; set; }

        /// <summary>
        /// Ordem do item no menu (0, 1, 2, etc).
        /// </summary>
        [Column("ds_url_recurso_menu")]
        public string UrlRecursoMenu { get; set; }

        [Column("ds_url")]
        public string UrlRecurso { get; set; }

        /// <summary>
        /// Código do Item de menu Pai.
        /// </summary>
        [Display(Name = "Item Pai")]
        [Column("id_menu_item_pai")]
        public int? MenuItemPai { get; set; }

        [Column("ds_area")]
        public string Area { get; set; }

        [Column("ds_controller")]
        public string Controller { get; set; }

        [Column("ds_action")]
        public string Action { get; set; }

        #endregion
    }
}
