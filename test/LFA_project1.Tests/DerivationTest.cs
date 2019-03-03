using System;
using LFA_Project1;
using Xunit;

namespace LFA_project1.Tests
{   
    
    public class DerivationTest
    {
        [Fact]
        public void Derive_01()
        {
            string[] ba = new string[]{"S","X","X","X","Aa","Ab","AY","Ba","Bb","BY","Fa","Fb","FY"};
            string[] aa = new string[]{"XY","XaA","XbB","F","aA","bA","Ya","aB","bB","Yb","aF","bF",""};

            int[] steps = new int[]{1,2,7,3,8,10,4,12,11,13};

            string initial = "S";

            string[] variables = new string[]{"a", "b"};

            Derivation derivation = new Derivation();

            derivation.Init(ba,aa,steps,variables,initial);

            Assert.Equal("baba", derivation.Derive());
        }
    }
}