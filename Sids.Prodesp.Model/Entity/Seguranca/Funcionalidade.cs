using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Funcionalidade : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public Funcionalidade() { }

        /// <summary>
        /// Construtor padrão. Personalizado.
        /// </summary>
        /// <param name="_codigo"> Código do Recurso.</param>
        /// <param name="_nome"> Nome do Recurso - usado no título das páginas.</param>
        /// <param name="_descricao"> Descrição - usado no description das páginas.</param>
        /// <param name="_keywords"> Palavras-chave - usadas no keywords das páginas.</param>
        /// <param name="_url"> Caminho do recurso.</param>
        /// <param name="_publico"> Indica se o recurso é público (1) ou não (0).</param>
        public Funcionalidade(
            int _codigo,
            string _nome,
            string _descricao,
            string _keywords,
            string _url,
            bool _publico)
        {
            Codigo = _codigo;
            Nome = _nome;
            Descricao = _descricao;
            Keywords = _keywords;
            URL = _url;
            Publico = _publico;
        }

        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código do Recurso.
        /// </summary>
        [Column("id_recurso")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        /// <summary>
        /// Nome do Recurso - usado no título das páginas.
        /// </summary>
        [Required]
        [Column("no_recurso")]
        public string Nome { get; set; }

        /// <summary>
        /// Descrição - usado no description das páginas.
        /// </summary>
        [Display(Name = "Descrição")]
        [Column("ds_recurso")]
        public string Descricao { get; set; }

        /// <summary>
        /// Palavras-chave - usadas no keywords das páginas.
        /// </summary>
        [Column("ds_keywords")]
        public string Keywords { get; set; }

        /// <summary>
        /// Caminho do recurso.
        /// </summary>
        [Required]
        [Column("ds_url")]
        public string URL { get; set; }
        

        /// <summary>
        /// Indica se o recurso é público (1) ou não (0).
        /// </summary>
        [Column("bl_publico")]
        public bool? Publico { get; set; }

        [Column("id_menu_url")]
        public int MenuUrlId { get; set; }

        public int MenuId { get; set; }

        public List<Acao> Acoes { get; set; }


        #endregion
    }
}
