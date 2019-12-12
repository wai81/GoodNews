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
        private readonly IRatingCalculationSevice _calculationSevice;
        private readonly IAfinneService _afinne;
        private Dictionary<string, string> _affinDictionary;

        public NewsService(IMediator mediator,
            IParserSevice parserSevice, IRatingCalculationSevice calculationSevice, IAfinneService afinne)
        {
            _mediator = mediator;
            _parser = parserSevice;
            _afinne = afinne;
            _calculationSevice = calculationSevice;
        }

        public async Task<bool> RequestUpdateNewsFromSourse()
        {
            _affinDictionary = new Dictionary<string, string>();
            _affinDictionary = _afinne.LoadDictionary();

            //var categoryNews = new Dictionary<Guid,Category>();
            var dataSourse = GetParseNews();
            var newsAll = await _mediator.Send(new GetNewsQueryModel());

            //foreach (var news in dataSourse)
            //{
            //    news.IndexPositive = await _calculationSevice.GetContentRating(news.NewsDescription, _affinDictionary);
            //}

            if (await UpdateCategory(dataSourse))
            {
                foreach (var news in dataSourse)
                {
                   news.Category = await _mediator.Send(new GetCategoryByNameQueryModel(news.CategoryName));
                      if (newsAll.Count(c => c.LinkURL.Equals(news.LinkURL)) == 0)
                      {
                            await _mediator.Send(new AddRangeNewsCommandModel(dataSourse));
                      }
                }
            }

            return true;
        }

        public IEnumerable<News> GetParseNews()
        {
            List<News> news = new List<News>();
            string[] sorseUri = new string[]
            {
                @"http://s13.ru/rss",
                @"https://news.tut.by/rss/all.rss",
                @"https://people.onliner.by/feed"
            };
            
            for (int a = 0; a < sorseUri.Length; a++)
            {
                news.AddRange(_parser.ParserNewsFromSource(sorseUri[a]));
            }
            return news;
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

    }
}
