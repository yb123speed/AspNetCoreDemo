namespace CustomExceptionSample
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
    }
}