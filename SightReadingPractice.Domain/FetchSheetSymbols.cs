using SightReadingPractice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SightReadingPractice.Domain
{
    public static class FetchSheetSymbols
    {
        private static readonly string[] septimaRange = new string[] { "A", "B", "C", "D", "E", "F", "G" };
        private static readonly string[] keySignatureSigns = new string[] { "#", "b" };
        private static readonly int[] septimaAreas = new int[] { -1, 0, 1 };
        private static readonly int howMany = 4;

        public static SheetSymbolsOutputModel CreateExercise(Random random, ClefType clefType)
        {
            Note[] notes = GenerateNotes(random, clefType);
            string[] teamTones = notes.Select(n => n.Tone).ToArray();
            KeySignature[] keySignatures = GenerateKeySignatures(random, teamTones);

            return new SheetSymbolsOutputModel(keySignatures, notes);
        }

        private static KeySignature[] GenerateKeySignatures(Random random, string[] teamTones)
        {
            List<KeySignature> keySignatures = new();
            string[] teamTonesDistinct = teamTones.Distinct().ToArray();
            int randomizedQuantity = random.Next(0, teamTonesDistinct.Length + 1);

            while (keySignatures.Count < randomizedQuantity)
            {
                string[] toneCandidates = teamTonesDistinct.Where(t => !keySignatures.Any(k => k.Tone == t)).ToArray();
                string tone = toneCandidates[random.Next(0, toneCandidates.Length)];
                string keySignatureSign = keySignatureSigns[random.Next(0, keySignatureSigns.Length)];

                keySignatures.Add(new KeySignature(tone, keySignatureSign));
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

                if (!IsOutsideBound(septimaArea, tone, clefType) && !notes.Any(n => n.Tone == tone && n.SeptimaArea == septimaArea))
                {
                    notes.Add(new Note(tone, septimaArea));
                }
            }

            return notes.ToArray();
        }
    }
}
