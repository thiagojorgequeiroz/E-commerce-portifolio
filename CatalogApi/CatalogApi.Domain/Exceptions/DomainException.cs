namespace Catalog.Domain.Exceptions
{
    public class DomainException : AppException
    {
        public DomainException(string message) : base(message, "Domain Exception", 400)
        {
        }
    }
}
