using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightReadingPractice.Domain
{
    public class KeySignature
    {
        public char Tone { get; }
        public char Signature { get; }

        public KeySignature(char tone, char sign)
        {
            Tone = tone;
            Signature = sign;
        }
    }
}
