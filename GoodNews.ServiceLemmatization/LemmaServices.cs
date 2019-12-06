using Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.ServiceLemmatization
{
    public class LemmaServices: ILemmaServices
    {

        public async Task<string> RequestForLemma(string cText)
        {

            try
            {
                string[] content;

                content = new string[] { };

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Post,
                        "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=714ec07b4d7c15088d0d17381d78f4ffe4582ef7");
                request.Content = new StringContent("[{\"text\":\"" + content + "\"}]", Encoding.UTF8, "application/json");

                var x = client.SendAsync(request).Result;
                var response=await x.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                return string.Empty;
                
            }
            
            
        }
    }
}
