using SightReadingPractice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SightReadingPractice.Domain
{
    public static class FetchSheetSymbols
    {
        private static readonly char[] septimaRange = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
        private static readonly char[] keySignatureSigns = new char[] { '#', 'b' };
        private static readonly int[] septimaAreas = new int[] { -1, 0, 1 };
        private static readonly int howMany = 4;

        public static SheetSymbolsOutputModel CreateExercise(Random random, ClefType clefType)
        {
            return new SheetSymbolsOutputModel(GenerateKeySignatures(random), GenerateNotes(random, clefType));
        }

        //public static SheetSymbolsOutputModel CreateExerciseTest()
        //{
        //    KeySignature[] keySignatures = new KeySignature[] { new KeySignature('A', '#'), new KeySignature('B', 'b') };
        //    Note[] notes = new Note[] { new Note('C', septimaAreas[2]), new Note('D', septimaAreas[1]), new Note('E', septimaAreas[2]), new Note('C', septimaAreas[0]) };

        //    return new SheetSymbolsOutputModel(keySignatures, notes);
        //}

        private static KeySignature[] GenerateKeySignatures(Random random)
        {
            List<KeySignature> keySignatures = new();
            int randomizedQuantity = random.Next(0, howMany + 1);

            while (keySignatures.Count < randomizedQuantity)
            {
                char tone = septimaRange[random.Next(0, septimaRange.Length)];
                char keySignatureSign = keySignatureSigns[random.Next(0, keySignatureSigns.Length)];

                if (!keySignatures.Any(x => x.Tone == tone))
                {
                    keySignatures.Add(new KeySignature(tone, keySignatureSign));
                }
            }

            return keySignatures.ToArray();
        }

        public static bool IsOutsideBound(int septimaArea, string tone, ClefType clefType)
        {
            bool outsideUpperBound;
            bool outsideLowerBound;

            if (clefType == ClefType.Bass) 
            {
                outsideUpperBound = septimaArea == septimaAreas[0] && Array.IndexOf(septimaRange, tone) > 4;
                outsideLowerBound = septimaArea == septimaAreas[2] && Array.IndexOf(septimaRange, tone) < 2;
            }
            else
            {
                outsideUpperBound = septimaArea == septimaAreas[0] && Array.IndexOf(septimaRange, tone) > 2;
                outsideLowerBound = false;
            }

            return outsideUpperBound || outsideLowerBound;
        }

        private static Note[] GenerateNotes(Random random, ClefType clefType)
        {
            List<Note> notes = new();

            while (notes.Count < howMany)
            {
                string tone = septimaRange[random.Next(0, septimaRange.Length)].ToString();
                int septimaArea = septimaAreas[random.Next(0, septimaAreas.Length)];

                if (!IsOutsideBound(septimaArea, tone, clefType))
                {
                    notes.Add(new Note(tone, septimaArea));
                }
            }

            return notes.ToArray();
        }
    }
}
