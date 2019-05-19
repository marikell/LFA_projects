using Xunit;
using System.Linq;
using LFA_project4.Utils;

namespace LFA_project4.Tests
{
    public class PostFixTest
    {
        [Theory()]
        [InlineData(new string[] { "A", "B" }, "A*.B*", "A*B*.")]
        [InlineData(new string[] { "A", "B" }, "A*B*", "AB**")]
        [InlineData(new string[] { "A", "B", "C" }, "(A.(C|B))*.B", "ACB|.*B.")]
        public void ShouldConvertToPostFix(string[] operands, string regularExpression, string expected)
        {
            Assert.Equal(expected, ConversionUtils.ToPostFix(regularExpression, operands.ToList()));
        }
    }
}