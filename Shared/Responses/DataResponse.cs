namespace Shared.Responses
{
    public class DataResponse<T> : Response
    {
        public DataResponse(string message, bool hasSuccess, List<T> data, Exception? ex = null) : base(message, hasSuccess, ex)
        {
            Data = data;
        }
        public DataResponse()
        {

        }

        public List<T> Data { get; set; }
        public bool IsEmptyData { get { return this.Data == null || this.Data.Count == 0; } }
    }
}
