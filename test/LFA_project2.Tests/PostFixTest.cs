using System;
using Xunit;
using LFA_project2;
using System.Linq;

namespace LFA_project2.Tests
{
    public class PostFixTest
    {
        [Theory()]
        [InlineData(new string[] { "A","B" },"A*.B*","A*B*.")]
        [InlineData(new string[]{"A","B","C"}, "(A.(C|B))*.B","ACB|.*B.")]
        public void ShouldConvertToPostFix(string[] operands, string regularExpression, string expected)
        {   
            Assert.Equal(expected, Conversion.ToPostFix(regularExpression, operands.ToList()));
        }
    }
}
