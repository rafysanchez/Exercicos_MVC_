using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.ValueObject.Service.Moq
{
    public class SiafemUsuario
    {
        public SiafemUsuario()
        {

        }


        [Column("id_usuario")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }


        [Column("ds_login")]
        [Display(Name = "Login")]
        public string ChaveAcesso { get; set; }


        [Column("ds_senha")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }


        [Column("bl_senha_expirada")]
        [Display(Name = "Senha Expirada?")]
        public bool SenhaExpirada { get; set; }
        
    }
}
