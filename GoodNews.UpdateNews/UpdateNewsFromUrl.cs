using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.News;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using ServiceParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.UpdateNews
{
    public class UpdateNewsFromUrl : IUpdateNewsFromUrl
    {
        private readonly IMediator _mediator;
        private readonly IParserSevice _parser;

        public UpdateNewsFromUrl(IMediator mediator, IParserSevice parserSevice)
        {
            _mediator = mediator;
            _parser = parserSevice;
        }

        //public async Task<bool> ParserNewsByUrl(string url)
        public async Task<bool> ParserNewsByUrl()
        {
            IEnumerable<News> news_tut = new List<News>();
            IEnumerable<News> news_s13 = new List<News>();
            IEnumerable<News> news_onl = new List<News>();
            var newsAll = await _mediator.Send(new GetNewsQueryModel());
            Parallel.Invoke(
                async () =>
                {
                    news_tut = await _parser.ParserNewsFromUrl(@"https://news.tut.by/rss/all.rss");
                    foreach (var t in news_tut)
                    {
                        if (newsAll.Count(c => c.LinkURL.Equals(t.LinkURL)) == 0)
                        {
                            await _mediator.Send(new AddNewsCommandModel(t));
                        }
                    }
                },
                async () =>
                {
                    news_s13 = await _parser.ParserNewsFromUrl(@"http://s13.ru/rss");
                    foreach (var s in news_s13)
                    {
                        if (newsAll.Count(c => c.LinkURL.Equals(s.LinkURL)) == 0)
                        {
                            await _mediator.Send(new AddNewsCommandModel(s));
                        }
                    }
                },
                async ()=>
                {
                    news_onl = await _parser.ParserNewsFromUrl(@"https://people.onliner.by/feed");
                    foreach (var o in news_onl)
                    {
                        if (newsAll.Count(c => c.LinkURL.Equals(o.LinkURL)) == 0)
                        {
                            await _mediator.Send(new AddNewsCommandModel(o));
                        }
                    }
                }
              );
            return true;
        }
    }
}
