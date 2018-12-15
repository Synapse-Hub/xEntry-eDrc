using System;
using System.Runtime.Serialization;

namespace Xentry.Desktop
{
    [Serializable]
    public class XentryDesktopCustomException : Exception
    {
        public XentryDesktopCustomException()
        {
        }

        public XentryDesktopCustomException(string message) : base(message)
        {
        }

        public XentryDesktopCustomException(string message, Exception innerException) : base(message, innerException)
        {
        }       

        protected XentryDesktopCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
