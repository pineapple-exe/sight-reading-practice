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
        private readonly FetchSheetSymbols _fetchSheetSymbols;
        private readonly NoteExerciseInteractor _noteExerciseInteractor;

        public SightReadingPracticeController(NoteExerciseInteractor noteExerciseInteractor, FetchSheetSymbols fetchSheetSymbols)
        {
            _fetchSheetSymbols = fetchSheetSymbols;
            _noteExerciseInteractor = noteExerciseInteractor;
        }

        [HttpGet("sheetSymbols")]
        public SheetSymbolsOutputModel GetSheetSymbols(ClefType clefType)
        {
            return _fetchSheetSymbols.CreateExercise(new Random(), clefType);
        }

        [HttpPost("exerciseResult")]
        public IActionResult AddExerciseResult(ExerciseResultInputModel exerciseResult)
        {
            _noteExerciseInteractor.AddExerciseResult(exerciseResult);

            return Ok();
        }
    }
}
