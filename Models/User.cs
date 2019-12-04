using System;
using Microsoft.AspNetCore.Identity;

namespace GoodNews.DB
{
   
    public class User : IdentityUser
    {
        public int Year { get; set; }

        public static implicit operator string(User v)
        {
            throw new NotImplementedException();
        }
    }
}
