using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class PerfilAcao : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public PerfilAcao() { }

        /// <summary>
        /// Construtor padrão. Personalizado
        /// </summary>
        /// <param name="_codigo">Código do Perfil Recurso</param>
        /// <param name="_perfil">Código do Perfil vinculado a Acão</param>
        /// <param name="_acao">Código do Acão da funcionalidade vinculada ao perfil</param>
        public PerfilAcao(
            int _codigo,
            int _perfil,
            int _funcionalidade,
            int _acao)
        {
            Codigo = _codigo;
            Perfil = _perfil;
            Funcionalidade = _funcionalidade;
            Acao = _acao;
        }
        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código do objeto
        /// </summary>
        [Column("id_perfil_acao")]
        public int Codigo { get; set; }
        
        /// <summary>
        /// Perfil Model
        /// </summary>
        [Column("id_perfil")]
        public int Perfil { get; set; }

        /// <summary>
        /// LogAcao Model
        /// </summary>
        [Column("id_recurso_acao")]
        public int Funcionalidade { get; set; }

        /// <summary>
        /// LogAcao Model
        /// </summary>
        [Column("id_acao")]
        public int Acao { get; set; }
        #endregion
    }
}
