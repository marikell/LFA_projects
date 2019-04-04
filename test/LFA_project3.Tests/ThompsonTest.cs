using Xunit;

namespace LFA_project3.Tests
{
    public class ThompsonTest
    {
        [Theory()]
        [InlineData("A+BC|.A*.",
        new string[]{
            "FROM: 4 // TO: 1 // COST: A",
            "FROM: 1 // TO: 5 // COST: &",
            "FROM: 6 // TO: 2 // COST: A",
            "FROM: 2 // TO: 8 // COST: &",
            "FROM: 8 // TO: 3 // COST: &",
            "FROM: 3 // TO: 4 // COST: &",
            "FROM: 5 // TO: 7 // COST: &",
            "FROM: 3 // TO: 5 // COST: &",
            "FROM: 1 // TO: 4 // COST: &",
            "FROM: 15 // TO: 6 // COST: &",
            "FROM: 7 // TO: 17 // COST: &",
            "FROM: 12 // TO: 9 // COST: B",
            "FROM: 9 // TO: 14 // COST: &",
            "FROM: 11 // TO: 10 // COST: C",
            "FROM: 10 // TO: 14 // COST: &",
            "FROM: 13 // TO: 11 // COST: &",
            "FROM: 13 // TO: 12 // COST: &",
            "FROM: 17 // TO: 13 // COST: &",
            "FROM: 14 // TO: 16 // COST: &",
            "FROM: 22 // TO: 15 // COST: &",
            "FROM: 16 // TO: 24 // COST: &",
            "FROM: 20 // TO: 18 // COST: A",
            "FROM: 18 // TO: 21 // COST: &",
            "FROM: 24 // TO: 19 // COST: &",
            "FROM: 19 // TO: 20 // COST: &",
            "FROM: 21 // TO: 23 // COST: &",
            "FROM: 19 // TO: 21 // COST: &",
            "FROM: 18 // TO: 20 // COST: &",
            "FROM: - // TO: 22 // COST: &",
            "FROM: 23 // TO: - // COST: &",})]

        public void ShouldResolveThompsonGraph(string posfixedExp, string[] expectedOutput)
        {
            var t = new Thompson(posfixedExp);
            t.Resolve();
            Assert.Equal(expectedOutput, t.PrintGraph().ToArray());
        }
    }
}