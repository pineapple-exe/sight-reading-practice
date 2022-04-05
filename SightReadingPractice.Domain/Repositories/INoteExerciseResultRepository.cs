using SightReadingPractice.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SightReadingPractice.Domain.Repositories
{
    public interface INoteExerciseResultRepository
    {
        void AddNoteExerciseResults(IEnumerable<NoteExerciseResult> noteExerciseResult);
        IQueryable<NoteExerciseResult> GetAllNoteExerciseResults();
    }
}
