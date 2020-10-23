using System;
using FakerLib;

namespace DoubleGeneratorLib
{
    public class DoubleGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (typeof(double) == type);
        }

        public object Generate(Type targetType, GeneratorContext generatorContext)
        {
            var rand = new Random();

            return (rand.NextDouble() * (1 - 0.1) + 0.1);
        }
    }
}
