using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.UpdateNews
{
    public interface IUpdateNewsFromUrl
    {
        //Task<bool> ParserNewsByUrl(string url);
        Task<bool> ParserNewsByUrl();
    }
}
