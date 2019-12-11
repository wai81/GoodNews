using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ILemmaDictionary
    {
        Task<string[]> DictionaryLemmaContentn(string content);
    }
}
