using SightReadingPractice.Domain.Entities;
using System.Collections.Generic;

namespace SightReadingPractice.Domain.Repositories
{
    public interface INoteExerciseResultRepository
    {
        void AddNoteExerciseResults(IEnumerable<NoteExerciseResult> noteExerciseResult);
    }
}
