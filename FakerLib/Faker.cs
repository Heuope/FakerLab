using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakerLib
{
    public class Faker
    {
        private GeneratorContext _context;
        private Stack<Type> _types;

        public Faker(GeneratorContext generatorContext)
        {
            _context = generatorContext;
            _types = new Stack<Type>();
        }

        private Faker(GeneratorContext generatorContext, Stack<Type> types)
        {
            _context = generatorContext;
            _types = types;
        }

        public T Create<T>()
        {
           return (T)Create(typeof(T));
        }

        public object Create(Type t)
        {
            if (_types.Contains(t))
                return null;

            _types.Push(t);

            if (_context.Generate(t) != null)
            {
                return _context.Generate(t);
            }

            var constructorInfos = t.GetConstructors();          
            var propertyInfos = t.GetProperties();
            var fieldInfos = t.GetFields();

            constructorInfos = constructorInfos.OrderByDescending(x => x.GetParameters().Count()).ToArray();

            var paramList = new List<object>();
            var obj = new object();

            var faker = new Faker(_context, _types);

            foreach (var constructor in constructorInfos)
            {
                foreach (var param in constructor.GetParameters())
                {
                    paramList.Add(faker.Create(param.ParameterType));
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
            if (obj == null)
            {
                try
                {
                    obj = Activator.CreateInstance(t);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            foreach (var field in fieldInfos)
            {
                if (_context.Generate(field.FieldType) == null)
                {
                    if (_types.Contains(field.FieldType))
                    {
                        continue;
                    }
                    try
                    {
                        field.SetValue(obj, faker.Create(field.FieldType));
                    }
                    catch (Exception)
                    {
                        field.SetValue(obj, default);
                    }
                }
                else
                {
                    try
                    {
                        field.SetValue(obj, _context.Generate(field.FieldType));
                    }
                    catch (Exception)
                    {
                        field.SetValue(obj, default);
                    }
                }
            }
            foreach (var property in propertyInfos)
            {
                if (_context.Generate(property.PropertyType) == null)
                {
                    if (_types.Contains(property.PropertyType))
                    {
                        continue;
                    }
                    try
                    {
                        property.SetValue(obj, faker.Create(property.PropertyType));
                    }
                    catch (Exception)
                    {
                        property.SetValue(obj, default);
                    }
                }
                else
                {
                    try
                    {
                        property.SetValue(obj, _context.Generate(property.PropertyType));
                    }
                    catch (Exception)
                    {
                        property.SetValue(obj, default);
                    }
                }
            }

            _types.Pop();
            return obj;
        }
    }
}
