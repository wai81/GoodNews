using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Commands.Models.Upload;
using GoodNews.Infrastructure.Queries.Models;
using GoodNews.Infrastructure.Queries.Models.Categories;
using GoodNews.Infrastructure.Queries.Models.RatingCalculation;
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

            LoadAfinnDictionary();
            var dataSourse = GetParseNews();
            bool v = await UploadNews(dataSourse);
            if (v)
            {
                var listNewsIndexPositive = await CalculationIndexPositive();
                await _mediator.Send(new UpdateNewsCommandModel(listNewsIndexPositive));
            }
                        
            return true;
        }

        public async Task<IEnumerable<News>> CalculationIndexPositive()
        {
            var list_NewsIndexPositive = await _mediator.Send(new GetNewsIndexPositiveNullModel());
            foreach (var item in list_NewsIndexPositive)
            {
                double indexPositive = await _calculationSevice.GetContentRating(item.NewsContent, _affinDictionary);
                item.IndexPositive = indexPositive;
            }
            return list_NewsIndexPositive;
        }

        private async Task<bool> UploadNews(IEnumerable<News> dataSourse)
        {
            try
            {
                await _mediator.Send(new UploadNewsCommandModel(dataSourse));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void LoadAfinnDictionary()
        {
            _affinDictionary = new Dictionary<string, string>();
            _affinDictionary = _afinne.LoadDictionary();
        
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
     
    }
}
