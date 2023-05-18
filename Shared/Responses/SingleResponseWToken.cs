namespace Shared.Responses
{
    public class SingleResponseWToken<T> : SingleResponse<T>
    {
        public SingleResponseWToken(string message, bool hasSuccess, T item, string token, Exception? ex) : base(message, hasSuccess, item, ex)
        {
            Token = token;
            Item = item;
        }
        public SingleResponseWToken()
        {

        }

        public string Token { get; set; }
        public T Item { get; set; }
        public bool NotFound { get { return this.Item == null; } }
    }
}
