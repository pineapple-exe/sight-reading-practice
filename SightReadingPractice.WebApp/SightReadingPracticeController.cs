using Microsoft.AspNetCore.Mvc;
using SightReadingPractice.Domain;
using SightReadingPractice.Domain.Interactors;
using SightReadingPractice.Domain.Models;
using System;

namespace BassClefPractice.WebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightReadingPracticeController : ControllerBase
    {
        private readonly NoteExerciseInteractor _noteExerciseInteractor;

        public SightReadingPracticeController(NoteExerciseInteractor noteExerciseInteractor)
        {
            _noteExerciseInteractor = noteExerciseInteractor;
        }

        [HttpGet("sheetSymbols")]
        public SheetSymbolsOutputModel GetSheetSymbols(ClefType clefType)
        {
            return FetchSheetSymbols.CreateExercise(new Random(), clefType);
        }

        [HttpPost("exerciseResult")]
        public IActionResult AddExerciseResult(ExerciseResultInputModel exerciseResult)
        {
            _noteExerciseInteractor.AddExerciseResult(exerciseResult);

            return Ok();
        }

        //[HttpGet("sheetSymbolsTest")]
        //public SheetSymbolsOutputModel GetSheetSymbolsTest()
        //{
        //    return FetchSheetSymbols.CreateExerciseTest();
        //}
    }
}
