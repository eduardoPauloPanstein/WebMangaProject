using DataAccessLayer;
using Entities.MangaS;
using Microsoft.Extensions.DependencyInjection;
using Shared.Responses;

namespace Test
{
    public class ResponseTest : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public ResponseTest(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void GetResponseUsingConstructor_ReturnIsEmptyNotData()
        {
            List<string> strings = new();
            strings.Add("oi");
            strings.Add("oie");

            var response = new DataResponse<string>("Data collected successfully.", true, strings);

            Assert.False(response.IsEmptyData);
        }
        [Fact]
        public void GetDataResponseUsingNewConstructor_ReturnIsNotEmptyData()
        {
            List<string> strings = new() { "1", "2", "3" };

            var response = new DataResponseTest<string>()
            {
                Data = strings,
                IsSuccess = true,
                Message = "Data collected successfully."
            };

            Assert.False(response.IsEmptyData);
        }
        public class DataResponseTest<T> : ResponseTeste
        {
            public List<T> Data { get; set; }
            public bool IsEmptyData { get { return this.Data == null || this.Data.Count == 0; } }
        }
        public class ResponseTeste
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public Exception? Exception { get; set; }

            public bool IsInfrastructureError { get { return this.Exception != null; } }
        }
    }
}
