using Core;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHttp
{
    public class HttpClientServices : IHttpClientServises
    {
        public async Task<string> SendRequest(string requstContent, string reqestUri)
        {
            try
            {
                string response = null;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request =
                        new HttpRequestMessage(new HttpMethod("POST"), reqestUri);
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
