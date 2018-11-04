using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class PerfilFuncionalidade : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public PerfilFuncionalidade() { }

        /// <summary>
        /// Construtor padrão. Personalizado
        /// </summary>
        /// <param name="_perfil">Código do Perfil Recurso</param>
        /// <param name="_perfil">Código do Perfil vinculado ao recurso</param>
        /// <param name="_recurso">Código da Recurso Funcionalidade ao perfil</param>
        public PerfilFuncionalidade(
            int _codigo,
            int _perfil,
            int _recurso)
        {
            Codigo = _codigo;
            Perfil = _perfil;
            Funcionalidade = _recurso;
        }
        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código do objeto
        /// </summary>
        [Column("id_perfil_recurso")]
        public int Codigo { get; set; }
        /// <summary>
        /// Perfil Model
        /// </summary>
        [Column("id_perfil")]
        public int Perfil { get; set; }

        /// <summary>
        /// Recurso Model
        /// </summary>
        [Column("id_recurso")]
        public int Funcionalidade { get; set; }
        
        #endregion
    }
}
