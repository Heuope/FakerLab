using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class ListGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        public object Generate(Type targetType, GeneratorContext generatorContext)
        {
            var result = Activator.CreateInstance(targetType);
            var rand = new Random();
            int elements = rand.Next(1, 5);

            var listType = targetType.GetGenericArguments()[0];
            var addMethod = targetType.GetMethod("Add");

            for (int i = 0; i < elements; i++)
            {
                addMethod.Invoke(result, new object[] { generatorContext.Generate(listType) });
            }

            return result;
        }
    }
}
