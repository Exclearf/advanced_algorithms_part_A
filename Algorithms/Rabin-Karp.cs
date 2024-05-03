using App.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Algorithms
{
    internal class Rabin_Karp : IAlgorithm
    {
        private string text { get; set; }
        private int textLength { get; set; }
        private int primeNumber { get; set; }
        public BigInteger power { get; set; }
        public int alphabetSize { get; set; }

        public Rabin_Karp(ITextRepository movieScript, int primeNumber, int alphabetSize)
        {
            text = movieScript.Text;
            textLength = text.Length;
            this.primeNumber = primeNumber;
            this.alphabetSize = alphabetSize;
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
            power = BigInteger.Pow(alphabetSize, patternLength - 1);
            BigInteger hash = calculateHash(pattern);
            BigInteger currentWindowHash = 0;

            for (int i = 0; i <= textLength - patternLength; i++)
            {
                if (i == 0)
                    currentWindowHash = calculateHash(text.Substring(0, patternLength));
                else
                    currentWindowHash = rollHash(currentWindowHash, text[i - 1], patternLength, text[i + patternLength - 1], power);

                if (currentWindowHash < 0)
                    currentWindowHash += primeNumber;

                if (currentWindowHash == hash)
                {
                    for (int j = 0; j < patternLength; j++)
                    {
                        if (pattern[j] != text[i + j])
                            break;
                        if (j == patternLength - 1)
                        {
                            timer.Stop();
                            return new AlgorithmResult(i, new TimeSpan(timer.ElapsedTicks));
                        }
                    }
                }
            }
            timer.Stop();
            return new AlgorithmResult(-1, new TimeSpan(timer.ElapsedTicks));
        }

        private BigInteger calculateHash(string textWindow)
        {
            BigInteger hash = 0;
            var textWindowLength = textWindow.Length;
            for (int i = 0; i < textWindowLength; i++)
            {
                hash += textWindow[i] * BigInteger.Pow(alphabetSize, textWindowLength - (i + 1)) & (primeNumber - 1);
            }
            return hash & (primeNumber - 1);
        }

        private BigInteger rollHash(BigInteger currentHash, char previousChar, int patternLength, char nextChar, BigInteger power)
        {
            BigInteger newHash = (currentHash - previousChar * power);
            newHash = (newHash * alphabetSize + nextChar) & (primeNumber - 1);
            return newHash;
        }
    }
}