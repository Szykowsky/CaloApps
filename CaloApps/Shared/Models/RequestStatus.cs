namespace CaloApps.Shared.Models
{
    public class RequestStatus
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public RequestStatus(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
