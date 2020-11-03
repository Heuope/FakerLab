using System;
using System.Collections.Generic;
using FakerLib;
using Newtonsoft.Json;

namespace Faker
{
    class Program
    {        
        static void Main(string[] args)
        {
            var context = new GeneratorContext();

            context.AddNewGenerator(new IntGenerator());
            context.AddNewGenerator(new StringGenerator());
            context.AddNewGenerator(new ListGenerator());
            context.AddNewGenerator(new DateTimeGenerator());
            context.LoadNewGenerator(@"C:\Users\konst\Desktop\FakerLab\DoubleGeneratorLib\bin\Debug\netcoreapp3.1\DoubleGeneratorLib.dll");
            context.LoadNewGenerator(@"C:\Users\konst\Desktop\FakerLab\FloatGeneratorLib\bin\Debug\netcoreapp3.1\FloatGeneratorLib.dll");

            var faker = new FakerLib.Faker(context);

            List<int> zoo = faker.Create<List<int>>();
            Console.WriteLine(zoo.Count);
            Console.WriteLine(JsonConvert.SerializeObject(zoo, Formatting.Indented));
        }
    }
}
