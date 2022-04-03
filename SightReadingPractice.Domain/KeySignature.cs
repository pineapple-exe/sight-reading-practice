
namespace SightReadingPractice.Domain
{
    public class KeySignature
    {
        public string Tone { get; }
        public string Signature { get; }

        public KeySignature(string tone, string sign)
        {
            Tone = tone;
            Signature = sign;
        }
    }
}
