﻿using System.Threading.Tasks;

namespace Core
{
    public interface INewsService
    {
        Task<bool> RequestUpdateNewsFromSourse();
       
        //Task<bool> ParserNewsTUT();
        //Task<bool> ParserNewsS13();
        //Task<bool> ParserNewsOnlainer();
        //Task<bool> ParserNewsByUrl();
    }
}
