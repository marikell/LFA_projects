using Xunit;

namespace LFA_project3.Tests
{
    public class ThompsonTest
    {
        [Theory()]
        [InlineData("A+BC|.A*.",
        new string[]{
            "FROM: Q4 // TO: Q1 // COST: A",
            "FROM: Q1 // TO: Q5 // COST: &",
            "FROM: Q6 // TO: Q2 // COST: A",
            "FROM: Q2 // TO: Q8 // COST: &",
            "FROM: Q8 // TO: Q3 // COST: &",
            "FROM: Q3 // TO: Q4 // COST: &",
            "FROM: Q5 // TO: Q7 // COST: &",
            "FROM: Q3 // TO: Q5 // COST: &",
            "FROM: Q1 // TO: Q4 // COST: &",
            "FROM: Q15 // TO: Q6 // COST: &",
            "FROM: Q7 // TO: Q17 // COST: &",
            "FROM: Q12 // TO: Q9 // COST: B",
            "FROM: Q9 // TO: Q14 // COST: &",
            "FROM: Q11 // TO: Q10 // COST: C",
            "FROM: Q10 // TO: Q14 // COST: &",
            "FROM: Q13 // TO: Q11 // COST: &",
            "FROM: Q13 // TO: Q12 // COST: &",
            "FROM: Q17 // TO: Q13 // COST: &",
            "FROM: Q14 // TO: Q16 // COST: &",
            "FROM: Q22 // TO: Q15 // COST: &",
            "FROM: Q16 // TO: Q24 // COST: &",
            "FROM: Q20 // TO: Q18 // COST: A",
            "FROM: Q18 // TO: Q21 // COST: &",
            "FROM: Q24 // TO: Q19 // COST: &",
            "FROM: Q19 // TO: Q20 // COST: &",
            "FROM: Q21 // TO: Q23 // COST: &",
            "FROM: Q19 // TO: Q21 // COST: &",
            "FROM: Q18 // TO: Q20 // COST: &",
            "FROM: - // TO: Q22 // COST: &",
            "FROM: Q23 // TO: - // COST: &",})]

        public void ShouldResolveThompsonGraph(string posfixedExp, string[] expectedOutput)
        {
            var t = new Thompson(posfixedExp);
            t.Resolve();
            Assert.Equal(expectedOutput, t.PrintGraph().ToArray());
        }
    }
}