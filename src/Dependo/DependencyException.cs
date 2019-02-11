namespace Dependo
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DependencyException : Exception
    {
        public DependencyException()
        {
        }

        protected DependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DependencyException(string message) : base(message)
        {
        }

        public DependencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
