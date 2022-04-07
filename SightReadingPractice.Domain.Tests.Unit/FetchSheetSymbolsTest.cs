using SightReadingPractice.Domain;
using SightReadingPractice.Domain.Interactors;
using SightReadingPractice.Domain.Entities;
using Xunit;
using SightReadingPractice.Domain.Tests.Unit.FakeRepositories;
using System.Collections.Generic;
using System;

namespace BassClefPractice.Domain.Tests.Unit
{
    public class Tests
    {
        [Fact]
        public void GetRecentDifficulties_FailedNotes_RelevantRepition()
        {
            //Arrange
            FakeNoteExerciseResultRepository exerciseResultRepository = new();
            DateTimeOffset dateTime = DateTimeOffset.Now;

            exerciseResultRepository.NoteExerciseResults.AddRange(new List<NoteExerciseResult>()
            {
                new NoteExerciseResult() { Id = 1,
                                           ActualTone = "A",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime.AddMinutes(-2),
                                           SeptimaArea = 0,
                                           Success = false },
                new NoteExerciseResult() { Id = 2,
                                           ActualTone = "B",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime.AddMinutes(-2),
                                           SeptimaArea = 0,
                                           Success = true },
                new NoteExerciseResult() { Id = 3,
                                           ActualTone = "C",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime.AddMinutes(-2),
                                           SeptimaArea = 1,
                                           Success = false },
                new NoteExerciseResult() { Id = 4,
                                           ActualTone = "D",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime.AddMinutes(-2),
                                           SeptimaArea = 0,
                                           Success = true },
            });

            exerciseResultRepository.NoteExerciseResults.AddRange(new List<NoteExerciseResult>() // Most recent entry.
            {
                new NoteExerciseResult() { Id = 5, 
                                           ActualTone = "A", 
                                           ClefType = ClefType.Bass, 
                                           DateTime = dateTime, 
                                           SeptimaArea = 0, 
                                           Success = true },
                new NoteExerciseResult() { Id = 6,
                                           ActualTone = "Bb",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime,
                                           SeptimaArea = -1,
                                           Success = false },
                new NoteExerciseResult() { Id = 7,
                                           ActualTone = "C",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime,
                                           SeptimaArea = 0,
                                           Success = true },
                new NoteExerciseResult() { Id = 8,
                                           ActualTone = "D",
                                           ClefType = ClefType.Bass,
                                           DateTime = dateTime,
                                           SeptimaArea = 0,
                                           Success = false },
            });

            FetchSheetSymbols fetchSheetSymbols = new(exerciseResultRepository);

            //Act
            List<Note> failedNotes = fetchSheetSymbols.GetRecentDifficulties(ClefType.Bass);

            //Assert
            Assert.NotEmpty(failedNotes);
            Assert.Equal(2, failedNotes.Count);

            Assert.Equal("Bb", failedNotes[0].Tone);
            Assert.Equal(-1, failedNotes[0].SeptimaArea);

            Assert.Equal("D", failedNotes[1].Tone);
            Assert.Equal(0, failedNotes[1].SeptimaArea);

        }

        [Theory]
        [InlineData("F", -1, ClefType.Bass)]
        [InlineData("G", -1, ClefType.Bass)]
        [InlineData("A", 1, ClefType.Bass)]
        [InlineData("B", 1, ClefType.Bass)]
        public void IsOutsideBound_OnlyInvalidNotes_UnivocallyTrue(string tone, int septimaArea, ClefType clefType)
        {
            //Act
            bool allOutsideScope = FetchSheetSymbols.IsOutsideBound(septimaArea, tone, clefType);

            //Assert
            Assert.True(allOutsideScope);
        }
    }
}