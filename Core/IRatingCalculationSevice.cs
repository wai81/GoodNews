using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IRatingCalculationSevice
    {
        Task<double?> GetContentRating(string content, Dictionary<string, string> affinDictionary);

    }
}
