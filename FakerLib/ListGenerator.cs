﻿using System;
using System.Collections;
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

        public object Generate(Type targetType, IGeneratorContext generatorContext)
        {
            var result = (IList)Activator.CreateInstance(targetType);
            var rand = new Random();
            int elements = rand.Next(1, 5);

            var listType = targetType.GetGenericArguments()[0];
            //var addMethod = targetType.GetMethod("Add");

            for (int i = 0; i < elements; i++)
            {
                var faker = new Faker(generatorContext);
                result.Add(faker.Create(listType));
                //addMethod.Invoke(result, new object[] { faker.Create(listType) });
            }

            return result;
        }
    }
}
