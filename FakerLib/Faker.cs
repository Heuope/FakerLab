using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakerLib
{
    public class Faker
    {
        private GeneratorContext _context;

        public Faker(GeneratorContext generatorContext)
        {
            _context = generatorContext;
        }

        public T Create<T>()
        {
           return (T)Create(typeof(T));
        }

        private object Create(Type t)
        {
            if (_context.Generate(t) != null)
            {
                return _context.Generate(t);
            }

            var constructorInfos = t.GetConstructors();          
            var propertyInfos = t.GetProperties();
            var fieldInfos = t.GetFields();

            constructorInfos.OrderByDescending(x => x.GetParameters().Count());

            object obj = CreateWithConstructor(constructorInfos, t);

            return obj;
        }

        private object CreateWithConstructor(ConstructorInfo[] constructorInfos, Type t)
        {
            var paramList = new List<object>();
            var obj = new object();

            foreach (var constructor in constructorInfos)
            {                
                foreach (var param in constructor.GetParameters())
                {
                    var faker = new Faker(_context);
                    var parametr = faker.Create(param.ParameterType);
                    paramList.Add(parametr);
                }

                try
                {
                    obj = Activator.CreateInstance(t, paramList.ToArray());
                    break;
                }
                catch (Exception)
                {
                    paramList.Clear();
                }
            }

            return obj;
        }
        
        private object[] CreateFields(FieldInfo[] fieldInfos, object obj)
        {
            var fieldsList = new List<object>();
            var faker = new Faker(_context);

            foreach (var property in fieldInfos)
            {
                fieldsList.Add(faker.Create(property.FieldType));
            }
            return fieldsList.ToArray();
        }

        private object[] CreateProperty(PropertyInfo[] propertyInfos)
        {
            var fieldsList = new List<object>();
            var faker = new Faker(_context);

            foreach (var property in propertyInfos)
            {
                fieldsList.Add(faker.Create(property.PropertyType));
            }
            return fieldsList.ToArray();
        }
    }
}
