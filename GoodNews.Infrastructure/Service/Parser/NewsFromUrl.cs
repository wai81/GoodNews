using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.News;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;

namespace GoodNews.Infrastructure.Service.Parser
{
    public class NewsFromUrl : INewsFromUrl
    {
        private readonly IMediator _mediator;
        private readonly IParserSevice _parser;

        public NewsFromUrl(IMediator mediator, IParserSevice parserSevice)
        {
            _mediator = mediator;
            _parser = parserSevice;
        }

        public async Task<bool> GetNewsUrl(string url)
        {
            IEnumerable<News> news = new List<News>();
            news = _parser.GetNewsFromUrl(url);

            var newsAll = await _mediator.Send(new GetNewsQueryModel());
            
            foreach (var n in news)
            {
                if (newsAll.Count(c => c.LinkURL.Equals(n.LinkURL)) == 0)
                {
                    _mediator.Send(new AddNewsCommandModel(n));
                }
            }
            return true;
        }
    }
}
