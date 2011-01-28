using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDW.Genetic
{
    class GeneticPrimes
    {
        public static Random rnd = new Random(123456);
        //public static Random rnd = new Random((int)DateTime.Now.Ticks);
        static void Main(string[] args)
        {
            GeneticManager gm = new GeneticManager();
            gm.GeneratePopulation();
            while (true)
            {
                gm.RunGeneration();
            }
        }
    }
}
