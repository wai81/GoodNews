using Core;
using GoodNews.ServiceNewsAnalysisContent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.NewsServices.LemmaService
{
    public class LemmaDictionary : ILemmaDictionary
    {
        private readonly IHttpClientServises _httpClient;

        public LemmaDictionary(IHttpClientServises httpClient)
        {
            _httpClient = httpClient;
        }

        public Dictionary<string, int> ParsingResponseFromLemma(string responseLemma)
        {
            Dictionary<string, int> _content = new Dictionary<string, int>();
            try
            {
                var json = JsonConvert.DeserializeObject<List<ParsingModel>>(responseLemma);
                var annotationsArray = json[0].annotations;
                foreach (var lemma in annotationsArray.Lemma)
                {
                    var item = lemma.Value;
                    if (item != "")
                    {
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
                return _content;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Dictionary<string, int>> DictionaryLemmaContentn(string cText)
        {         
            try
            {
                //var response = new Dictionary<string, int>();
                var result = new Dictionary<string, int>();
                string urlLematization = "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=714ec07b4d7c15088d0d17381d78f4ffe4582ef7";
                //int cTextLength = cText.Length;
                //string[] content;
                //content = new string[cTextLength / 1500 + 1];
                //for (int a = 0; a < content.Length; a++)
                //{
                //    content[a] = cText.Substring(a * 1500, cTextLength - a * 1500 > 1500 ? 1500 : cTextLength - a * 1500);
                //}

                //for (int a = 0; a < content.Length; a++)
                //{
                    var listWordsContent = await _httpClient.SendRequest(cText, urlLematization);
                    var response = ParsingResponseFromLemma(listWordsContent);
                    //foreach (KeyValuePair<string, int> keyValues in jsonList)
                    //{
                    //    string key = keyValues.Key;
                    //    int newValue = keyValues.Value;
                    //    int val;
                    //    if (key != null || newValue != null)
                    //    {
                    //        if (result.TryGetValue(key, out val))
                    //        {
                    //            result[key] = val + newValue;
                    //        }
                    //        else
                    //        {
                    //            result.Add(key, newValue);
                    //        }
                    //    }
                    //}


                //}
                //response = result;
                return response;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

    }
}
