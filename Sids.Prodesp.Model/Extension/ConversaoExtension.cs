using Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Extension
{
    public static class ConversaoExtension
    {
        public static KeyValuePair<string, string> ConverterQuantidade(this decimal quantidadeMaterialServico)
        {

            int parteInteira = (int)Math.Truncate(quantidadeMaterialServico);
            int parteDecimal = (int)Math.Round((quantidadeMaterialServico - parteInteira) * 1000);

            return new KeyValuePair<string, string>(parteInteira.ToString(), parteDecimal.ToString("D3"));
        }
        public static long ParaInteiro(this decimal qtd)
        {
            return (long)(qtd * 1000);
        }
        public static decimal ParaDecimal(this decimal qtd)
        {
            return qtd / 1000;
        }
        public static long ParaInteiro(this double qtd)
        {
            return (long)(qtd * 1000);
        }
        public static double ParaDecimal(this double qtd)
        {
            return qtd / 1000;
        }
    }
}
