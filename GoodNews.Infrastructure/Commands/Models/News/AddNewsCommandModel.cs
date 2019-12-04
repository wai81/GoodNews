using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.News
{
    public class AddNewsCommandModel : IRequest<bool>
    {
        public DB.News News { get; }
        public AddNewsCommandModel(DB.News news)
        {
            News = news;
        }

    }
}
