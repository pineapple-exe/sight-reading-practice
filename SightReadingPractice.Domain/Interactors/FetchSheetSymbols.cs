using SightReadingPractice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SightReadingPractice.Domain.Repositories;
using SightReadingPractice.Domain.Entities;

namespace SightReadingPractice.Domain.Interactors
{
    public class FetchSheetSymbols
    {
        private static readonly string[] septimaRange = new string[] { "A", "B", "C", "D", "E", "F", "G" };
        private static readonly string[] keySignatureSigns = new string[] { "#", "b" };
        private static readonly int[] septimaAreas = new int[] { -1, 0, 1 };
        private static readonly int howMany = 4;

        private readonly INoteExerciseResultRepository _noteExerciseResultRepository;

        public FetchSheetSymbols(INoteExerciseResultRepository noteExerciseResultRepository)
        {
            _noteExerciseResultRepository = noteExerciseResultRepository;
        }

        public List<NoteExerciseResult> GetLatestFlawedEntry(ClefType clefType)
        {
            IQueryable<NoteExerciseResult> entries = _noteExerciseResultRepository.GetAllNoteExerciseResults()
                                                                                  .Where(e => e.ClefType == clefType && !e.Success)
                                                                                  .OrderBy(e => e.DateTime);

            if (!entries.Any()) return null;
            else return entries.Where(e => e.DateTime == entries.Last().DateTime).ToList();
        }

        public List<Note> GetRecentDifficulties(ClefType clefType)
        {
            List<NoteExerciseResult> flawedEntry = GetLatestFlawedEntry(clefType);

            if (flawedEntry == null) return null;
            else return flawedEntry.Select(exercise => new Note(exercise.ActualTone, exercise.SeptimaArea))
                                   .ToList();
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

        public static RepetitionAndExerciseNotes GenerateNotes(Random random, IEnumerable<Note> recentlyFailedNotes, ClefType clefType)
        {
            List<Note> repetitionSelection = new();
            List<Note> generatedNotes = new();

            if (recentlyFailedNotes != null)
            {
                repetitionSelection = recentlyFailedNotes.Shuffle(random).Take(random.Next(1, recentlyFailedNotes.Count())).ToList();
                generatedNotes = repetitionSelection.Select(n => new Note(n.Tone[0].ToString(), n.SeptimaArea)).ToList();
            }

            while (generatedNotes.Count < howMany)
            {
                string tone = septimaRange[random.Next(0, septimaRange.Length)];
                int septimaArea = septimaAreas[random.Next(0, septimaAreas.Length)];
                Note note = new(tone, septimaArea);

                if (!IsOutsideBound(septimaArea, tone, clefType) && !generatedNotes.Contains(note))
                {
                    generatedNotes.Add(note);
                }
            }

            return new RepetitionAndExerciseNotes(repetitionSelection, generatedNotes.Shuffle(random));
        }

        public static List<KeySignature> GenerateKeySignatures(Random random, List<string> repetitionSelection, List<string> teamTonesDistinct)
        {
            List<KeySignature> keySignatures = new();
            List<string> toneCandidates = teamTonesDistinct;

            if (repetitionSelection != null)
            {
                foreach (String repetitionTone in repetitionSelection.Where(repetitionTone => repetitionTone.Length == 2))
                {
                    keySignatures.Add(new KeySignature(repetitionTone[0].ToString(), repetitionTone[1].ToString()));
                }

                toneCandidates = teamTonesDistinct.Where(teamTone => !repetitionSelection.Any(repetitionTone => repetitionTone == teamTone)).ToList();
            }

            int quantityToAdd = random.Next(0, toneCandidates.Count - keySignatures.Count + 1);

            while (keySignatures.Count < quantityToAdd)
            {
                string tone = toneCandidates[random.Next(0, toneCandidates.Count)];
                string keySignatureSign = keySignatureSigns[random.Next(0, keySignatureSigns.Length)];

                keySignatures.Add(new KeySignature(tone, keySignatureSign));
                toneCandidates.Remove(tone);
            }

            return keySignatures;
        }

        public SheetSymbolsOutputModel CreateExercise(Random random, ClefType clefType)
        {
            List<Note> recentlyFailedNotes = GetRecentDifficulties(clefType);
            RepetitionAndExerciseNotes repetitionAndExerciseNotes = GenerateNotes(random, recentlyFailedNotes, clefType);

            List<string> teamTonesDistinct = repetitionAndExerciseNotes.GeneratedNotes.Select(n => n.Tone).Distinct().ToList();
            List<string> repetitionTonesToComplement = repetitionAndExerciseNotes.RepetitionSelection.Where(n => n.Tone.Length > 1)
                                                                                                   .Select(n => n.Tone)
                                                                                                   .Distinct().ToList();

            List<KeySignature> keySignatures = GenerateKeySignatures(random, repetitionTonesToComplement, teamTonesDistinct);

            return new SheetSymbolsOutputModel(keySignatures, repetitionAndExerciseNotes.GeneratedNotes);
        }
    }
}
