namespace Shared.Responses
{
    public class SingleResponse<T> : Response
    {
        public SingleResponse(string message, bool hasSuccess, T item, Exception? ex = null) : base(message, hasSuccess, ex)
        {
            Item = item;
        }
        public SingleResponse()
        {

        }
        public T Item { get; set; }
        public bool NotFound { get { return this.Item == null; } }
    }
}
