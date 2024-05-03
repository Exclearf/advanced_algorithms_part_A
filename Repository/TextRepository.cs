using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static App.Repository.ITextRepository;

namespace App.Repository
{
    public class TextRepository : ITextRepository
    {
        public string Text { get; private set; }
        public HashSet<char> Alphabet { get; set; }
        public char LowestAsciiCharacter { get; set; }
        public TextRepository(string filePath)
        {
            Text = File.ReadAllText(filePath);
            Alphabet = new HashSet<char>(Text);
            if (Alphabet.Count > 0)
            {
                LowestAsciiCharacter = Alphabet.Min();
            }
        }
    }
}
