using App.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    internal class BruteForce : IAlgorithm
    {
        private string text {  get; set; }
        private int textLength {  get; set; }
        public BruteForce(ITextRepository movieScript)
        {
            text = "abc";
            textLength = text.Length;
        }
        public IAlgorithmResult Find(string pattern)
        {
            var timer = Stopwatch.StartNew();
            var patternLength = pattern.Length;
            for (int i = 0; i <= textLength - patternLength; i++) {
                int j = 0;
                while (j < patternLength && text[i+j]== pattern[j])
                {
                    j++;
                }
                if (j == patternLength)
                {
                    timer.Stop();
                    return new AlgorithmResult(i, new TimeSpan(timer.ElapsedTicks));
                }
            }
            timer.Stop();
            return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
        }
        public bool FindWildcard(string pattern)
        {
            if (pattern == "*" || pattern == "")
            {
                return true;
            }
            if(text == "")
            {
                return false;
            }

            return isMatch(pattern, 0);
        }

        private bool isMatch(string pattern, int textIndex)
        {
            var patternLength = pattern.Length;
            for (int i = textIndex; i <= textLength - patternLength; i++)
            {
                int j = 0;
                while (j < patternLength)
                {
                    if((text[i + j] == pattern[j] || pattern[j] == '?'))
                    {
                        j++;
                    }
                    else if(pattern[j] == '*')
                    {
                        if (isMatch(pattern.Substring(j+1), i + j))
                        {
                            return true;
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (j == patternLength)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
