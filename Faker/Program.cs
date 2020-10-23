using System;
using System.Collections.Generic;
using FakerLib;

namespace Faker
{
    class Program
    {        
        static void Main(string[] args)
        {
            var context = new GeneratorContext();

            var faker = new FakerLib.Faker(context);

            Bar t = faker.Create<Bar>();

            Console.WriteLine(t.a);
            Console.WriteLine(t.b);
            Console.WriteLine(t.foo.c);
            foreach (var item in t.e)
            {
                foreach (var s in item)
                {
                    Console.Write($"{s} ");
                }
            }
        }
    }
}
