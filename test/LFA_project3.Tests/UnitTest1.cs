using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace LFA_project3.Tests
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(AFDData.AFDs), MemberType = typeof(AFDData))]
        public void ShouldReturnAFDSteps(AFD afd,string[] variables, List<Closure> expected)
        {
            afd.BuildClosures(new Node(0, "q0"), variables);

            foreach (Closure c in expected)
            {
                int id = c.StateFrom.ID;

                string exp = string.Join(",", c.States.OrderBy(x => x.ID).Select(o => o.Value));

                string res = string.Join(",",afd.States.FirstOrDefault(o => o.StateFrom.ID == id).States.OrderBy(k => k.ID).Select(v => v.Value));

                Assert.Equal(exp, res);
            }
        }

        
    }
}
