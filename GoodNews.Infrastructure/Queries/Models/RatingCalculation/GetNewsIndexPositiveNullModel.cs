using GoodNews.DB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Queries.Models.RatingCalculation
{
    public class GetNewsIndexPositiveNullModel : IRequest<IEnumerable<News>>
    {
    
    }
}
