using Core;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.HttpServices
{
    public class HttpClientServices : IHttpClientServises
    {
        public async Task<string> SendRequest(string requstContent, string url)
        {
            string response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request =
                        new HttpRequestMessage(new HttpMethod("POST"), url);
                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                    request.Content = new StringContent("[ { \"text\" : \"" + requstContent + "\" } ]", Encoding.UTF8, "application/json");
                    var x = client.SendAsync(request).Result;
                    response = await x.Content.ReadAsStringAsync();
                }
                return response;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
