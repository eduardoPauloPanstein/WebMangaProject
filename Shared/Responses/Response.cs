namespace Shared.Responses
{
    public class Response
    {
        public Response(string message, bool hasSuccess, Exception? ex = null)
        {
            Message = message;
            HasSuccess = hasSuccess;
            Exception = ex;
        }
        public Response()
        {

        }


        public string Message { get; set; }
        public bool HasSuccess { get; set; }
        public Exception? Exception { get; set; }

        public bool IsInfrastructureError { get { return this.Exception != null; } }
    }
}
