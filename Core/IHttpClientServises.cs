using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IHttpClientServises
    {
       Task<string> SendRequest(string requstContent, string url);
    }
}
