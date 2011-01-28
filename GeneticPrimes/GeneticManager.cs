using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDW.Genetic
{
    public class GeneticManager
    {
        public List<Chromosome> population = new List<Chromosome>();
        public float mutationFrequency = .05f;

        int bestScoreEver;
        Chromosome bestEver;
        private int populationCount = 10;
        private int[] ranks;

        public void GeneratePopulation()
        {
            for (int i = 0; i < populationCount; i++)
            {
                Chromosome c = new Chromosome();
                c.PopulateGenes();
                population.Add(c);
            }
            bestEver = population[0];
            ranks = new int[populationCount];
        }

        public void RunGeneration()
        {
            RankPopulation();
            TraceRanks();
            BreedPopulation();
            MutatePopulation();
        }

        int[] average = new int[100];
        int avIndex;
        private void TraceRanks()
        {
            average[avIndex++] = ranks.Sum();
            if (avIndex >= average.Length)
            {
                avIndex = 0;
                Console.Write((average.Sum() / average.Length) + "\t");
            }
            //for (int i = 0; i < ranks.Length; i++)
            //{
            //    Console.Write(ranks[i] + "\t");
            //}
            //Console.WriteLine("");
        }
        public void RankPopulation()
        {
            int lowest = int.MaxValue;
            int lowestIndex = 0;
            for (int i = 0; i < populationCount; i++)
            {
                ranks[i] = population[i].GetRank();
                if (ranks[i] >= bestScoreEver)
                {
                    bestScoreEver = ranks[i];
                    bestEver = population[i];
                }
                else if(ranks[i] < lowest)
                {
                    lowest = ranks[i];
                    lowestIndex = i;
                }
            }

            population[lowestIndex] = bestEver.Clone();
            population[lowestIndex].Mutate();
        }

        public void BreedPopulation()
        {
            Chromosome[] newPopulation = new Chromosome[population.Count];
            int[] rwa = GetRankWeightedArray();
            int len = rwa.Length;
            int index = 0;
            for (int i = 0; i < population.Count; i++)
            {
                index = (index < rwa.Length - 2) ? index : index % 2;
                int index0 = rwa[index++];
                int index1 = rwa[index++];
                while (index1 == index0)
                {
                    index1 = rwa[index++ % len];
                }
                //newPopulation[i] = population[index0].RandomMerge(population[index1]);
                newPopulation[i] = population[index0].RouletteMerge(population[index1]);                
            }
            population = new List<Chromosome>(newPopulation);
        }

        protected int[] GetRankWeightedArray()
        {
            int total = ranks.Sum();
            int[] lives = new int[total];
            int index = 0;
            for (int i = 0; i < ranks.Length; i++)
            {
                if (ranks[i] == 1)
                {
                    //population[i].Mutate();
                }

                for (int j = 0; j < ranks[i]; j++)
                {
                    lives[index++] = i;
                }
            }
            Shuffle(lives);
            return lives;
        }

        protected void Shuffle(int[] ar)
        {
            int len = ar.Length;
            for (int i = 0; i < len * 4; i++)
            {
                int index0 = GeneticPrimes.rnd.Next(len);
                int index1 = GeneticPrimes.rnd.Next(len);
                int temp = ar[index0];
                ar[index0] = ar[index1];
                ar[index1] = temp;
            }
        }

        public void MutatePopulation()
        {
            for (int i = 0; i < population.Count; i++)
            {
                if (GeneticPrimes.rnd.Next(100) < mutationFrequency * 100)
                {
                    //population[i] = new Chromosome();
                    //population[i].PopulateGenes();
                    population[i].Mutate();
                }
            }
        }
    }
}
