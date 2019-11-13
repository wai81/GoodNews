using System.Collections.Generic;
using GoodNews.DB;
using Models;

namespace GoodNews.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<News> News { get; set; }

    }
}
