using System;
using System.Collections.Generic;
using System.Text;

namespace JewelsOnContainers.Services.OrderApi.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class OrderingDomainException : Exception
    {
        public OrderingDomainException()
        { }

        public OrderingDomainException(string message)
            : base(message)
        { }

        public OrderingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
