using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IGetIndexMoodNews
    {
        Task<double> GetScore(string cText);
    }
}
