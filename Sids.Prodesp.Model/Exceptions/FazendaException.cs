using Sids.Prodesp.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Exceptions
{
    [Serializable]
    public class FazendaException : Exception
    {
        public EnumTipoServicoFazenda Servico { get; set; }

        #region Construtores
        public FazendaException() : base()
        {
        }

        public FazendaException(EnumTipoServicoFazenda servico, string message) : base(servico + " - " + message)
        {
            Servico = servico;
        }
        #endregion


        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Servico", Servico);
        }
    }

    [Serializable]
    public class SiafisicoException : FazendaException
    {
        public SiafisicoException() : base()
        {
            Servico = EnumTipoServicoFazenda.Siafisico;
        }

        public SiafisicoException(string mensagem) : base(EnumTipoServicoFazenda.Siafisico, mensagem)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    [Serializable]
    public class SiafemException : FazendaException
    {
        public SiafemException() : base()
        {
            Servico = EnumTipoServicoFazenda.Siafem;
        }

        public SiafemException(string mensagem) : base(EnumTipoServicoFazenda.Siafem, mensagem)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
