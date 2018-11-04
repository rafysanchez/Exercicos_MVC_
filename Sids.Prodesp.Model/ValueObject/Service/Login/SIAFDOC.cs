namespace Sids.Prodesp.Model.ValueObject.Service.Login
{
    public class SIAFDOC
    {
        public string cdMsg { get; set; }
        public SiafemLogin SiafemLogin { get; set; }
    }
    public class SiafemLogin
    {
        public login login { get; set; }
    }
    public class login
    {
        public string Codigo { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string ManterSenha { get; set; }
        public string Ano { get; set; }
        public string CICSS { get; set; }
    }
}
