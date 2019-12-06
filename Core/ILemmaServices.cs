using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ILemmaServices
    {
        Task<string> RequestForLemma(string content);
    }
}
