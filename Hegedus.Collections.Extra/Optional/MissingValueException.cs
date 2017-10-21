using System;
using System.Runtime.Serialization;

namespace Hegedus.Extra.Collections
{
    public class MissingValueException : InvalidOperationException
    {
        public MissingValueException() : base("Optional object must have a value") { }

        public MissingValueException(string message) : base(message)
        {
        }

        public MissingValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
