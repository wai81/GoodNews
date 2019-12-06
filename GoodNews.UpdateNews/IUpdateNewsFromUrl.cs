using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.UpdateNews
{
    public interface IUpdateNewsFromUrl
    {
        Task<bool> ParserNewsTUT();
        Task<bool> ParserNewsS13();
        Task<bool> ParserNewsOnlainer();
        //Task<bool> ParserNewsByUrl();
    }
}
