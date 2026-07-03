namespace Catalog.Domain.Exceptions
{
    public class DatabaseException : AppException
    {
        public DatabaseException(string message) : base(message, "Error in database access", 400)
        {
        }
    }
}
