using Microsoft.AspNetCore.Mvc;
using SightReadingPractice.Domain;
using SightReadingPractice.Domain.Models;
using System;

namespace BassClefPractice.WebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightReadingPracticeController : ControllerBase
    {
        [HttpGet("sheetSymbols")]
        public SheetSymbolsOutputModel GetSheetSymbols(ClefType clefType)
        {
            return FetchSheetSymbols.CreateExercise(new Random(), clefType);
        }

        //[HttpGet("sheetSymbolsTest")]
        //public SheetSymbolsOutputModel GetSheetSymbolsTest()
        //{
        //    return FetchSheetSymbols.CreateExerciseTest();
        //}
    }
}
