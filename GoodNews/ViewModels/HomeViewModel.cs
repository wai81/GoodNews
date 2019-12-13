using System.Collections.Generic;
using GoodNews.DB;

namespace GoodNews.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<News> News { get; set; }

    }
}
