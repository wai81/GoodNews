using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.ServiceLemmatization
{
    public class LemmaServices : ILemmaServices
    {

        public async Task<string> SendRequest(string requstContent)
        {
            string response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpRequestMessage request = 
                        new HttpRequestMessage(new HttpMethod("POST"), "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=714ec07b4d7c15088d0d17381d78f4ffe4582ef7");
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

        public Dictionary<string, int> ParsingResponseFromLemma(string responseLemma)
        //public string ParsingResponseFromLemma(string responseLemma)
        {
            Dictionary<string, int> _content= new Dictionary<string, int>();

            try
            {
                var json = JsonConvert.DeserializeObject<List<ParsingModel>>(responseLemma);
                var annotationsArray = json[0].annotations;
                foreach (var lemma in annotationsArray.Lemma)
                {
                    var item = lemma.Value;
                    if (item != "")
                    {
                        //content[item] = 1;
                        if (_content.ContainsKey(item))
                        {
                            _content[item] += 1;
                        }
                        else
                        {
                            _content[item] = 1;
                        }
                    }
                }

                //string dictionaryString = "{";
                //foreach (KeyValuePair<string, int> keyValues in content)
                //{
                //    dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
                //}
                //return dictionaryString.TrimEnd(',', ' ') + "}";
                return _content;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public async Task<String> RequestToLemma(string cText)
        public async Task<Dictionary<string, int>> RequestToLemma(string cText)
        {
                       
            try
            {
                //string response = null;
                Dictionary<string, int> response = new Dictionary<string, int>();
                int cTextLength = cText.Length;
                string[] content;

                content = new string[cTextLength / 1500 + 1];
                for (int a = 0; a < content.Length; a++)
                {
                    content[a] = cText.Substring(a * 1500, cTextLength - a * 1500 > 1500 ? 1500 : cTextLength - a * 1500);
                }

                for (int a = 0; a < content.Length; a++)
                {
                    var listWordsContent = await SendRequest(content[a]);
                    var jsonList = ParsingResponseFromLemma(listWordsContent);
                    foreach (KeyValuePair<string, int> keyValues in jsonList)
                    {
                        var key = keyValues.Key;
                        var newValue = keyValues.Value;
                        int val;
                        if (response.TryGetValue(key, out val))
                        {
                            response[key] = val + newValue;
                        }
                        else
                        {
                          response.Add(key, newValue);
                        }
                       
                    }
                     //response = $"{response}{jsonList}";
                }
                     
                return response;
            }
            catch (Exception ex)
            {
                return null;

            }


        }

        
    }
}
