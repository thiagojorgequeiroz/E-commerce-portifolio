namespace Catalog.Application.Exceptions
{
    public class UnexpectedException : AppException
    {
        public UnexpectedException() : base("An unexpected error occurred.", "Unexpected Error", 500)
        {
        }
    }
}
