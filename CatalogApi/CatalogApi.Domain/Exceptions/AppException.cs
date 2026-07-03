namespace Catalog.Domain.Exceptions
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; }
        public string Title { get; }

        protected AppException(string message, string title, int statusCode) : base(message)
        {
            StatusCode = statusCode;
            Title = title;
        }
    }
}
