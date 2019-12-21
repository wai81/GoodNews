using GoodNews.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.API.Models
{
    public class NewsModel
    {
        public long CountPages { get; set; }
        public int CurrentNumPage { get; set; }
        public int CountNewsOnPage { get; set; }
        public IEnumerable<News> News { get; set; }
      
    }
}
