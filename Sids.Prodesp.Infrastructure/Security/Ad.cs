namespace Sids.Prodesp.Infrastructure.Security
{
    using Model.Entity.Seguranca;
    using Model.Interface.Security;
    using System.Configuration;
    using System.DirectoryServices;

    public class Ad : IAutenticacao
    {
        public bool Authenticate(Usuario entity)
        {
            var usuarioAD = FindADUser(entity.ChaveDeAcesso, entity.Senha);
            if (usuarioAD != null)
            {
                GetAdProperties(usuarioAD, entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Procura pelo usuário informado no Active Directory
        /// </summary>
        /// <returns> SearchResult.</returns>
        private SearchResult FindADUser(string user, string password)
        {
            try
            {
                var AdsiSearch = new DirectorySearcher(GetLdapConnection(user, password));
                AdsiSearch.Filter = $"(sAMAccountName={user})";

                return AdsiSearch.FindOne();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna uma instância de DirectoryEntry
        /// </summary>
        /// <returns> DirectoryEntry.</returns>
        private DirectoryEntry GetLdapConnection(string strADUser, string strADPass)
        {
            string strEndActiveDirectory = string.Format("LDAP:// {0}", AppConfig.Domain);

            DirectoryEntry de = new DirectoryEntry(strEndActiveDirectory, strADUser, strADPass, AuthenticationTypes.Secure);

            return de;
        }

        /// <summary>
        /// Retorna as propriedades de um usuário cadastrado no AD.
        /// </summary>
        /// <param name="adUser">Usuário do Active Directory.</param>
        /// <param name="user">Usuário do Portal com alguns dados preenchidos.</param>
        private static void GetAdProperties(SearchResult adUser, Usuario user)
        {
            var collection = adUser.Properties;
            foreach (string Key in collection.PropertyNames)
            {
                foreach (var obj in collection[Key])
                {
                    switch (Key.ToLower())
                    {
                        case "samaccountname": user.ChaveDeAcesso = obj.ToString(); break;
                        case "displayname": user.Nome = obj.ToString(); break;
                        case "mail": user.Email = obj.ToString(); break;
                        case "pager": user.CPF = obj.ToString(); break;
                        default: break;
                    }
                }
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                user.Email = collection["userprincipalname"][0].ToString();
            }
        }
    }
}
