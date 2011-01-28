using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDW.Genetic
{
    public abstract class GeneticFunction
    {
        public int modifier;

        protected static int rndRange = 51;
        protected static int mutationRange = 3;
        protected static float mutationSeverity = .2f;

        public GeneticFunction()
        {
        }

        public static GeneticFunction GetGene()
        {
            GeneticFunction result = null;
            int r = GeneticPrimes.rnd.Next(7);
            switch (r)
            {
                case 0:
                    result = new AddFunction();
                    break;
                case 1:
                    result = new SubtractFunction();
                    break;
                case 2:
                    result = new MultiplyFunction();
                    break;
                case 3:
                    result = new DivideFunction();
                    break;
                case 4:
                    result = new OddFunction();
                    break;
                case 5:
                    result = new SquareRootFunction();
                    break;
                case 6:
                    result = new SquareFunction();
                    break;
            }
            result.modifier = GeneticPrimes.rnd.Next(rndRange);
            return result;
        }

        public abstract int Process(int input);
        public abstract GeneticFunction Clone();

        public void Mutate()
        {
            modifier += GeneticPrimes.rnd.Next((int)(-mutationSeverity * mutationRange), (int)(mutationSeverity * mutationRange));
        }

    }

    public class AddFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return modifier + input;
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new AddFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class SubtractFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return modifier - input;
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new SubtractFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class MultiplyFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return modifier * input;
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new MultiplyFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class DivideFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return (input != 0) ? (int)(modifier / input) : input;
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new DivideFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class OddFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return input % 2 == 0 ? input + 1 : input;
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new OddFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class PositiveFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return Math.Abs(input);
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new PositiveFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class SquareFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return input * input;
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new SquareFunction();
            result.modifier = modifier;
            return result;
        }
    }
    public class SquareRootFunction : GeneticFunction
    {
        public override int Process(int input)
        {
            return (int)Math.Sqrt(input);
        }
        public override GeneticFunction Clone()
        {
            GeneticFunction result = new SquareRootFunction();
            result.modifier = modifier;
            return result;
        }
    }
}
