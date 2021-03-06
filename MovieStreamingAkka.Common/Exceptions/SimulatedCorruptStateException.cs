using System;
using System.Runtime.Serialization;

namespace MovieStreamingAkka.Common.Exceptions
{
    [Serializable]
    public class SimulatedCorruptStateException : Exception
    {
        public SimulatedCorruptStateException()
        {
        }

        public SimulatedCorruptStateException(string message) : base(message)
        {
        }

        public SimulatedCorruptStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimulatedCorruptStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
