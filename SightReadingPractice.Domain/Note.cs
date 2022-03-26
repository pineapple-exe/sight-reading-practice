using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightReadingPractice.Domain
{
    public class Note
    {
        public char Tone { get; }
        public int SeptimaArea { get; }

        public Note(char tone, int septimaArea)
        {
            Tone = tone;
            SeptimaArea = septimaArea;
        }
    }
}
