using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Infrastructure
{
    public static class AppConfig
    {
        #region SIDS
        private static CultureInfo currentCulture;
        public static CultureInfo CurrentCulture
        {
            get
            {
                if (currentCulture == null)
                {
                    currentCulture = new CultureInfo("pt-br");
                }

                return currentCulture;
            }
        }
        public static string Hash
        {
            get { return ConfigurationManager.AppSettings["hash"]; }
        }

        #region Seguranca
        public static string DiasExpiracaoSenha
        {
            get { return ConfigurationManager.AppSettings["DiasExpiracaoSenha"]; }
        }
        public static string Domain
        {
            get { return ConfigurationManager.AppSettings["Domain"]; }
        }
        #endregion 
        #endregion

        #region Web Service SeFaz
        public static string AnoExercicio
        {
            get { return ConfigurationManager.AppSettings["AnoExercicio"]; }
        }

        public static string WsUrl
        {
            get { return ConfigurationManager.AppSettings["WSURL"]; }
        }

        public static string Acesso
        {
            get { return ConfigurationManager.AppSettings["Acesso"]; }
        }

        #region Seguranca
        public static string WsSiafisicoUser
        {
            get { return "PSIAFISIC18"; }
        }

        public static string WsSiafemUser
        {
            get { return "PSIAFEM2018"; }
        }

        public static string WsPassword
        {
            get { return "13NOVEMBRO"; }
        }
        #endregion

        #region Urls

        #region Reserva
        public static string WsReservaUrlDes
        {
            get { return ConfigurationManager.AppSettings["UrlReservaDes"]; }
        }
        public static string WsReservaUrlHom
        {
            get { return ConfigurationManager.AppSettings["UrlReservaHom"]; }
        }
        public static string WsReservaUrlProd
        {
            get { return ConfigurationManager.AppSettings["UrlReservaprod"]; }
        }
        #endregion

        #region Empenho
        public static string WsEmpenhoUrlDes
        {
            get { return ConfigurationManager.AppSettings["UrlEmpenhoDes"]; }
        }
        public static string WsEmpenhoUrlHom
        {
            get { return ConfigurationManager.AppSettings["UrlEmpenhoHom"]; }
        }
        public static string WsEmpenhoUrlProd
        {
            get { return ConfigurationManager.AppSettings["UrlEmpenhoprod"]; }
        }
        #endregion

        #region SubEmpenho
        public static string WsSubEmpenhoUrlDes
        {
            get { return ConfigurationManager.AppSettings["UrlSubEmpenhoDes"]; }
        }
        public static string WsSubEmpenhoUrlHom
        {
            get { return ConfigurationManager.AppSettings["UrlSubEmpenhoHom"]; }
        }
        public static string WsSubEmpenhoUrlProd
        {
            get { return ConfigurationManager.AppSettings["UrlSubEmpenhoprod"]; }
        }
        #endregion

        #region PgtoContaUnica
        public static string WsPgtoContaUnicaUrlDes
        {
            get { return ConfigurationManager.AppSettings["UrlPgtoContaUnicaDes"]; }
        }
        public static string WsPgtoContaUnicaUrlHom
        {
            get { return ConfigurationManager.AppSettings["UrlPgtoContaUnicaHom"]; }
        }
        public static string WsPgtoContaUnicaUrlProd
        {
            get { return ConfigurationManager.AppSettings["UrlPgtoContaUnicaprod"]; }
        }
        #endregion

        #region PgtoContaDer
        public static string WsPgtoContaDerUrlDes
        {
            get { return ConfigurationManager.AppSettings["UrlPgtoContaDerDes"]; }
        }
        public static string WsPgtoContaDerUrlHom
        {
            get { return ConfigurationManager.AppSettings["UrlPgtoContaDerHom"]; }
        }
        public static string WsPgtoContaDerUrlProd
        {
            get { return ConfigurationManager.AppSettings["UrlPgtoContaDerprod"]; }
        }
        #endregion

        #region Movimentacao
        public static string UrlMovimentacaoDes
        {
            get { return ConfigurationManager.AppSettings["UrlMovimentacaoDes"]; }
        }
        public static string UrlMovimentacaoHom
        {
            get { return ConfigurationManager.AppSettings["UrlMovimentacaoHom"]; }
        }
        public static string UrlMovimentacaoProd
        {
            get { return ConfigurationManager.AppSettings["UrlMovimentacaoProd"]; }
        }

        #endregion

        #endregion

        public static bool IsProd
        {
            get
            {
                return WsUrl == "siafemProd";
            }
        }
        #endregion
    }
}
