using System.Collections.Generic;

namespace SightReadingPractice.Domain.Models
{
    public class SheetSymbolsOutputModel
    {
        public List<KeySignature> KeySignatures { get; }
        public List<Note> Notes { get; }

        public SheetSymbolsOutputModel(List<KeySignature> keySignatures, List<Note> notes)
        {
            KeySignatures = keySignatures;
            Notes = notes;
        }
    }
}
