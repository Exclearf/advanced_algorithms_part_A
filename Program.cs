using App.Algorithms;
using App.Repository;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var pattern = "gtga";
            var pattern = "121";
            var warAndPeace = "./Resources/warAndPeace.txt";
            var dna = "./Resources/dnaSequence.txt";
            var text = new TextRepository(dna);

            // Brute-Force Algorithm
            var bruteForceAlgorithm = new BruteForce(text);
            // Synday's Boyer-Moore Algorithm Improvement
            var sundaysAlgorithm = new Sundays(text);
            // Knuth-Morris-Pratt Algorithm
            var kmp = new KMP(text);
            // Rabin-Karp Algorithm
            var rabin_karp = new Rabin_Karp(text, (int)Math.Pow(2, 5), 5);
            // Gusfield Z
            var gusfieldZ = new GusfieldZ(text, '+');
            // FSM
            var fsm = new FSM(text);
        }
    }
}
