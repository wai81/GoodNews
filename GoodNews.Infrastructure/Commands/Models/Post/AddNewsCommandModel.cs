using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Post
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
