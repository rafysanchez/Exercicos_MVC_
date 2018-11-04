using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Log
{
    /// <summary>
    /// Classe que representa uma Aplicação.
    /// </summary>
    public class LogAplicacao
    {
        #region Construtores

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public LogAplicacao() { }

        #endregion

        #region Propriedades Públicas
        
        [Column("id_log_aplicacao")]
        [Display(Name = "Código")]
        public int Id { get; set; }  // (Primary Key)

        [Column("ds_argumento")]
        [Display(Name = "Código")]
        public string Argumento { get; set; }

        [Column("ds_ip")]
        [Display(Name = "IP")]
        public string Ip { get; set; }

        [Column("ds_url")]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Column("dt_log")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Column("id_acao")]
        [Display(Name = "Ação")]
        public short? AcaoId { get; set; }

        [Column("id_navegador")]
        [Display(Name = "Navegador")]
        public short? NavegadorId { get; set; }

        [Column("id_recurso")]
        [Display(Name = "Funcionalidade")]
        public int? RecursoId { get; set; }

        [Column("id_resultado")]
        [Display(Name = "Resultado Ação")]
        public short? ResultadoId { get; set; }

        [Column("id_usuario")]
        [Display(Name = "Usuário")]
        public int? UsuarioId { get; set; }

        [Column("ds_versao")]
        [Display(Name = "Versão")]
        public string Versao { get; set; }

        [Column("ds_log")]
        [Display(Name = "Descrição do Log")]
        public string Descricao { get; set; }

        [Column("ds_navegador")]
        [Display(Name = "Descrição do Navegador")]
        public string Navegador { get; set; }

        [Column("ds_terminal")]
        [Display(Name = "Terminal")]
        public string Terminal { get; set; }

        [Column("ds_xml")]
        public string Xml { get; set; }

        #endregion
    }
}
