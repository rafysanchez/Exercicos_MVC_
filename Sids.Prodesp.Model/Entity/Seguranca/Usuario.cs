using Sids.Prodesp.Model.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Usuario: BaseModel
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão. Padrão.
        /// </summary>
        public Usuario() { }
        
        #endregion

        #region Métodos públicos

        #endregion

        #region Propriedades Públicas

        [Column("id_usuario")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }
        
        /// <summary>
        /// E-mail.
        /// </summary>
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Endereço de e-mail inválido.")]
        [Column("ds_email")]
        public string Email { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        [Column("ds_nome")]
        public string Nome { get; set; }

        /// <summary>
        /// CPF do usuário.
        /// </summary>
        [Column("nr_cpf")]
        public string CPF { get; set; }

        /// <summary>
        /// Senha de acesso ao Siafem (criptografada).
        /// </summary>
        [DataType(DataType.Password)]
        [Column("ds_senha_siafem")]
        public string SenhaSiafem { get; set; }

        /// <summary>
        /// Chave de Acesso ao Portal.
        /// </summary>
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Informe o login do usuário", AllowEmptyStrings = false)]
        [Column("ds_login")]
        public string ChaveDeAcesso { get; set; }

        /// <summary>
        /// Senha (criptografada).
        /// </summary>
        [Required(ErrorMessage = "Informe a senha do usuário", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Column("ds_senha")]
        public string Senha { get; set; }
        
        /// <summary>
        /// Indica se a senha está expirada ou não (Enum SimNao).
        /// </summary>
        [Display(Name = "Senha Expirou?")]
        [Column("bl_senha_expirada")]
        public bool SenhaExpirada { get; set; }

        
        /// <summary>
        /// Indica se a senha está expirada ou não (Enum SimNao).
        /// </summary>
        [Display(Name = "Senha Siafem Expirou?")]
        [Column("bl_senha_siafem_expirada")]
        public bool SenhaSiafemExpirada { get; set; }

        /// <summary>
        /// Data e hora de expiração da senha.
        /// </summary>
        [Display(Name = "Senha expira"), DataType(DataType.Date)]
        [Column("dt_expiracao_senha")]
        public DateTime DataExpiracaoSenha { get; set; }

        /// <summary>
        /// Data e hora do último acesso.
        /// </summary>
        [Display(Name = "Último Acesso"), DataType(DataType.Date)]
        [Column("dt_ultimo_acesso")]
        public DateTime DataUltimoAcesso { get; set; }

        /// <summary>
        /// Número de tentativas de login inválidas (para bloqueio).
        /// </summary>
        [Display(Name = "Tentativas de login inválidas")]
        [Column("nr_tentativa_login_invalidas")]
        public int TentativasLoginInvalidas { get; set; }

        /// <summary>
        /// Indica se o usuário está bloqueado ou não.
        /// </summary>
        [Column("bl_bloqueado")]
        public bool Bloqueado { get; set; }

        /// <summary>
        /// Indica se o usuário deve alterar a senha (senha expirada ou lembrete de senha).
        /// </summary>
        [Display(Name = "Alterar Senha?")]
        [Column("bl_alterar_senha")]
        public bool AlterarSenha { get; set; }

        /// <summary>
        /// Flag para validar usuários que irão acessar rotinas que utilizam integração com SIAFEM
        /// </summary>
        [Display(Name = "Acessa Siafem?")]
        [Column("bl_acesso_siafem")]
        public bool AcessaSiafem { get; set; }

        /// <summary>
        /// Token SHA512 único para o usuário, usado como parâmetro para reset de senha.
        /// </summary>
        [Column("ds_token")]
        public string Token { get; set; }

        [Column("id_sistema")]
        public short? SistemaId { get; set; }

        [Column("id_area")]
        public short? AreaId { get; set; }

        [Column("id_regional")]
        public short? RegionalId { get; set; }
        
        [Column("bl_alterar_senha_siafem")]
        public bool AlterarSenhaSiafem { get; set; }


        public TipoAutenticacao TipoAutenticacao { get; set; }

        [Column("cd_impressora132")]
        public string Impressora132 { get; set; }

        [Column("cd_impressora80")]
        public string Impressora80 { get; set; }

        #endregion
    }
}
