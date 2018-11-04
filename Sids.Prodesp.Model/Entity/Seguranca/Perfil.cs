using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Perfil : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor Padrão. Padrão.
        /// </summary>
        public Perfil() { }

        /// <summary>
        /// Construtor padrão. Personalizado
        /// </summary>
        /// <param name="_codigo">Código do Perfil</param>
        /// <param name="_empresa">Código da empresa vinculada ao perfil</param>
        /// <param name="_descricao">Descrição do Perfil</param>
        public Perfil(
            int _codigo,
            string _detalhe,
            string _descricao)
        {
            Codigo = _codigo;
            Detalhe = _detalhe;
            Descricao = _descricao;
        }


        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código do Perfil (PK)
        /// </summary>
        [Display(Name = "Código")]
        [Column("id_perfil")]
        public int Codigo { get; set; }
        
        [Required(ErrorMessage = "O campo Descrição Perfil é obrigatório")]
        [Display(Name = "Nome do Perfil")]
        [Column("ds_perfil")]
        public string Descricao { get; set; }
        
        [Display(Name = "Detalhes")]
        [Column("ds_detalhe")]
        public string Detalhe { get; set; }

        /// <summary>
        /// Empresa Model
        /// </summary>
        [Display(Name = "Administrador")]
        [Column("bl_administrador")]
        public bool Administrador { get; set; }

        #endregion
    }
}






















