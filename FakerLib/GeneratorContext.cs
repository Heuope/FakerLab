using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class GeneratorContext
    {
        private delegate object Generator();
        
        public Dictionary<Type, IGenerator> generatros = new Dictionary<Type, IGenerator>();

        public GeneratorContext()
        {
            generatros.Add(typeof(int), new IntGenerator());
            generatros.Add(typeof(string), new StringGenerator());
            generatros.Add(typeof(float), new FloatGenerator());
        }

        public void AddNewGenerator()
        {

        }
    }
}
