using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class PerfilUsuario : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construto padrão. Padrão.
        /// </summary>
        public PerfilUsuario() { }

        /// <summary>
        /// Construtor padrão. Personalizado.
        /// </summary>
        /// <param name="_perfil">Código do perfil vinculado ao usuário</param>
        /// <param name="_usuario">Código do usuário vinculado ao perfil</param>
        public PerfilUsuario(
            int _perfil,
            int _usuario
        )
        {
            Perfil = _perfil;
            Usuario = _usuario;
        }

        #endregion

        #region Propriedades Públicas

        [Column("id_perfil_usuario")]
        public int Codigo { get; set; }

        /// <summary>
        /// Perfil Model
        /// </summary>
        [Column("id_perfil")]
        public int Perfil { get; set; }

        [Column("ds_perfil")]
        public string DescPerfil { get; set; }

        /// <summary>
        /// Usuário Model
        /// </summary>
        [Column("id_usuario")]
        public int Usuario { get; set; }

        [Column("associado")]
        public bool Associado { get; set; }

        #endregion
    }
}
