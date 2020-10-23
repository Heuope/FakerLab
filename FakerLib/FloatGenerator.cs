using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    class FloatGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (typeof(float) == type);
        }

        public object Generate(Type targetType, GeneratorContext generatorContext)
        {
            var rand = new Random();

            return (float)(rand.NextDouble() * (1 - 0.1) + 0.1);
        }
    }
}
