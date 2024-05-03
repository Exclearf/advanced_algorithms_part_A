using App.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    internal class Sundays : IAlgorithm
    {
        private string text { get; set; }
        private int textLength { get; set; }

        public Sundays(ITextRepository script)
        {
            text = "abbbc";
            textLength = text.Length;
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
            if (textLength < patternLength)
            {
                timer.Stop();
                return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
            }

            int i = 0;

            var shiftTable = CreateShiftTable(pattern);

            while (i <= textLength - patternLength)
            {
                int j = patternLength - 1;

                while (j >= 0 && pattern[j] == text[i + j])
                {
                    // Match pattern
                    j--;
                }

                if (j < 0)
                {
                    // Match found
                    timer.Stop();
                    return new AlgorithmResult(i, new TimeSpan(timer.ElapsedTicks));
                }
                else
                {
                    // Mismatch found
                    var nextChar = i + patternLength < textLength ? text[i + patternLength] : '\0';
                    i += shiftTable.ContainsKey(nextChar) ? shiftTable[nextChar] : patternLength+1;
                }

            }
            timer.Stop();
            return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
        }

        public bool FindWildcard(string pattern)
        {
            var shiftTable = CreateWildcardShiftTable(pattern);
            var wildcardCharTable = CreateWildcardCharTable(pattern);
            return isMatch(pattern.Replace("*", String.Empty), 0, 0, shiftTable, wildcardCharTable);
        }

        private bool isMatch(string pattern, int textIndex, int patternIndex, Dictionary<char, int> shiftTable, List<int> wildcardCharTable)
        {
            int i = textIndex;
            var patternLength = pattern.Length;
            if (string.IsNullOrEmpty(pattern))
            {
                return false;
            }
            if (patternLength == 0 || wildcardCharTable[patternIndex] == 1)
            {
                return true;
            }

            while (i <= textLength - patternLength)
            {
                int j = 0;

                while (j < patternLength - 1 && (pattern[j] == text[i + j] || pattern[j] == '?' || wildcardCharTable[j] == '*'))
                {
                    if (wildcardCharTable[j] == 1)
                    {
                        if (isMatch(pattern.Substring(j), i, j, shiftTable, wildcardCharTable))
                        {
                            return true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    j++;
                }

                if (j == patternLength - 1)
                {
                    return true;
                }
                else
                {
                    // Mismatch found
                    var nextChar = i + patternLength < textLength ? text[i + patternLength] : '\0';
                    i += shiftTable.ContainsKey(nextChar) ? shiftTable[nextChar] : patternLength + 1;
                }

            }
            return false;
        }

        private List<int> CreateWildcardCharTable(string pattern) => pattern.Select(x => x == '*' ? 1 : 0).ToList();

        private Dictionary<char, int> CreateWildcardShiftTable(string pattern)
        {
            int m = pattern.Length;
            var table = new Dictionary<char, int>();

            for (int i = 0; i < m; i++)
            {
                if (pattern[i] != '*' && pattern[i] != '?')
                {
                    table[pattern[i]] = m - i;
                }
            }

            return table;
        }

        private Dictionary<char, int> CreateShiftTable(string pattern)
        {
            int m = pattern.Length;
            var table = new Dictionary<char, int>();

            for(int i = 0; i < m; i++)
            {
                table[pattern[i]] = m - i;
            }

            return table;
        }
    }
}
