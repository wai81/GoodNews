using Core;
using Newtonsoft.Json;
using ServiceLemmatization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLemmatization
{
    public class LemmaDictionary : ILemmaDictionary
    {
        private readonly IHttpClientServises _httpClient;
        private List<string> lemmaList;
        private const string urlLematization = "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=714ec07b4d7c15088d0d17381d78f4ffe4582ef7";

        public LemmaDictionary(IHttpClientServises httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string[]> DictionaryLemmaContentn(string cText)
        {
            try
            {
                var listWordsContent = await _httpClient.SendRequest(cText, urlLematization);
                var json = JsonConvert.DeserializeObject<List<ParsingModel>>(listWordsContent);
                var annotationsArray = json[0].annotations;
                int countLema = annotationsArray.Lemma.Count;
                var requestArray = new string[countLema];
                int i = 0;
                foreach (var lemma in annotationsArray.Lemma)
                {
                    string item = lemma.Value;
                    requestArray[i] = item;
                    ++i;
                }
                return requestArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
