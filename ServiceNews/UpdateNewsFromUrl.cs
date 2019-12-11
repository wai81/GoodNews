using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Queries.Models;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;

namespace ServiceNews
{
    public class NewsService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IParserSevice _parser;
        private readonly ILemmaDictionary _lemma;
        private readonly IAfinneService _afinne;
        private Dictionary<string, string> _affinDictionary;

        public NewsService(IMediator mediator,
            IParserSevice parserSevice, ILemmaDictionary lemma, IAfinneService afinne)
        {
            _mediator = mediator;
            _parser = parserSevice;
            _lemma = lemma;
            _afinne = afinne;
        }

        public async Task<bool> RequestUpdateNewsFromSourse(string sorseURL)
        {
            _affinDictionary = new Dictionary<string, string>();
            _affinDictionary = _afinne.LoadDictionary();

            //var categoryNews = new Dictionary<Guid,Category>();
            var dataSourse = _parser.ParserNewsFromSource(sorseURL);
            var newsAll = await _mediator.Send(new GetNewsQueryModel());

            foreach (var news in dataSourse)
            {
                news.IndexPositive = await GetScore(news.NewsDescription);
            }

            if (await UpdateCategory(dataSourse))
            {
                foreach (var news in dataSourse)
                {
                    if (newsAll.Count(c => c.LinkURL.Equals(news.LinkURL)) == 0)
                    {
                        news.Category = await _mediator.Send(new GetCategoryByNameQueryModel(news.CategoryName));

                    }
                }
            }

            return await _mediator.Send(new AddRangeNewsCommandModel(dataSourse));


        }

        private async Task<bool> UpdateCategory(IEnumerable<News> dataSourse)
        {
            List<Category> categoryName = new List<Category>();
            foreach (var cat in dataSourse)
            {
                if (await _mediator.Send(new GetCategoryByNameQueryModel(cat.CategoryName)) == null)
                {
                    categoryName.Add(new Category() { Name = cat.CategoryName });
                }
            }

            if (categoryName.Count == 0)
            {
                return true;
            }
            return await _mediator.Send(new AddCategoryCommandModel(categoryName));

        }

        public async Task<double> GetScore(string cText)
        {
            try
            {
                int scorePositive = 0;
                int scoreNegative = 0;
                int countWords = 0;
                int cTextLength = cText.Length;
                var contentM = new string[cTextLength / 1500 + 1];
                for (int a = 0; a < contentM.Length; a++)
                {
                    contentM[a] = cText.Substring(a * 1500, cTextLength - a * 1500 > 1500 ? 1500 : cTextLength - a * 1500);
                }

                for (int a = 0; a < contentM.Length; a++)
                {
                    int scoreP = 0;
                    int scoreN = 0;
                    int countW = 0;

                    var contentWord = await _lemma.DictionaryLemmaContentn(contentM[a]);
                    int wordCount = contentWord.Length;
                    if(wordCount!=0)
                    for (var i = 0; i < wordCount; i++)
                    {
                        var word = contentWord[i];
                        if (word != "")
                        {
                            if (_affinDictionary.ContainsKey(word))
                            {
                                var item = Convert.ToInt32(_affinDictionary[word]);

                                if (item > 0) scoreP += item;
                                if (item < 0) scoreN += item;
                                countW += 1;
                            }
                        }
                    }

                    scorePositive += scoreP;
                    scoreNegative += scoreN;
                    countWords += countW;
                }
                double result;
                if (countWords != 0)
                {
                     result = Math.Round((double)scorePositive / countWords,2);
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
