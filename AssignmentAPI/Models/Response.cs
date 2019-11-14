namespace AssignmentAPI.Models
{
    public class Response<T>
    {
        public string Message { get; set; }
        public bool Susscess { get; set; }
        public bool IsException { get; set; }
        public string ErrorMessage { get; set; }
        public T Payload { get; set; }

        public Response()
        {
            Message = string.Empty;
            Susscess = false;
            IsException = false;
            ErrorMessage = string.Empty;
        }
    }
}
