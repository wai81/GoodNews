using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ILemmaServices
    {
        //Task<string> RequestToLemma(string cText);
        Task<Dictionary<string, int>> RequestToLemma(string cText);

    }
}
