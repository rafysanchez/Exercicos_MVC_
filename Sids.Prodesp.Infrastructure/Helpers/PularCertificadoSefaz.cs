namespace Sids.Prodesp.Infrastructure.Helpers
{
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    internal static class PularCertificadoSefaz
    {
        static string CertificadosBloquear;

        public static void SetCertificatePolicy(string listaCertificadosBloquear)
        {
            CertificadosBloquear = listaCertificadosBloquear ?? string.Empty;
            var certificado = ServicePointManager.ServerCertificateValidationCallback;
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            var pulaCertificado = false;

            foreach (var item in CertificadosBloquear.Split(';'))
                if (cert.Subject.Contains(item)) pulaCertificado = true;

            return pulaCertificado;
        }

    }
}
