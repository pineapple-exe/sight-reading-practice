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

        private static Note[] GenerateNotes(Random random, List<Note> recentlyFailedNotes, ClefType clefType)
        {
            List<Note> notes = new();

            if (recentlyFailedNotes != null)
            { 
                int howMuchRepititionToRemove = random.Next(0, recentlyFailedNotes.Count); //Repeat at least one failed note.

                for (int i = 0; i < howMuchRepititionToRemove; i++)
                {
                    recentlyFailedNotes.RemoveAt(random.Next(0, recentlyFailedNotes.Count));
                }

                notes = recentlyFailedNotes.Select(fn => new Note(fn.Tone[0].ToString(), fn.SeptimaArea)).ToList();
            }

            while (notes.Count < howMany)
            {
                string tone = septimaRange[random.Next(0, septimaRange.Length)];
                int septimaArea = septimaAreas[random.Next(0, septimaAreas.Length)];
                Note note = new(tone, septimaArea);

                if (!IsOutsideBound(septimaArea, tone, clefType) && !notes.Contains(note))
                {
                    notes.Add(note);
                }
            }

            return notes.ToArray();
        }

        private static KeySignature[] GenerateKeySignatures(Random random, List<Note> recentlyFailedNotes, List<string> teamTonesDistinct)
        {
            List<KeySignature> keySignatures = new();
            List<string> toneCandidates = teamTonesDistinct;

            if (recentlyFailedNotes != null)
            {
                foreach (Note fn in recentlyFailedNotes.Where(fn => fn.Tone.Length == 2))
                {
                    keySignatures.Add(new KeySignature(fn.Tone[0].ToString(), fn.Tone[1].ToString()));
                }

                toneCandidates = teamTonesDistinct.Where(t => !recentlyFailedNotes.Any(fn => fn.Tone == t)).ToList();
            }

            int quantityToAdd = random.Next(0, toneCandidates.Count - keySignatures.Count + 1);

            while (keySignatures.Count < quantityToAdd)
            {
                string tone = toneCandidates[random.Next(0, toneCandidates.Count)];
                string keySignatureSign = keySignatureSigns[random.Next(0, keySignatureSigns.Length)];

                keySignatures.Add(new KeySignature(tone, keySignatureSign));
                toneCandidates.Remove(tone);
            }

            return keySignatures.ToArray();
        }

        public SheetSymbolsOutputModel CreateExercise(Random random, ClefType clefType)
        {
            List<Note> recentlyFailedNotes = GetRecentDifficulties(clefType);
            Note[] notes = GenerateNotes(random, recentlyFailedNotes, clefType);

            List<string> teamTonesDistinct = notes.Select(n => n.Tone).Distinct().ToList();

            KeySignature[] keySignatures = GenerateKeySignatures(random, recentlyFailedNotes, teamTonesDistinct);

            return new SheetSymbolsOutputModel(keySignatures, notes);
        }
    }
}
