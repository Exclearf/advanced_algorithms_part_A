using App.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    internal class GusfieldZ : IAlgorithm
    {
        private string text { get; set; }
        private int textLength { get; set; }
        private char specialCharacter { get; set; }

        public GusfieldZ(ITextRepository script, char specialCharacter)
        {
            text = script.Text;
            textLength = script.Text.Length;
            this.specialCharacter = specialCharacter;
        }

        public IAlgorithmResult Find(string pattern)
        {
            var timer = Stopwatch.StartNew();
            if (string.IsNullOrEmpty(pattern))
            {
                timer.Stop();
                return new AlgorithmResult(0, new TimeSpan(timer.ElapsedTicks));
            }

            var concatString = pattern + specialCharacter + text;
            var concatStringLenth = concatString.Length;
            var patternLength = pattern.Length;

            // Create Z array
            var zArray = new int[concatStringLenth];
            int left = 0, right = 0;
            for (int i = 1; i < concatStringLenth; ++i)
            {
                if (i > right)
                {
                    right = i;
                    left = right;
                    while (right < concatStringLenth && concatString[right - left] == concatString[right])
                    {
                        right++;
                    }
                    zArray[i] = right - left;
                    right--;
                }
                else
                {
                    int i1 = i - left;

                    if (zArray[i1] < right - i + 1)
                    {
                        zArray[i] = zArray[i1];
                    }
                    else
                    {
                        left = i;
                        while (right < concatStringLenth && concatString[right - left] == concatString[right])
                        {
                            right++;
                        }
                        zArray[i] = right - left;
                        right--;
                    }
                }
            }


            for (int i = 0; i < concatStringLenth; i++)
            {
                if (zArray[i] == patternLength)
                {
                    timer.Stop();
                    return new AlgorithmResult(i - patternLength - 1, new TimeSpan(timer.ElapsedTicks));
                }
            }
            timer.Stop();
            return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
        }
    }
}