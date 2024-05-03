using App.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    internal class KMP: IAlgorithm
    {
        private string text { get; set; }
        private int textLength { get; set; }

        public KMP(ITextRepository movieScript)
        {
            text = movieScript.Text;
            textLength = movieScript.Text.Length;
        }

        public IAlgorithmResult Find(string pattern)
        {
            var timer = Stopwatch.StartNew();
            if (string.IsNullOrEmpty(pattern))
            {
                timer.Stop();
                return new AlgorithmResult(0, new TimeSpan(timer.ElapsedTicks));
            }
            var patternLength = pattern.Length;
            var lps = new List<int>(new int[patternLength]);
            int prevLpsValue = 0, i = 1;
            
            while (i < patternLength)
            {
                if (pattern[i] == pattern[prevLpsValue])
                {
                    lps[i] = prevLpsValue + 1;
                    prevLpsValue++;
                    i++;
                }
                else if (prevLpsValue == 0)
                {
                    lps[i] = 0;
                    i++;
                }
                else
                {
                    prevLpsValue = lps[prevLpsValue - 1];
                } 
            }

            // Reuse vars
            i = 0;
            int j = 0;
            while (i < textLength) {
                if (text[i] == pattern[j])
                {
                    j++;
                    i++;
                }
                else
                {
                    if(j == 0)
                    {
                        i++;
                    }
                    else
                    {
                        j = lps[j - 1];
                    }
                }
                if (j == patternLength)
                {
                    timer.Stop();
                    return new AlgorithmResult(i-patternLength, new TimeSpan(timer.ElapsedTicks));
                }
            }
            timer.Stop();
            return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
        }
    }
}
