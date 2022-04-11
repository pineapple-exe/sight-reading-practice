using System;
using System.Collections.Generic;

namespace SightReadingPractice.Domain.Interactors
{
    public class RepetitionAndExerciseNotes
    {
        public List<Note> RepetitionSelection { get; }
        public List<Note> GeneratedNotes { get; }

        public RepetitionAndExerciseNotes(List<Note> repitionSelection, List<Note> generatedNotes)
        {
            RepetitionSelection = repitionSelection;
            GeneratedNotes = generatedNotes;
        }
    }
}
