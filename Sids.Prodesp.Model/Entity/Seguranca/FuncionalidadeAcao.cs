using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class FuncionalidadeAcao : BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public FuncionalidadeAcao() { }

        /// <summary>
        /// Construtor padrão. Personalizado
        /// </summary>
        /// <param name="_codigo">Código da Funcionalidade Acão</param>
        /// <param name="_funcionalidade">Código da Funcionalidade vinculado a Acão</param>
        /// <param name="_acao">Código do Acão vinculado a Funcionalidade</param>
        public FuncionalidadeAcao(
            int _codigo,
            int _funcionalidade,
            int _acao)
        {
            Codigo = _codigo;
            Funcionalidade = _funcionalidade;
            Acao = _acao;
        }
        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Código do objeto
        /// </summary>
        [Column("id_recurso_acao")]
        public int Codigo { get; set; }
        /// <summary>
        /// Perfil Model
        /// </summary>
        [Column("id_recurso")]
        public int Funcionalidade { get; set; }

        /// <summary>
        /// LogAcao Model
        /// </summary>
        [Column("id_acao")]
        public int Acao { get; set; }
        
        #endregion
    }
}
