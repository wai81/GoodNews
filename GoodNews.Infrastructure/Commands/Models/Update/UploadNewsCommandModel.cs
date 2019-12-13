using GoodNews.DB;
using MediatR;
using System.Collections.Generic;

namespace GoodNews.Infrastructure.Commands.Models.Upload
{
    public class UploadNewsCommandModel : IRequest<bool>
    {
        public IEnumerable<News> News { get; }

        public UploadNewsCommandModel(IEnumerable<News> news)
        {
            News = news;
        }
    }
}