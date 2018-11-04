using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.ValueObject
{
    public class ResultadoOperacaoVo
    {
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }

        public ResultadoOperacaoVo()
        {

        }
        public ResultadoOperacaoVo(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
        }
    }

    public class ResultadoOperacaoObjetoVo<T> : ResultadoOperacaoVo
    {
        public T Objeto { get; set; }
        public ResultadoOperacaoObjetoVo() : base()
        {

        }

        public ResultadoOperacaoObjetoVo(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
        }

        public ResultadoOperacaoObjetoVo(bool sucesso, string mensagem, T objeto)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Objeto = objeto;
        }
    }
}
