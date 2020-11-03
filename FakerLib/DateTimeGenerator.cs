using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class DateTimeGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (typeof(DateTime) == type);
        }

        public object Generate(Type targetType, IGeneratorContext generatorContext)
        {
            var rand = new Random();
            return new DateTime(rand.Next(1950, 2021), rand.Next(1, 13), rand.Next(1, 32), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));
        }
    }
}
