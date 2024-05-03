using App.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    internal class FSM : IAlgorithm
    {
        private string text { get; set; }
        private int textLength { get; set; }
        public FSM(ITextRepository script)
        {
            text = script.Text;
            textLength = script.Text.Length;
        }

        public IAlgorithmResult Find(string pattern)
        {
            var timer = Stopwatch.StartNew();
            if (string.IsNullOrEmpty(pattern))
            {
                timer.Stop();
                return new AlgorithmResult(0, new TimeSpan(timer.ElapsedTicks));
            }
            var alphabetSize = 3;
            var patternLength = pattern.Length;
            var transitions = new int[patternLength + 1, alphabetSize];
            var prevLps = 0;
            var currentState = 0;

            // Initial state for all is 0
            for (int i = 0; i < alphabetSize; ++i)
            {
                transitions[0, i] = 0;
            }

            // For first character - move to state 1
            transitions[0, pattern[0]] = 1;

            // use lps for rest
            for(    int i = 1;  i < patternLength; ++i)
            {
                for (int j = 0; j < alphabetSize; ++j)
                {
                    transitions[i, j] = transitions[prevLps, j];
                }

                transitions[i, pattern[i]] = i+1;
                prevLps = transitions[prevLps, pattern[i]];
            }

            for(int i = 0; i < textLength; i++)
            {
                currentState = transitions[currentState, text[i]];

                if(currentState == patternLength)
                {
                    timer.Stop();
                    return new AlgorithmResult(i-patternLength+1, new TimeSpan(timer.ElapsedTicks));
                }
            }
            timer.Stop();
            return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
        }
    }
}
