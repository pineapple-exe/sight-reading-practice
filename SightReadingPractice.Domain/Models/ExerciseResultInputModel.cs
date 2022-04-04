using System;
using System.Collections.Generic;

namespace SightReadingPractice.Domain.Models
{
    public class ExerciseResultInputModel
    {
        public ClefType ClefType { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public List<AnswerResult> ExerciseResult { get; set; }
    }
}
