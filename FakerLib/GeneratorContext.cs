using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FakerLib
{
    public class GeneratorContext : IGeneratorContext
    {
        private List<IGenerator> _generators = new List<IGenerator>();

        public object Generate(Type t)
        {
            foreach (var generator in _generators)
            {
                if (generator.CanGenerate(t))
                {
                    return generator.Generate(t, this);
                }
            }
            return null;
        }

        public void LoadNewGenerator(string pathToDll)
        {
            Assembly assembly = Assembly.LoadFrom(pathToDll);
            foreach (var type in assembly.GetTypes())
            {
                var temp = Activator.CreateInstance(type);
                if (temp is IGenerator)
                {
                    _generators.Add((IGenerator)temp);
                }
            }
        }

        public void AddNewGenerator(IGenerator generator)
        {
            _generators.Add(generator);
        }
    }
}
