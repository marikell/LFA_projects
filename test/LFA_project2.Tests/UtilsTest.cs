using LFA_project2.Config;
using LFA_project2.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LFA_project2.Tests
{
    public class UtilsTest
    {
        [Theory]
        [InlineData("", new string[] { "a", "b" }, false)]
        [InlineData("a*b", new string[] { "a", "b" }, true)]
        [InlineData("a*b", new string[] { }, false)]
        public void ShouldValidateEmptyStrings(string regularExpression, string[] operands, bool expected)
        {
            InputUtils utils = new InputUtils(new Input(operands, regularExpression));

            Assert.Equal(expected, utils.ValidateEmptyStrings());
        }

        [Theory]
        [InlineData("(a+b)*", new string[] { "a", "b" }, true)]
        [InlineData("(a+b*", new string[] { "a", "b" }, false)]
        [InlineData("(a+b}*", new string[] { "a", "b" }, false)]
        [InlineData("([a+b])}*", new string[] { "a", "b" }, false)]

        public void ShouldValidateBrackets(string regularExpression, string[] operands, bool expected)
        {
            InputUtils utils = new InputUtils(new Input(operands, regularExpression));

            Assert.Equal(expected, utils.ValidateBrackets());
        }

        [Theory]
        [InlineData("(ab*k)|l*", new string[] { "ab", "k","l" }, false)]
        [InlineData("(a*k)|l*", new string[] { "a", "k", "l" }, true)]
        public void ShouldValidateOperandsWithOnlyCharacter(string regularExpression, string[] operands, bool expected)
        {
            InputUtils utils = new InputUtils(new Input(operands, regularExpression));
            Assert.Equal(expected, utils.ValidateCharacterOperands());
        }

        [Theory]
        [InlineData("(a + b ) *", new string[] { "a", "b" }, "(a+b)*")]
        public void ShouldRemoveEmptySpaces(string regularExpression, string[] operands, string expected)
        {
            InputUtils utils = new InputUtils(new Input(operands, regularExpression));

            Assert.Equal(expected, utils.RemoveEmptySpaces());
        }

        [Theory]
        [InlineData("ab*", new string[] { "a","b" }, true)]
        [InlineData("abc*c|a", new string[] { "a", "b","c" }, true)]
        public void ShouldValidateRegex(string regularExpression, string[] operands, bool expected)
        {
            InputUtils utils = new InputUtils(new Input(operands, regularExpression));

            Assert.Equal(expected, utils.ValidateRegex(utils.GetOperandsOccurrences()));            
        }

        [Theory]
        [InlineData("(ab*bc)*a|ab", new string[] { "a", "b" },  new string[] {"ab","bc","a","ab" })]
        public void ShouldGetOperandsOccurrences(string regularExpression, string[] operands, string[] expected)
        {
            InputUtils utils = new InputUtils(new Input(operands, regularExpression));
            Assert.Equal(utils.GetOperandsOccurrences(), expected);
        }

    }
}
