namespace TweetService.DAL.Context
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public ServiceResponse<T> BadResponse(string message)
        {
            Success = false;
            Message = message;
            return this;
        }
    }
}
