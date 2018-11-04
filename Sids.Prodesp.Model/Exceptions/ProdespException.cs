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
    public class ProdespException : Exception
    {
        #region Construtores
        public ProdespException() : base()
        {
        }

        public ProdespException(string message) : base("Prodesp - " + message)
        {
        }
        #endregion


        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
