using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class GeneratorContext
    {
        private List<IGenerator> generators = new List<IGenerator>();

        public object Generate(Type t)
        {
            foreach (var generator in generators)
            {
                if (generator.CanGenerate(t))
                {
                    return generator.Generate(t, this);
                }
            }
            return null;
        }

        public GeneratorContext()
        {
            generators.Add(new IntGenerator());
            generators.Add(new StringGenerator());
            generators.Add(new FloatGenerator());
            generators.Add(new ListGenerator());
        }

        public void AddNewGenerator(IGenerator generator)
        {
            generators.Add(generator);
        }
    }
}
