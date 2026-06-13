using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message, "Bad Request", 400)
        {
        }
    }
}
