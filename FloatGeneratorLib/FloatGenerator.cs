using System;
using FakerLib;

namespace FloatGeneratorLib
{
    public class FloatGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (typeof(float) == type);
        }

        public object Generate(Type targetType, IGeneratorContext generatorContext)
        {
            var rand = new Random();

            return (float)(rand.NextDouble() * (1 - 0.1) + 0.1);
        }
    }
}
