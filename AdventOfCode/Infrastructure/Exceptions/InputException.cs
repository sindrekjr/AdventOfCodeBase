using System;
using System.Runtime.Serialization;

namespace AdventOfCode.Infrastructure.Exceptions
{
    class InputException : Exception
    {
        public InputException(string message) : base(message) { }

        protected InputException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}