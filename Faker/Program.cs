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

            var faker = new FakerLib.Faker(context);

            Zoo t = faker.Create<Zoo>();

            Console.WriteLine(JsonConvert.SerializeObject(t));
        }
    }
}
