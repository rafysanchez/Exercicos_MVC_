namespace Sids.Prodesp.Infrastructure.Services.Moq
{
    using DataBase.Seguranca.Moq;
    using Model.ValueObject.Service.Moq;
    using System.Linq;
    using System.Xml;

    public class RecebeMSG
    {
        private readonly SiafemUsuarioDal _repository;


        public RecebeMSG()
        {
            _repository = new SiafemUsuarioDal();
        }


        public string Message(string login, string password, string year, string UnidadeGestora, string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            switch (document.GetElementsByTagName("cdMsg")[0].FirstChild.Value)
            {
                case "SIAFLOGIN001":
                    return Login(login, password);
                case "SIAFTrocaSenha":
                    return AlterarSenha(login, password, 
                        document.GetElementsByTagName("NovaSenha")[0].FirstChild.Value);
                default:
                    return null;
            }
        }

        public string Login(string login, string password)
        {
            var erro = default(string);
            var usuario = _repository.Fetch(new SiafemUsuario { ChaveAcesso = login });
            
            if (usuario.Count() == 0)
            {
                erro = "<MSG><BCMSG></BCMSG><SISERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>- ACESSO NAO PERMITIDO</MsgErro></login></SiafemLogin></SIAFDOC>";
                erro = erro + "<SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>" + AppConfig.WsPassword + "</Senha><Ano>2016</Ano></login></SiafemLogin></SIAFDOC></SISERRO></MSG>";
                return erro;
            }

            if (usuario.Count(x => x.ChaveAcesso == "87863878128") > 0)
            {
                erro = "<MSG><BCMSG></BCMSG><SISERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>ERRO NO SIAFEM</MsgErro></login></SiafemLogin></SIAFDOC>";
                erro = erro + "<SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>13Outubro</Senha><Ano>2016</Ano></login></SiafemLogin></SIAFDOC></SISERRO></MSG>";

                return erro;
            }

            if (usuario.Count(x => x.Senha != password) > 0)
            {
                erro = "<MSG><BCMSG></BCMSG><SISERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>SENHA INCORRETA DIGITE NOVAMENTE</MsgErro></login></SiafemLogin></SIAFDOC>";
                erro = erro + "<SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>13Outubro</Senha><Ano>2016</Ano></login></SiafemLogin></SIAFDOC></SISERRO></MSG>";

                return erro;
            }


            if (usuario.Count(x => x.SenhaExpirada == true) > 0)
            {
                erro = "<MSG><BCMSG></BCMSG><SISERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>- POR FAVOR, TROQUE SUA SENHA</MsgErro></login></SiafemLogin></SIAFDOC>";
                erro = erro + "<SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>13Outubro</Senha><Ano>2016</Ano></login></SiafemLogin></SIAFDOC></SISERRO></MSG>";

                return erro;
            }

            return "<MSG><BCMSG><Doc_Estimulo><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>" + AppConfig.WsPassword + "</Senha><Ano>2016</Ano><CICSS>false</CICSS></login></SiafemLogin></SIAFDOC></Doc_Estimulo></BCMSG><SISERRO><Doc_Retorno><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>true</StatusOperacao><MsgErro></MsgErro></login></SiafemLogin></SIAFDOC></Doc_Retorno></SISERRO></MSG>";
        }

        public string AlterarSenha(string login, string password, string newPassword)
        {
            var erro = default(string);
            var usuarios = _repository.Fetch(new SiafemUsuario { ChaveAcesso = login });

            if (usuarios.Count() == 0)
            {
                return "<ERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>- ACESSO NAO PERMITIDO</MsgErro></login></SiafemLogin></SIAFDOC></ERRO>";
            }


            if (usuarios.Count(x => x.ChaveAcesso == "93857303638") > 0)
            {
                erro = "<MSG><BCMSG></BCMSG><SISERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>ERRO NO SIAFEM</MsgErro></login></SiafemLogin></SIAFDOC>";
                erro = erro + "<SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>13Outubro</Senha><Ano>2016</Ano></login></SiafemLogin></SIAFDOC></SISERRO></MSG>";

                return erro;
            }

            var usuario = usuarios.FirstOrDefault(x => x.ChaveAcesso == login) ?? new SiafemUsuario();
            if (usuario.Senha != password)
            {
                return "<ERRO><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>false</StatusOperacao><MsgErro>SENHA INCORRETA DIGITE NOVAMENTE</MsgErro></login></SiafemLogin></SIAFDOC></ERRO>";
            }

            usuario.Senha = newPassword;
            usuario.SenhaExpirada = false;

            _repository.Edit(usuario);

            return "<MSG><BCMSG><Doc_Estimulo><SIAFDOC><cdMsg>SIAFTrocaSenha</cdMsg><SiafemLogin><login><Codigo>" + AppConfig.WsSiafemUser + "</Codigo><Senha>" + AppConfig.WsPassword + "</Senha><NovaSenha>12NOVEMBRO</NovaSenha><ManterSernha>s</ManterSernha><Ano>2016</Ano><CICSS>false</CICSS></login></SiafemLogin></SIAFDOC></Doc_Estimulo></BCMSG><SISERRO><Doc_Retorno><SIAFDOC><cdMsg>SIAFLOGIN001</cdMsg><SiafemLogin><login><StatusOperacao>true</StatusOperacao><MsgErro></MsgErro></login></SiafemLogin></SIAFDOC></Doc_Retorno></SISERRO></MSG>";
        }
    }
}
