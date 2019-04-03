using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace LFA_project3.Tests
{
    public class AFDTest
    {
        [Theory]
        [MemberData(nameof(AFDData.AFDs), MemberType = typeof(AFDData))]
        public void ShouldGenerateAFDSteps(AFD afd, List<Closure> expected, List<Closure> expectedTable)
        {
            afd.GenerateAFDSteps(new Node(0, "q0"));

            //Testa os estados gerados
            foreach (Closure closure in expected)
            {
                Assert.Equal(closure.ToString(), afd.States.FirstOrDefault(o => o.StateFrom.ID == closure.StateFrom.ID).ToString());
            }

            //Testa todos os passos gerados
            foreach (var grp in expectedTable.GroupBy(o => o.StateFrom.ID).ToList())
            {
                //para cada id
                foreach (Closure cl in grp.ToList())
                {
                    Assert.Equal(cl.ToString(), afd.GetClosureTable()
                        .FirstOrDefault(o => o.StateFrom.ID == cl.StateFrom.ID && o.Character == cl.Character)
                        .ToString());
                }
            }

        }

        [Fact]
        public void ShouldGetInitialClosure()
        {
            throw new Exception("Test not implemented!");
        }

        [Fact]
        public void ShouldFindClosureByState()
        {
            throw new Exception("Test not implemented!");
        }

        [Fact]
        public void ShouldGetState()
        {
            throw new Exception("Test not implemented!");
        }



    }
}
