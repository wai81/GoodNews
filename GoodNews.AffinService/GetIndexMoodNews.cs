using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoodNews.ServiceNewsAnalysisContent
{
    public class GetIndexMoodNews : IGetIndexMoodNews
    {
        private readonly ILemmaServices _lemma;
        
        public GetIndexMoodNews(ILemmaServices lemma)
        {
            _lemma = lemma;
        }

       

        public Dictionary<string, string> LoadDictionary()
        {
            //try
            //{
            Dictionary<string, string> afinnDictionary = new Dictionary<string, string>();

            var afinnFile = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\.." + "\\.." + "\\.." + "\\.." + @"\GoodNews.AffinService"+@"\AFINN-ru.json");
            afinnDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(afinnFile);
            return afinnDictionary;


             //}
            //catch (Exception ex)
            //{
            //    return null;
            //}


        }

        public async Task<float> GetScore(string content)
        {
            float result;
            int scorePositive = 0;
            int scoreNegative = 0;
            int countWords = 0;

            var afinnDictionary = LoadDictionary();
            var contentDictionary = await _lemma.RequestToLemma(content);
            //var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(textL);

            foreach (var word in contentDictionary.Keys)
            {
                if (afinnDictionary.ContainsKey(word))
                {
                    var item = Convert.ToInt32(afinnDictionary[word]);

                    if (item > 0) scorePositive += item * contentDictionary[word];
                    if (item < 0) scoreNegative += item * contentDictionary[word];
                    countWords += contentDictionary[word];
                }
            }
            result = scorePositive / countWords;
            return result;
        }

    }
}
