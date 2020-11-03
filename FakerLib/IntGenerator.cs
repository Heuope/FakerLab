using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class IntGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (typeof(int) == type);
        }

        public object Generate(Type targetType, IGeneratorContext generatorContext)
        {
            var rand = new Random();
            return rand.Next(1, 100);
        }
    }
}
