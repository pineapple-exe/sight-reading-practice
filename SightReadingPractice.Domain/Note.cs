
using System;

namespace SightReadingPractice.Domain
{
    public class Note : IEquatable<Note>
    {
        public string Tone { get; set; }
        public int SeptimaArea { get; }

        public Note(string tone, int septimaArea)
        {
            Tone = tone;
            SeptimaArea = septimaArea;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Note);
        }

        public bool Equals(Note other)
        {
            return other != null &&
                   Tone == other.Tone &&
                   SeptimaArea == other.SeptimaArea;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tone, SeptimaArea);
        }
    }
}
