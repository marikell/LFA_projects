using LFA_project1.Validation;
using Xunit;

namespace LFA_project1.Tests
{
    public class ValidadorTest
    {
        #region ValidateUserInput

        [Theory()]
        [InlineData("SA",
         new string[] { "x", "y" },
         new string[] { },
         new string[] { },
        new int[] { 2, 5, 7, 3, 8, 10, 4, 12, 5, 13 },
        false)]

        [InlineData("KA",
         new string[] { "p", "k" },
         new string[] { "Ka" },
         new string[] { "Kp" },
        new int[] { },
        false)]

        [InlineData("Pp",
        new string[] { "x", "y" },
        new string[] {"Ka" },
        new string[] {"Kp" },
        new int[] { 1, 2 },
        false)]
        public void ValidateUserInput(string initialWord, string[] variables, string[] rulesBfA, string[] rulesAfA, int[] steps, bool expectedResult)
        {
            Assert.Equal(expectedResult, Validator.ValidateUserInput(initialWord, variables, rulesBfA, rulesAfA, steps));
        }
        #endregion

        #region ValidateGramma

        [Theory()]
        [InlineData(new string[] { "", "" },
        new string[] { "" },
        "",
        false)]
        [InlineData(new string[] { "P", "P" },
        new string[] { "X", "Y", "K" },
        "P",
        false)]
        [InlineData(new string[] { "P", "S", "T" },
        new string[] { "X", "Y", "K" },
        "P",
        true)]
        [InlineData(new string[] { "P", "S", "T" },
        new string[] { "X", "Y", "K" },
        "L",
        false)]
        public void ValidateGramma(string[] rulesBfA, string[] rulesAfA, string initialWord, bool expectedResult)
        {
            Assert.Equal(expectedResult, Validator.ValidateGramma(rulesBfA, rulesAfA, initialWord));
        }
        #endregion
    }
}