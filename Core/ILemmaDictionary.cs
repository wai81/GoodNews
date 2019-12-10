using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ILemmaDictionary
    {
        Task<Dictionary<string, int>> DictionaryLemmaContentn(string cText);
    }
}
