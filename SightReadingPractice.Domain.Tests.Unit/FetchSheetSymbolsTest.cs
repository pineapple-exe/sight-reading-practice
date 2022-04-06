using SightReadingPractice.Domain;
using SightReadingPractice.Domain.Interactors;
using Xunit;

namespace BassClefPractice.Domain.Tests.Unit
{
    public class Tests
    {
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