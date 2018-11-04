using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Base
{
    public class BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public BaseModel() { }

        /// <summary>
        /// Construtor personalizado.
        /// </summary>
        /// <param name="status"> Status do Objeto.</param>
        /// <param name="dataCriacao"> Data de criação do Registro.</param>
        /// <param name="usuarioCriacao"> Usuário que criou o registo.</param>
        public BaseModel(bool status, DateTime dataCriacao, int usuarioCriacao)
        {
            Status = status;
            DataCriacao = dataCriacao;
            UsuarioCriacao = usuarioCriacao;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Sobrecarga do método ToString() da classe Object.
        /// </summary>
        /// <returns> String com as propriedades do objeto.</returns>
        public override string ToString()
        {
            var propertyInfo = GetType().GetProperties();
            var sResult = $"Class={GetType().Name}|";

            foreach (var pInfo in propertyInfo)
            {
                sResult = string.Concat(sResult, $"{pInfo.Name}={pInfo.GetValue(this, null)}|");
            }

            return sResult;
        }

        #endregion

        #region Propriedades Públicas

        /// <summary>
        /// Ativo ou Inativo
        /// </summary>
        [Display(Name = "Status")]
        [Column("bl_ativo")]
        public bool? Status { get; set; }

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        [Display(Name = "Data Criação"), DataType(DataType.DateTime)]
        [Column("dt_criacao")]
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Usuário que criou o registro.
        /// </summary>
        [Display(Name = "Usuário Criação")]
        [Column("id_usuario_criacao")]
        public int UsuarioCriacao { get; set; }

        #endregion
    }
}
