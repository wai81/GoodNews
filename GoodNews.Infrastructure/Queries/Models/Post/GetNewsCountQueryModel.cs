using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Queries.Models.Post
{
    public class GetNewsCountQueryModel : IRequest<long>
    {
    }
}
