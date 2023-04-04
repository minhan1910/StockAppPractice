using StockAppPractice.Constants;
using System.Text.Json;

namespace StockAppPractice.HttpClientHandler
{
    public class HttpClientProcessor : IHttpClientProcessor
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientProcessor(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Dictionary<string, object>?> GetJsonDataReadAsStream(string uri)
        {
            using(HttpClient httpClient = _httpClientFactory.CreateClient()) 
            { 
                var httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri(uri),
                    Method = HttpMethod.Get,
                };            

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequest);

                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>?>(response);   

                if (responseDictionary is null) 
                {
                    throw new ArgumentException(SystemConstant.RESPONSE_ERROR);
                }

                if (responseDictionary.ContainsKey(SystemConstant.ERROR))
                {
                    throw new InvalidOperationException(
                        Convert.ToString(responseDictionary[SystemConstant.ERROR]));
                }

                return responseDictionary;
            }
        }
    }
}
