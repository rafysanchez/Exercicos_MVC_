using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Exceptions
{
    [Serializable]
    public class SidsException : Exception
    {
        public SidsException()
        {
        }

        public SidsException(string message)
            : base(message)
        {
        }

        public SidsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected SidsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
