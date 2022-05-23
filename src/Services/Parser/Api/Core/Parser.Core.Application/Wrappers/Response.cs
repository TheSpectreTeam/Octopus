namespace Parser.Core.Application.Wrappers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public Response()
        {
        }

        public Response(string message)
        {
            IsSucceeded = false;
            Message = message;
        }

        public Response(T data, string message = null)
        {
            Data = data;
            IsSucceeded = true;
            Message = message;
        }
    }
}
