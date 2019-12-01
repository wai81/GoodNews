using System;
using System.Collections.Generic;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Post
{
    public class AddRangeNewsCommandModel : IRequest<Guid[]>
    {
        public IEnumerable<News> News { get; }
        public AddRangeNewsCommandModel(IEnumerable<News> news)
        {
            News = news;
        }

    }
}
