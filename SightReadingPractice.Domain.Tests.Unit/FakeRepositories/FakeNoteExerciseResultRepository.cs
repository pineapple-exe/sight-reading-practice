
using SightReadingPractice.Domain.Entities;
using SightReadingPractice.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SightReadingPractice.Domain.Tests.Unit.FakeRepositories
{
    internal class FakeNoteExerciseResultRepository : INoteExerciseResultRepository
    {
        internal List<NoteExerciseResult> NoteExerciseResults = new();

        public void AddNoteExerciseResults(IEnumerable<NoteExerciseResult> noteExerciseResult)
        {
            NoteExerciseResults.AddRange(noteExerciseResult);
        }

        public IQueryable<NoteExerciseResult> GetAllNoteExerciseResults()
        {
            return NoteExerciseResults.AsQueryable();
        }
    }
}
