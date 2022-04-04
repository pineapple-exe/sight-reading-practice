using SightReadingPractice.Domain.Entities;
using SightReadingPractice.Domain.Repositories;
using System.Collections.Generic;

namespace SightReadingPractice.Data.Repositories
{
    public class NoteExerciseResultRepository : INoteExerciseResultRepository
    {
        private readonly SightReadingPracticeDbContext _context;

        public NoteExerciseResultRepository(SightReadingPracticeDbContext context)
        {
            _context = context;
        }

        public void AddNoteExerciseResults(IEnumerable<NoteExerciseResult> noteExerciseResult)
        {
            _context.AddRange(noteExerciseResult);
            _context.SaveChanges();
        }
    }
}
