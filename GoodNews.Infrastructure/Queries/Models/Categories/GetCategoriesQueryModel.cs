using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Categories
{
    public class GetCategoriesQueryModel : IRequest<IEnumerable<Category>>
    {
    }
}
