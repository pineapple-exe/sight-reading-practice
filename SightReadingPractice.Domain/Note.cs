
namespace SightReadingPractice.Domain
{
    public class Note
    {
        public string Tone { get; }
        public int SeptimaArea { get; }

        public Note(string tone, int septimaArea)
        {
            Tone = tone;
            SeptimaArea = septimaArea;
        }
    }
}
