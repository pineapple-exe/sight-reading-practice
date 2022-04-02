using SightReadingPractice.Domain;
using Xunit;

namespace BassClefPractice.Domain.Tests.Unit
{
    public class Tests
    {
        [Theory]
        [InlineData("f", -1, (ClefType)0)]
        [InlineData("g", -1, (ClefType)0)]
        [InlineData("a", 1, (ClefType)0)]
        [InlineData("b", 1, (ClefType)0)]
        public void IsOutsideBound_OnlyInvalidNotes_UnivocallyTrue(string tone, int septimaArea, ClefType clefType)
        {
            //Act
            bool allOutsideScope = FetchSheetSymbols.IsOutsideBound(septimaArea, tone, clefType);

            //Assert
            Assert.True(allOutsideScope);
        }
    }
}