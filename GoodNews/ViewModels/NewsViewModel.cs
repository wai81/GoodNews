using System.Collections.Generic;
using GoodNews.DB;

namespace GoodNews.ViewModels
{
    public class NewsViewModel
    {
        public News News { get; set; }
        public IEnumerable<NewsComment> NewsComments { get; set; }
    }
}
