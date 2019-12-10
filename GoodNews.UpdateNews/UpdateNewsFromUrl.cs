using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Queries.Handlers.Categories;
using GoodNews.Infrastructure.Queries.Models;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using ServiceParser.Parser;

namespace GoodNews.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IParserSevice _parser;
        private readonly ILemmaDictionary _lemma;

        public NewsService(IMediator mediator,
            IParserSevice parserSevice, ILemmaDictionary lemma)
        {
            _mediator = mediator;
            _parser = parserSevice;
            _lemma = lemma;
          
        }

        public async Task<bool> RequestUpdateNewsFromSourse(string sorseURL)
        {
            //var categoryNews = new Dictionary<Guid,Category>();
            List<Category> categoryName = new List<Category>();
            var dataSourse = _parser.ParserNewsFromSource(sorseURL);
            var newsAll = await _mediator.Send(new GetNewsQueryModel());
            foreach (var cat in dataSourse)
            {
                if (await _mediator.Send(new GetCategoryByNameQueryModel(cat.CategoryName)) == null)
                {
                    categoryName.Add(new Category() { Name = cat.CategoryName });
                }
            }

            if (await _mediator.Send(new AddCategoryCommandModel(categoryName)))
            { }


            foreach (var news in dataSourse)
            {
                if (newsAll.Count(c => c.LinkURL.Equals(news.LinkURL)) == 0)
                {
                    news.Category = await _mediator.Send(new GetCategoryByNameQueryModel(news.CategoryName));
                }
            }

            foreach (var news in dataSourse)
            {
                var i = await _lemma.DictionaryLemmaContentn(news.NewsContent);
             Console.WriteLine(1);
               // news.IndexPositive = await _getIndex.GetScore(news.NewsDescription);
            }
            //await _mediator.Send(new AddNewsCommandModel(news));
            return true;
        }

        //public async Task<double> GetScore(string content)
        //{
        //    double result;
        //    var afinnDictionary = LoadDictionary();
        //    var contentDictionary = await RequestToLemma(content);
        //    //var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(textL);
        //    int scorePositive = 0;
        //    int scoreNegative = 0;
        //    int countWords = 0;
        //    foreach (var word in contentDictionary.Keys)
        //    {
        //        if (afinnDictionary.ContainsKey(word))
        //        {
        //            var item = Convert.ToInt32(afinnDictionary[word]);

        //            if (item > 0) scorePositive += item * contentDictionary[word];
        //            if (item < 0) scoreNegative += item * contentDictionary[word];
        //            countWords += contentDictionary[word];
        //        }
        //    }
        //    result = (double)scorePositive / countWords;
        //    return result;
        //}

    }
}
