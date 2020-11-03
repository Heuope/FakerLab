using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class StringGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (typeof(string) == type);
        }

        public object Generate(Type targetType, IGeneratorContext generatorContext)
        {
            var str = new StringBuilder();
            var rand = new Random();

            for (int i = 0; i < rand.Next(1, 50); i++)
            {
                str.Append((char)rand.Next(1, 255));
            }

            return str.ToString();
        }
    }
}
