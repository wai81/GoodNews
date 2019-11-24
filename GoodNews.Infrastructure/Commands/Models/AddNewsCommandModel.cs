using GoodNews.DB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Commands.Models
{
    public class AddNewsCommandModel : IRequest<bool>
    {
        public News News { get; }
        public AddNewsCommandModel(News news)
        {
            News = news;
        }
    }
}
