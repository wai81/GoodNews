using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.DB;

namespace GoodNews.API.Models
{
    public class NewsDetailsModel
    {
        public News News { get; set; }
        //public Category Category { get; set; }
        public IEnumerable<NewsComment> NewsComments { get; set; }
    }
}
