
namespace SightReadingPractice.Domain.Models
{
    public class SheetSymbolsOutputModel
    {
        public KeySignature[] KeySignatures { get; }
        public Note[] Notes { get; }

        public SheetSymbolsOutputModel(KeySignature[] keySignatures, Note[] notes)
        {
            KeySignatures = keySignatures;
            Notes = notes;
        }
    }
}
