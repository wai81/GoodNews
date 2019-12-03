using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Queries.Models.Post;
using MediatR;

namespace GoodNews.Infrastructure.Service.Parser
{
    public class NewsFromUrl : INewsFromUrl
    {
        private readonly IMediator mediator;
        private readonly IParserSevice parser;

        public NewsFromUrl(IMediator mediator, IParserSevice parserSevice)
        {
            this.mediator = mediator;
            parser = parserSevice;
        }

        public async Task<bool> GetNewsUrl(string url)
        {
            var newsAll = await mediator.Send(new GetNewsQueryModel());
            var news = parser.GetNewsFromUrlAsync(url);
            foreach (var n in news)
            {
                if (newsAll.Count(c => c.LinkURL.Equals(n.LinkURL)) == 0)
                {
                   await mediator.Send(new AddNewsCommandModel(n));
                }
            }
            return true;
        }
    }
}
