using GoodNews.DB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Commands.Models.Post
{
    public class UpdateNewsCommandModel:IRequest<bool>
    {
         public IEnumerable<News> News { get; }

    public UpdateNewsCommandModel(IEnumerable<News> news)
    {
        News = news;
    }
}
}
