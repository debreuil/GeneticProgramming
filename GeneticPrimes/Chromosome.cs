using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDW.Genetic
{
    public class Chromosome
    {
        public int score;
        public int maxLength = 7;
        List<GeneticFunction> genes = new List<GeneticFunction>();
        static int testIterations = 10;
        static int[] seeds;

        public int chances = 0;

        public Chromosome()
        {
        }
        static Chromosome()
        {
            GenerateSeeds();
        }

        public static void GenerateSeeds()
        {
            seeds = new int[testIterations];
            for (int i = 0; i < testIterations; i++)
            {
                seeds[i] = GeneticPrimes.rnd.Next(5000);
            }
        }
        public void PopulateGenes()
        {
            for (int i = 0; i < maxLength; i++)
            {
                genes.Add(GeneticFunction.GetGene());
            }
        }
        public int GetRank()
        {
            //GenerateSeeds();
            GeneticPrimes.rnd = new Random((int)DateTime.Now.Ticks);
            int[] tests = new int[testIterations];

            for (int i = 0; i < testIterations; i++)
            {
                tests[i] = GetResult(seeds[i]);
            }

            score = FitnessCloseTo4000(tests);
            return score;
        }

        public int FitnesOdd(int[] tests)
        {
            int[] uniqueTests = tests.Distinct().ToArray();
            int result = (int)(uniqueTests.Length / 3) + 1;
            for (int i = 0; i < uniqueTests.Length; i++)
            {
                if (uniqueTests[i] % 2 == 1)
                {
                    result += 5;
                }
            }
            return result;
        }
        public int FitnessCloseTo4000(int[] tests)
        {
            int[] uniqueTests = tests.Distinct().ToArray();
            int result = 2;// (int)(uniqueTests.Length);
            if (uniqueTests.Length > 5)
            {
                int min = 1000;
                int max = 1500;
                float dif = max - min;
                int weight = 5;
                for (int i = 0; i < uniqueTests.Length; i++)
                {
                    if (uniqueTests[i] > min && uniqueTests[i] < max)
                    {
                        result += (int)((uniqueTests[i] - min) / dif * weight);
                        if (uniqueTests[i] % 10 == 3)
                        {
                            result = 4;
                        }
                    }
                }
            }
            return result;
        }

        public int GetResult(int startValue)
        {
            int result = startValue;
            for (int i = 0; i < genes.Count; i++)
            {
                result = genes[i].Process(result);
            }
            return result;
        }

        public Chromosome RouletteMerge(Chromosome ch)
        {
            Chromosome result = new Chromosome();

            int splitPoint = GeneticPrimes.rnd.Next(genes.Count);
            for (int i = 0; i < genes.Count; i++)
            {
                GeneticFunction gene = i < splitPoint ? genes[i].Clone() : ch.genes[i].Clone();
                result.genes.Add(gene);
            }

            return result;
        }
        public Chromosome RandomMerge(Chromosome ch)
        {
            Chromosome result = new Chromosome();
            for (int i = 0; i < genes.Count; i++)
            {
                GeneticFunction gene = GeneticPrimes.rnd.Next(100) < 50 ? genes[i].Clone() : ch.genes[i].Clone();
                result.genes.Add(gene);
            }

            return result;
        }
        public void Mutate()
        {
            int mutationPoint = GeneticPrimes.rnd.Next(genes.Count);
            genes[mutationPoint] = GeneticFunction.GetGene();
            //genes[mutationPoint].Mutate();
        }
        public Chromosome Clone()
        {
            Chromosome result = new Chromosome();
            for (int i = 0; i < genes.Count; i++)
            {
                result.genes.Add(genes[i].Clone());
            }
            return result;
        }
    }
}
