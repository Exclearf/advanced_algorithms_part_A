using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    public interface IAlgorithmResult
    {
        int MatchIndex { get; }
        TimeSpan RunningTime { get; }
    }
}
