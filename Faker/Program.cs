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
            context.LoadNewGenerator(@"C:\Users\konst_9hggwum\OneDrive\Desktop\FakerLab\FloatGeneratorLib\bin\Debug\netcoreapp3.1\FloatGeneratorLib.dll");
            context.LoadNewGenerator(@"C:\Users\konst_9hggwum\OneDrive\Desktop\FakerLab\DoubleGeneratorLib\bin\Debug\netcoreapp3.1\DoubleGeneratorLib.dll");

            var faker = new FakerLib.Faker(context);

            List<Zoo> zoo = faker.Create<List<Zoo>>();

            Console.WriteLine(JsonConvert.SerializeObject(zoo, Formatting.Indented));
        }
    }
}
