
namespace SightReadingPractice.Domain.Models
{
    public class AnswerResult
    {
        public Note Note { get; }
        public bool Success { get; }
        public AnswerResult(Note note, bool success)
        {
            Note = note;
            Success = success;
        }
    }
}
