using MediatR;

namespace GoodNews.Infrastructure.Queries.Models
{
    public class GetNewsByUrlExistModel : IRequest<bool>
    {
        public string Url { get; }

        public GetNewsByUrlExistModel(string url)
        {
            Url = url;
        }
    }
}
