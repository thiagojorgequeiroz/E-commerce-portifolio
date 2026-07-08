namespace Catalog.Application.Contract.v1.Commun
{
    public class CommunMessageResponse
    {
        public string SuccessMessage { get; set; }

        public CommunMessageResponse(string successMessage)
        {
            SuccessMessage = successMessage;
        }
    }
}
