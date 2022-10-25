using System.Text.Json;

namespace HM.Product.API
{
    public class HttpServiceProvider : IHttpServiceProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpServiceProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<T?> InvokeService<T>(string url)
        {
            try
            {
                var _httpRequestMessage = new HttpRequestMessage { Method = HttpMethod.Get, RequestUri = new Uri(url) };
                using var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(_httpRequestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseData);
                }
                else
                {
                    return default;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
