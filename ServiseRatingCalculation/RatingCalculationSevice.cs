using Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiseRatingCalculation
{
    public class RatingCalculationSevice : IRatingCalculationSevice
    {
        private readonly ILemmaDictionary _lemma;
        //private readonly IAfinneService _afinne;
        public RatingCalculationSevice(ILemmaDictionary lemma)
        {
          _lemma = lemma;
           // _afinne = afinne;
        }

        public async Task<double> GetContentRating(string content,  Dictionary<string, string> affinDictionary)
        {
            try
            {
                int scorePositive = 0;
                int scoreNegative = 0;
                int countWords = 0;
                int scoreTotal = 0;
                int cTextLength = content.Length;
                var contentM = new string[cTextLength / 1500 + 1];
                for (int a = 0; a < contentM.Length; a++)
                {
                    contentM[a] = content.Substring(a * 1500, cTextLength - a * 1500 > 1500 ? 1500 : cTextLength - a * 1500);
                }

                for (int a = 0; a < contentM.Length; a++)
                {
                    int scoreP = 0;
                    int scoreN = 0;
                    int countW = 0;

                    var contentWord = await _lemma.DictionaryLemmaContentn(contentM[a]);
                    if (contentWord != null)
                    {
                        int wordCount = contentWord.Length;
                        if (wordCount != 0)
                            for (var i = 0; i < wordCount; i++)
                            {
                                var word = contentWord[i];
                                if (word != "")
                                {
                                    if (affinDictionary.ContainsKey(word))
                                    {
                                        var item = Convert.ToInt32(affinDictionary[word]);
                                        if (item > 0) scoreP += item;
                                        if (item < 0) scoreN += item;
                                        countW += 1;
                                    }
                                }
                            }
                    }
                    scorePositive += scoreP;
                    scoreNegative += scoreN;
                    countWords += countW;
                }
                scoreTotal = scorePositive + scoreNegative;
                double result;
                if (scoreTotal != 0)
                {
                    result = Math.Round((double)scoreTotal / countWords, 2);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
