using System;

namespace SightReadingPractice.Domain.Entities
{
    public class NoteExerciseResult
    {
        public int Id { get; set; }
        public ClefType ClefType { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string ActualTone { get; set; }
        public int SeptimaArea { get; set; }
        public bool Success { get; set; }
    }
}
