using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    class AlgorithmResult : IAlgorithmResult
    {
        public TimeSpan RunningTime { get; private set; }
        public int MatchIndex { get; private set; }

        public AlgorithmResult(int matchIndex, TimeSpan executionTimeSpan) {
            MatchIndex = matchIndex;
            RunningTime = executionTimeSpan;
        }
    }
}
